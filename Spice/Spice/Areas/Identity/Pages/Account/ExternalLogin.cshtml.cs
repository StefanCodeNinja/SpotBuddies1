using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using SpotBuddies.Models;
using SpotBuddies.Utility;

namespace SpotBuddies.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ExternalLoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<ExternalLoginModel> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ExternalLoginModel(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ILogger<ExternalLoginModel> logger,
            IEmailSender emailSender,
             RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string LoginProvider { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [Required]
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public string StreetAddress { get; set; }

            public string PhoneNumber { get; set; }

            public string City { get; set; }

            public string State { get; set; }

            public string PostalCode { get; set; }

            public string Country { get; set; }
            public string Role { get; set; }
            public string DateOfBirth { get; set; }
            public string Gender { get; set; }
            public string Locality { get; set; }
            public string GivenName { get; set; }
            public string HomePhone { get; set; }
            public string MobilePhone { get; set; }
            public string NameIdentifier { get; set; }
            public string OtherPhone { get; set; }
            public string FRole { get; set; }
            public string CookiePath { get; set; }
            public string UserData { get; set; }
            public string Surname { get; set; }
            public string System { get; set; }
            public string Thumbprint { get; set; }
            public string Upn { get; set; }
            public string Version { get; set; }
            public string Uri { get; set; }
            public string Webpage { get; set; }
            public string WindowsAccountName { get; set; }
            public string WindowsDeviceClaim { get; set; }
            public string WindowsDeviceGroup { get; set; }
            public string WindowsFqbnVersion { get; set; }
            public string WindowsSubAuthority { get; set; }
            public string WindowsUserClaim { get; set; }
            public string X500DistinguishedName { get; set; }
        }

        public IActionResult OnGetAsync()
        {
            return RedirectToPage("./Login");
        }

        public IActionResult OnPost(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);
                return LocalRedirect(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ReturnUrl = returnUrl;
                LoginProvider = info.LoginProvider;
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    Input = new InputModel
                    {
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                        FirstName = info.Principal.FindFirstValue(ClaimTypes.Name),
                        LastName = info.Principal.FindFirstValue(ClaimTypes.Name),
                        Country = info.Principal.FindFirstValue(ClaimTypes.Country),
                        Gender = info.Principal.FindFirstValue(ClaimTypes.Gender),
                        Locality = info.Principal.FindFirstValue(ClaimTypes.Locality),
                        GivenName = info.Principal.FindFirstValue(ClaimTypes.GivenName),
                        HomePhone = info.Principal.FindFirstValue(ClaimTypes.HomePhone),
                        MobilePhone = info.Principal.FindFirstValue(ClaimTypes.MobilePhone),
                        OtherPhone = info.Principal.FindFirstValue(ClaimTypes.OtherPhone),
                        NameIdentifier = info.Principal.FindFirstValue(ClaimTypes.NameIdentifier),                       
                        FRole = info.Principal.FindFirstValue(ClaimTypes.Role),
                        State = info.Principal.FindFirstValue(ClaimTypes.StateOrProvince),
                        CookiePath = info.Principal.FindFirstValue(ClaimTypes.CookiePath),
                        UserData = info.Principal.FindFirstValue(ClaimTypes.UserData),
                        StreetAddress = info.Principal.FindFirstValue(ClaimTypes.StreetAddress),
                        Surname = info.Principal.FindFirstValue(ClaimTypes.Surname),
                        System = info.Principal.FindFirstValue(ClaimTypes.System),
                        Thumbprint = info.Principal.FindFirstValue(ClaimTypes.Thumbprint),
                        Upn = info.Principal.FindFirstValue(ClaimTypes.Upn),
                        Uri = info.Principal.FindFirstValue(ClaimTypes.Uri),
                        Version = info.Principal.FindFirstValue(ClaimTypes.Version),
                        Webpage = info.Principal.FindFirstValue(ClaimTypes.Webpage),
                        WindowsAccountName = info.Principal.FindFirstValue(ClaimTypes.WindowsAccountName),
                        WindowsDeviceClaim = info.Principal.FindFirstValue(ClaimTypes.WindowsDeviceClaim),
                        WindowsDeviceGroup = info.Principal.FindFirstValue(ClaimTypes.WindowsDeviceGroup),
                        WindowsFqbnVersion = info.Principal.FindFirstValue(ClaimTypes.WindowsFqbnVersion),
                        WindowsSubAuthority = info.Principal.FindFirstValue(ClaimTypes.WindowsSubAuthority),
                        WindowsUserClaim = info.Principal.FindFirstValue(ClaimTypes.WindowsUserClaim),
                        X500DistinguishedName = info.Principal.FindFirstValue(ClaimTypes.X500DistinguishedName)
                    };
                }
                return Page();
            }
        }

        //After Register post button 
        public async Task<IActionResult> OnPostConfirmationAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            // Get the information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information during confirmation.";

                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            if (ModelState.IsValid)
            {
                          
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    City = Input.City,
                    StreetAddress = Input.StreetAddress,
                    State = Input.State,
                    PostalCode = Input.PostalCode,
                    PhoneNumber = Input.PhoneNumber,
                    Role = SD.CustomerEndUser,
                    DateOfBirth = Input.DateOfBirth,
                    Gender = Input.Gender

                };



                var result = await _userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, SD.CustomerEndUser);
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                       
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);

                        //var userId = await _userManager.GetUserIdAsync(user);
                        //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        //var callbackUrl = Url.Page(
                        //    "/Account/ConfirmEmail",
                        //    pageHandler: null,
                        //    values: new { area = "Identity", userId = userId, code = code },
                        //    protocol: Request.Scheme);

                        //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            LoginProvider = info.LoginProvider;
            ReturnUrl = returnUrl;
            return Page();
        }
    }
}
