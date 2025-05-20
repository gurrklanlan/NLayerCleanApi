using App.Application.Contracts.Persistance;
using App.Domain.Categories;
using App.Persistance;
using Microsoft.EntityFrameworkCore;

namespace App.Persistance.Categories
{
    public class CategoryRepository(AppDbContext context) : GenericRepository<Category>(context), ICategoryRepository
    {
         
       
        public Task<Category?> GetCategoryWithProductsAsync(int id)
        {
          return  Context.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
                
        }

        public IQueryable<Category> GetCategoryWithProductsAsync()
        {
            return Context.Categories.Include(x => x.Products).AsQueryable();
        }

        Task<List<Category>> ICategoryRepository.GetCategoryWithProductsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
