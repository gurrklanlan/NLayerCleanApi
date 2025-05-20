using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Categories;

namespace App.Application.Contracts.Persistance
{
    public interface ICategoryRepository:IGenericRepository<Category>
    {
        Task<Category?> GetCategoryWithProductsAsync(int id);
        Task<List<Category>> GetCategoryWithProductsAsync();
    }
}
