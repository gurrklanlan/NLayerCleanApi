using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Application.Features.Categories.Dto;
using App.Services.Categories.Create;
using App.Services.Categories.Update;

namespace App.Application.Features.Categories
{
    public interface ICategoryService
    {
        Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int categoryId);
        Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProducts();
        Task<ServiceResult<List<CategoryDto>>> GetAllListAsync();

        Task<ServiceResult<List<CategoryDto>>> GetByIdAsync(int id);
        Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request);
        Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request);
        Task<ServiceResult> DeleteAsync(int id);



    }
}
