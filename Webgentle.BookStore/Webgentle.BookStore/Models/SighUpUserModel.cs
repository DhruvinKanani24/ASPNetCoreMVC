using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Webgentle.BookStore.Models
{
    public class SighUpUserModel
    {
        [Required(ErrorMessage ="Please enter your firstname")]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage="Please enter your email")]
        [Display(Name ="Email Address")]
        [EmailAddress(ErrorMessage ="Please enter valid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter a strong password")]
        [Compare("ConfirmPassword", ErrorMessage = "password does not match")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please confirm your password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
