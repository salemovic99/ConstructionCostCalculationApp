using ConstructioncostcalculationDAL.Models;

namespace ConstructioncostcalculationBLL.Repositories
{
    public interface IUnitOfwork : IDisposable
    {
        IBaseRepository<Wage> WagesRepository { get; }
        IBaseRepository<Material> MaterialsRepository { get; }
        IBaseRepository<Currency> CurrenciesRepository { get; }
        IBaseRepository<Category> CategoriesRepository { get; }
        Task<int> SaveAsync();
    }
}
