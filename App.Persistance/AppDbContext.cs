using System.Reflection;
using App.Domain.Categories;
using App.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace App.Persistance
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
       
        public DbSet<Product> Products { get; set; }=default!;
        public DbSet<Category> Categories { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            base.OnModelCreating(modelBuilder);
        }
    }
}
