using ConstructioncostcalculationDAL.Data;
using ConstructioncostcalculationDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstructioncostcalculationBLL.Repositories
{
    public class WagesRepository : BaseRepository<Wage>
    {
        private readonly ApplicationDbContext context;
        public WagesRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<float> getTotoalCostByCategoryAsync(int categoryId)
        {

            var wages = await context.Wages.Where(w => w.Category.CategoryId == categoryId).ToListAsync();

            return wages.Sum(w => w.Amount);

        }
    }
}
