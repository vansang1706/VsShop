using System.Collections.Generic;

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
