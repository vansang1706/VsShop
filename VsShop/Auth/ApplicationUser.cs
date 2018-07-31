using Microsoft.AspNetCore.Identity;
using System;

namespace VsShop.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime Birthday { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
