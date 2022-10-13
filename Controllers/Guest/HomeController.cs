using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers.Guest
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("Home");
        }
    }
}
