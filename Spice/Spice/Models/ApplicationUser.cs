using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotBuddies.Models
{
    public class ApplicationUser : IdentityUser
    {


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Family { get; set; }
        public string StreetAddress { get; set; }
        public string StreetNumber { get; set; }
        public string Apartment { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Role { get; set; }
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
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
}
