using ConstructioncostcalculationDAL.Data;
using Microsoft.EntityFrameworkCore;

namespace ConstructioncostcalculationBLL.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<T> dbSet;
        public BaseRepository(ApplicationDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await dbSet.ToListAsync();

        public async Task<T> GetByIdAsync(int id) => await dbSet.FindAsync(id);
        public async Task<bool> AddAsync(T entity)
        {
            try
            {
                if (entity is null)
                    return false;

                await dbSet.AddAsync(entity);
                await context.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            try
            {
                if (id == 0)
                    return false;

                var item = await GetByIdAsync(id);

                if (item is null)
                    return false;

                dbSet.Remove(item);
                await context.SaveChangesAsync();

                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }


        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                if (entity is null)
                    return false;

                dbSet.Update(entity);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
