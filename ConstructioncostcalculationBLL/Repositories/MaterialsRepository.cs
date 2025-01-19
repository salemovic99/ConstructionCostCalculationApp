using ConstructioncostcalculationDAL.Data;
using ConstructioncostcalculationDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstructioncostcalculationBLL.Repositories
{
    public class MaterialsRepository : BaseRepository<Material>
    {
        private readonly ApplicationDbContext context;

        public MaterialsRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }


        public async Task<double> getMaterialCostbyCurrency(int currencyId)
        {

            var items = await context.Materials.Where(m => m.CurrencyId == currencyId).ToListAsync();

            var total = Convert.ToDouble(items.Sum(t => t.Total));
            return total;
        }


    }
}
