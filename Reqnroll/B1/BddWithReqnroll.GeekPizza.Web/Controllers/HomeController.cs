using BddWithReqnroll.GeekPizza.Web.Models;
using BddWithReqnroll.GeekPizza.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace BddWithReqnroll.GeekPizza.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        // GET: api/home
        [HttpGet]
        public HomePageModel GetHomePageModel(string token = null)
        {
            var model = new HomePageModel();
            model.MainMessage = "Welcome to Geek Pizza!";
            model.UserName = AuthenticationServices.GetCurrentUserName(HttpContext, token);
            model.IsAdmin = AuthenticationServices.IsAdmin(HttpContext, token);
            return model;
        }
    }
}