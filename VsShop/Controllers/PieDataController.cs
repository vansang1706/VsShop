using System.Collections.Generic;
using System.Linq;
using VsShop.Models;
using VsShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace VsShop.Controllers
{
    [Route("api/[controller]")]
    public class PieDataController : Controller
    {
        private readonly IPieRepository _pieRepository;

        public PieDataController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        [HttpGet]
        public IEnumerable<PieViewModel> LoadMorePies()
        {
            IEnumerable<Pie> dbPies = null;
            dbPies = _pieRepository.GetAllPies().OrderBy(a => a.PieId).Take(3);

            List<PieViewModel> pies = new List<PieViewModel>();
            foreach (var item in dbPies)
            {
                pies.Add(MapDbPieToPieViewModel(item));
            }
            return pies;
        }

        private PieViewModel MapDbPieToPieViewModel(Pie pie)
        {
            return new PieViewModel()
            {
                PieId = pie.PieId,
                Name = pie.Name,
                ShortDescription = pie.ShortDescription,
                Price = pie.Price,
                ImageThumbnailUrl = pie.ImageThumbnailUrl
            };
        }   
    }
}