using System.Collections.Generic;
using System.Linq;

namespace VsShop.Models
{
    public class MockPieRepository : IPieRepository
    {
        private List<Pie> _pies;

        public MockPieRepository()
        {
            if (_pies == null)
            {
                InitializePies();
            }
        }

        private void InitializePies()
        {
            _pies = new List<Pie>
            {
                
            };
        }

        public IEnumerable<Pie> GetAllPies()
        {
            return _pies;
        }

        public IEnumerable<Pie> GetPiesOfTheWeek()
        {
            return _pies.Where(pie => pie.IsPieOfTheWeek);
        }

        public Pie GetPieById(int pieId)
        {
            return _pies.FirstOrDefault(a => a.PieId == pieId);
        }

        public void CreatePie(Pie pie)
        {

        }

        public void UpdatePie(Pie pie)
        {

        }

        public void DeletePie(int pieID)
        {

        }
    }
}
