using Microsoft.AspNetCore.Mvc;

namespace graduation.WebUI.Site.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
