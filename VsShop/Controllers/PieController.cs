using System.Collections.Generic;
using System.Linq;
using VsShop.Models;
using VsShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace VsShop.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPieReviewRepository _pieReviewRepository;
        private readonly HtmlEncoder _htmlEncoder;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository, IPieReviewRepository pieReviewRepository, HtmlEncoder htmlEncoder)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
            _pieReviewRepository = pieReviewRepository;
            _htmlEncoder = htmlEncoder;
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

        [Route("[controller]/[action]/{id}")]
        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie == null)
                return NotFound();

            return View(new PieDetailViewModel() { Pie = pie });
        }

        [Route("[controller]/[action]/{id}")]
        [HttpPost]
        public IActionResult Details(int id, string review)
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie == null)
                return NotFound();

            //_pieReviewRepository.AddPieReview(new PieReview() { Pie = pie, Review = review });

            string encodedReview = _htmlEncoder.Encode(review);
            _pieReviewRepository.AddPieReview(new PieReview() { Pie = pie, Review = encodedReview });

            return View(new PieDetailViewModel() { Pie = pie });
        }
    }
}