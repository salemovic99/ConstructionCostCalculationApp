using ConstructioncostcalculationDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstructioncostcalculationDAL.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Material> Materials { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Wage> Wages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seeding Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "أجر بناء" },
                    new Category { CategoryId = 2, CategoryName = "أجر طلوع مواد بناء" },
                    new Category { CategoryId = 3, CategoryName = "أجر الصبة" },
                    new Category { CategoryId = 4, CategoryName = "أجر تأسيس الكهرباء" },
                    new Category { CategoryId = 5, CategoryName = "أجر النجار" }
            );

            // Seeding Currencies
            modelBuilder.Entity<Currency>().HasData(
                new Currency { CurrencyId = 1, CurrencyName = "ريال يمني" },
                    new Currency { CurrencyId = 2, CurrencyName = "ريال سعودي" }

            );
        }
    }
}
