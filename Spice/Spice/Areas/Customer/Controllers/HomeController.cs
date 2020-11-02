using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpotBuddies.Data;
using SpotBuddies.Models;
using SpotBuddies.Models.ViewModels;
using SpotBuddies.Utility;

namespace SpotBuddies.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            IndexViewModel IndexVM = new IndexViewModel()
            {
                MenuItem = await _db.MenuItem.Include(m => m.Category).Include(m => m.SubCategory).ToListAsync(),
                Category = await _db.Category.ToListAsync(),
                Coupon = await _db.Coupon.Where(c=>c.IsActive==true).ToListAsync()
            };

            //retruev the user identity
            var calaimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = calaimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            //retrieve the shooping cart count of the user and asign the count to the session 
            if(claim != null)
            {
                var cnt =  _db.ShoppingCart.Where(u => u.ApplicationUserId == claim.Value).ToList().Count();
                HttpContext.Session.SetInt32(SD.ssShopingCartCount, cnt);
            }
            return View(IndexVM);

        }

        [Authorize]
        public async Task<IActionResult> Details (int id)
        {
            var MenuItemFromDb = await _db.MenuItem.Include(m => m.Category).Include(m => m.SubCategory).Where(m => m.Id == id).FirstOrDefaultAsync();
            ShoppingCart cartObj = new ShoppingCart()
            {
                MenuItem = MenuItemFromDb,
                MenuItemId = MenuItemFromDb.Id
            };

            return View(cartObj);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details (ShoppingCart shoppingObj)
        {
            shoppingObj.Id = 0;
            if(ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                shoppingObj.ApplicationUserId = claim.Value;

                ShoppingCart cartFromDb = await _db.ShoppingCart.Where(c => c.ApplicationUserId == shoppingObj.ApplicationUserId 
                                                                        && c.MenuItemId == shoppingObj.MenuItemId).FirstOrDefaultAsync();

                if(cartFromDb == null)
                {
                    await _db.ShoppingCart.AddAsync(shoppingObj);
                }
                else
                {
                    cartFromDb.Count = cartFromDb.Count + shoppingObj.Count;
                }

                await _db.SaveChangesAsync();

                var count = _db.ShoppingCart.Where(c => c.ApplicationUserId == shoppingObj.ApplicationUserId).ToList().Count();
                HttpContext.Session.SetInt32(SD.ssShopingCartCount, count);

                return RedirectToAction("Index");
            }
            else
            {
                var MenuItemFromDb = await _db.MenuItem.Include(m => m.Category).Include(m => m.SubCategory).Where(m => m.Id == shoppingObj.MenuItemId).FirstOrDefaultAsync();
                ShoppingCart cartObj = new ShoppingCart()
                {
                    MenuItem = MenuItemFromDb,
                    MenuItemId = MenuItemFromDb.Id
                };

                return View(cartObj);
            }


        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
