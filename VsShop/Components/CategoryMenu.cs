using VsShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace VsShop.Components
{
    public class CategoryMenu : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryMenu(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _categoryRepository.GetAllCategories().OrderBy(a => a.CategoryName);
            return View(categories);
        }
    }
}
