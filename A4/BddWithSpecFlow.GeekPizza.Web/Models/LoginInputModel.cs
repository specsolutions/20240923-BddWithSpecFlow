using System.ComponentModel.DataAnnotations;

namespace BddWithSpecFlow.GeekPizza.Web.Models
{
    public class LoginInputModel
    {
        [Display(Name = "User name")]
        public string Name { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
