using System;
using System.ComponentModel.DataAnnotations;

namespace BddWithSpecFlow.GeekPizza.Web.Models
{
    /// <summary>
    /// Represents the information that required for user registration
    /// </summary>
    public class RegisterInputModel
    {
        [Display(Name = "User name (required)")]
        public string UserName { get; set; }

        [Display(Name = "Password (required)")]
        public string Password { get; set; }

        [Display(Name = "Re-enter password (required)")]
        public string PasswordReEnter { get; set; }
    }
}
