using System.Linq;
using VsShop.Models;
using VsShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using VsShop.Utility;
using System;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VsShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private IMemoryCache _memoryCache;

        public HomeController(IPieRepository pieRepository, IMemoryCache memoryCache)
        {
            _pieRepository = pieRepository;
            _memoryCache = memoryCache;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.Title = "Home page";

            // caching change for IMemoryCache
            List<Pie> piesOfTheWeekCached = null;


            // code tuong duong dong: piesOfTheWeekCached = _memoryCache.GetOrCreate(CacheEntryConstants.PiesOfTheWeek, entry =>
            //if(!_memoryCache.TryGetValue(CacheEntryConstants.PiesOfTheWeek, out piesOfTheWeekCached))
            //{
            //    piesOfTheWeekCached = _pieRepository.GetPiesOfTheWeek().ToList();
            //    var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(30));
            //    //cacheEntryOptions.RegisterPostEvictionCallback(FillCacheAgain, this);

            //    _memoryCache.Set(CacheEntryConstants.PiesOfTheWeek, piesOfTheWeekCached, cacheEntryOptions);
            //}

            piesOfTheWeekCached = _memoryCache.GetOrCreate(CacheEntryConstants.PiesOfTheWeek, entry =>
            {
                entry.SetSlidingExpiration(TimeSpan.FromSeconds(30));
                entry.Priority = CacheItemPriority.High;
                return _pieRepository.GetPiesOfTheWeek().ToList();
            });

            var homeViewModel = new HomeViewModel()
            {
                Title = "Pies of the week",
                PiesOfTheWeek = piesOfTheWeekCached
            };

            return View(homeViewModel);
        }

        //private void FillCacheAgain(object key, object value, EvictionReason reason, object state)
        //{
        //    _logger.LogInformation(LogEventIds.LoadHomepage, "Cache was cleared: reason " + reason.ToString());
        //}
    }
}
