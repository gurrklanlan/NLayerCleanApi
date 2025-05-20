using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Application.Contracts.Persistance;
using App.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace App.Persistance.Products
{
    public class ProductRepository(AppDbContext context) : GenericRepository<Product>(context), IProductRepository
    {
        public async Task<List<Product>> GetTopPriceProductAsync(int count)
        {
            return await Context.Products.OrderByDescending(x=>x.Price).Take(count).ToListAsync();

        }
    }
}
