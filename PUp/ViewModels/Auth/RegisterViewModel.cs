using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PUp.ViewModels.Auth
{
    public class RegisterViewModel:BaseModelView
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "String {0} must be at last {2} caracters.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "String {0} must be at last {2} caracters.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password ")]
        [Compare("Password", ErrorMessage = "Password not matching.")]
        public string ConfirmPassword { get; set; }
    }
}