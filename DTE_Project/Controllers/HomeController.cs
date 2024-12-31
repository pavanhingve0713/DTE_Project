using Microsoft.AspNetCore.Mvc;

namespace DTE_Project.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
