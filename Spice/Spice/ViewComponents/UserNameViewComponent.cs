using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpotBuddies.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SpotBuddies.ViewComponents
{
    public class UserNameViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public UserNameViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userFormDb = await _db.ApplicationUser.FirstOrDefaultAsync(u => u.Id == claims.Value);

            return View(userFormDb);
        }
    }
}
