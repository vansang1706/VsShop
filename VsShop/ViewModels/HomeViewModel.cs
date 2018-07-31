using VsShop.Models;
using System.Collections.Generic;

namespace VsShop.ViewModels
{
    public class HomeViewModel
    {
        public string Title { get; set; }
        public List<Pie> PiesOfTheWeek { get; set; }
    }
}
