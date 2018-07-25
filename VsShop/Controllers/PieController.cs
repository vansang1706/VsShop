using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VsShop.Models;
using VsShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace VsShop.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult List(string category)
        {
            ViewBag.Title = "Pies List";

            IEnumerable<Pie> pies;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(category))
            {
                pies = _pieRepository.GetAllPies().OrderBy(p => p.PieId);
                currentCategory = "All pies";
            }
            else
            {
                pies = _pieRepository.GetAllPies().Where(p => p.Category.CategoryName == category).OrderBy(p => p.PieId);
                currentCategory = category;
            }

            var piesListViewModel = new PiesListViewModel
            {
                Pies = pies,
                CurrentCategory = currentCategory
            };

            return View(piesListViewModel);
        }

        public IActionResult Details(int id=0)
        {
            var pie = _pieRepository.GetPieById(id);
            if(pie == null)
            {
                return NotFound();
            }
            return View(pie);
        }
    }
}