using ConstructioncostcalculationDAL.Data;
using ConstructioncostcalculationDAL.Models;

namespace ConstructioncostcalculationBLL.Repositories
{
    public class UnitOfWork : IUnitOfwork
    {

        private readonly ApplicationDbContext context;

        public WagesRepository WagesRepository { get; private set; }

        public MaterialsRepository MaterialsRepository { get; private set; }

        public IBaseRepository<Currency> CurrenciesRepository { get; private set; }

        public IBaseRepository<Category> CategoriesRepository { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            WagesRepository = new WagesRepository(context);
            MaterialsRepository = new MaterialsRepository(context);
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
