using Microsoft.AspNetCore.Mvc;

namespace VsShop.Controllers
{
    public class AppExceptionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}