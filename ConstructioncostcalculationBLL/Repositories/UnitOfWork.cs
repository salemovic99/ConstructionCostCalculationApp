using ConstructioncostcalculationDAL.Data;
using ConstructioncostcalculationDAL.Models;

namespace ConstructioncostcalculationBLL.Repositories
{
    public class UnitOfWork : IUnitOfwork
    {

        private readonly ApplicationDbContext context;

        public IBaseRepository<Wage> WagesRepository { get; private set; }

        public IBaseRepository<Material> MaterialsRepository { get; private set; }

        public IBaseRepository<Currency> CurrenciesRepository { get; private set; }

        public IBaseRepository<Category> CategoriesRepository { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            WagesRepository = new BaseRepository<Wage>(context);
            MaterialsRepository = new BaseRepository<Material>(context);
            CurrenciesRepository = new BaseRepository<Currency>(context);
            CategoriesRepository = new BaseRepository<Category>(context);

        }

        public void Dispose()
        {
            context.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
