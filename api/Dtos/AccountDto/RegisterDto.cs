using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.AccountDto
{
    public class RegisterDto
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Password must be minmum of 6 characters")]
        public string? Password { get; set;}
        [Required]
        [Compare("Password")]
        public string? PasswordConfirmation { get; set;}
    }
}