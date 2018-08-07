using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VsShop.Models
{
    public class PieReviewRepository : IPieReviewRepository
    {
        private readonly AppDbContext _appDbContext;
        public PieReviewRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddPieReview(PieReview pieReview)
        {
            _appDbContext.PieReviews.Add(pieReview);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<PieReview> GetReviewsForPie(int pieId)
        {
            var reviewsforPie = _appDbContext.PieReviews.Where(a => a.Pie.PieId == pieId);
            return reviewsforPie;
        }

    }
}
