using System.Linq;
using VsShop.Models;
using VsShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VsShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepository _pieRepository;

        public HomeController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.Title = "Home page";
            var homeViewModel = new HomeViewModel()
            {
                Title = "Pies of the week",
                PiesOfTheWeek = _pieRepository.GetPiesOfTheWeek().ToList()
            };

            return View(homeViewModel);
        }
    }
}
