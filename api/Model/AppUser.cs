using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Model
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; } 
        public string? Password { get; set; }
        public string? PasswordConfirmation { get; set; }

        public List<Portfolio> portfolios = new List<Portfolio>();
    }
}