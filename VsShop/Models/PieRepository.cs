using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
