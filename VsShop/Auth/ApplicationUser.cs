using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VsShop.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime Birthday { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
