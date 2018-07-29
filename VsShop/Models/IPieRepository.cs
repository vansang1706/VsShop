using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VsShop.Models
{
    public interface IPieRepository
    {
        IEnumerable<Pie> GetAllPies();
        IEnumerable<Pie> GetPiesOfTheWeek();
        Pie GetPieById(int pieId);
        void UpdatePie(Pie pie);
        void CreatePie(Pie pie);
        void DeletePie(int pieId);
    }
}
