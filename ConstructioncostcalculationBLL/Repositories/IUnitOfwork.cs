using ConstructioncostcalculationDAL.Models;

namespace ConstructioncostcalculationBLL.Repositories
{
    public interface IUnitOfwork : IDisposable
    {
        WagesRepository WagesRepository { get; }
        MaterialsRepository MaterialsRepository { get; }
        IBaseRepository<Currency> CurrenciesRepository { get; }
        IBaseRepository<Category> CategoriesRepository { get; }
        Task<int> SaveAsync();
    }
}
