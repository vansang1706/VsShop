using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace VsShop.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly AppDbContext _appDbContext;

        public PieRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Pie> GetAllPies()
        {
            return _appDbContext.Pies.Include(c=>c.Category);
        }

        public IEnumerable<Pie> GetPiesOfTheWeek()
        {
            return _appDbContext.Pies.Where(pie=>pie.IsPieOfTheWeek);
        }

        public Pie GetPieById(int pieId)
        {
            return _appDbContext.Pies.FirstOrDefault(a => a.PieId == pieId);
        }

        public void UpdatePie(Pie pie)
        {
            _appDbContext.Pies.Update(pie);
            _appDbContext.SaveChanges();
        }

        public void CreatePie(Pie pie)
        {
            _appDbContext.Pies.Add(pie);
            _appDbContext.SaveChanges();
        }

        public void DeletePie(int pieId)
        {
            var pie = _appDbContext.Pies.FirstOrDefault(a => a.PieId == pieId);
            _appDbContext.Pies.Remove(pie);
            _appDbContext.SaveChanges();
        }
    }
}
