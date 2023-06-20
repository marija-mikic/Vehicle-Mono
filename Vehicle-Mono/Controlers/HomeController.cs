using Microsoft.AspNetCore.Mvc;

namespace Vehicle_Mono.Controlers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
