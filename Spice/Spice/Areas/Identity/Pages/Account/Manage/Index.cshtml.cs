using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpotBuddies.Data;
using SpotBuddies.Models;

namespace SpotBuddies.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _db;
      

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Display(Name = "Street")]
            public string StreetAddress { get; set; }
            [Display(Name = "Str. Number")]
            public string StreetNumber { get; set; }
            [Display(Name = "Apartment")]
            public string Apartment { get; set; }
            [Display(Name = "City")]
            public string City { get; set; }
            [Display(Name = "State")]
            public string State { get; set; }
            [Display(Name = "Postal Code")]
            public string PostalCode { get; set; }
            [Display(Name = "Country")]
            public string Country { get; set; }

        }



        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
           
            //my code
            var userId = await _userManager.GetUserIdAsync(user);
            var appUser = await _db.ApplicationUser.FindAsync(userId);


            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                StreetAddress = appUser.StreetAddress,
                StreetNumber = appUser.StreetNumber,
                Apartment = appUser.Apartment,
                City = appUser.City,
                PostalCode = appUser.PostalCode,
                Country = appUser.Country
            };
        }




        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);


            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);

                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }


            //Update if any changes in Applicaton User profile 
            var userIdForUpdate = await _userManager.GetUserIdAsync(user);
            var appUser = await _db.ApplicationUser.FindAsync(userIdForUpdate);

            if (appUser != null)
            {

                appUser.FirstName = Input.FirstName;
                appUser.LastName = Input.LastName;
                appUser.StreetAddress = Input.StreetAddress;
                appUser.StreetNumber = Input.StreetNumber;
                appUser.Apartment = Input.Apartment;
                appUser.PostalCode = Input.PostalCode;
                appUser.City = Input.City;
                appUser.Country = Input.Country;

                //Save changes 
                _db.ApplicationUser.Update(appUser);
                await _db.SaveChangesAsync();

            }
            


            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
