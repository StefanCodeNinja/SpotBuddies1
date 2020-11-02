using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spice.Data;
using Spice.Models;
using Spice.Utility;

namespace Spice.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.ManagerUser)]
    [Area("Admin")]
    public class CouponController : Controller
    {

        private readonly ApplicationDbContext _db;

        public CouponController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _db.Coupon.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Coupon coupons)
        {

            //if the file is valid fetch the image from the file and save it
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                
                //if file.Count > 0 that means the file is selected
                if(files.Count>0)
                {
                    //convert the file/image into bytes to store it into the database

                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }

                    coupons.Picture = p1;
                }
                _db.Coupon.Add(coupons);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(coupons);
        }

        [HttpGet]
        public async Task<IActionResult> Edit (int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var coupon = await _db.Coupon.FindAsync(id);
            if(coupon == null)
            {
                return NotFound();
            }

            return View(coupon);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Coupon coupon1)
        {
            var coupon = coupon1;

            if(coupon == null)
            {
                return NotFound();
            }
            var couponDB = await _db.Coupon.FindAsync(coupon1.Id);
            if(couponDB == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;

                if (files.Count > 0)
                {
                    byte[] np = null;
                    using (var fs = files[0].OpenReadStream())
                    {
                        using(var ms = new MemoryStream())
                        {
                            fs.CopyTo(ms);
                            np = ms.ToArray();
                        }
                    }
                    couponDB.Picture = np;
                }

                couponDB.Id = coupon.Id;
                couponDB.Name = coupon.Name;
                couponDB.Discount = coupon.Discount;
                couponDB.IsActive = coupon.IsActive;
                couponDB.MinimumAmount = coupon.MinimumAmount;
                couponDB.CouponType = coupon.CouponType;

                _db.Coupon.Update(couponDB);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coupon1);
        }
        //GET Details
        public async Task<IActionResult> Details (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var coupon = await _db.Coupon.FindAsync(id);
            if (coupon == null)
            {
                return NotFound();
            }
            return View(coupon);
        }

        //Delete Get
        [HttpGet]
        public async Task<IActionResult> Delete (int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var coupon = await _db.Coupon.FindAsync(id);
            if(coupon == null)
            {
                return NotFound();
            }
            return View(coupon);

        }

        //Delete POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var coupon = await _db.Coupon.FindAsync(id);
            if(coupon == null)
            {
                return NotFound();
            }
            _db.Coupon.Remove(coupon);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}