using System.Net;
using App.Repositories;
using App.Repositories.Categories;
using App.Services.Categories.Create;
using App.Services.Categories.Update;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace App.Services.Categories
{
    public class CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper):ICategoryService
    {
        public async Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int categoryId)
        {
            var category = await categoryRepository.GetCategoryWithProductsAsync(categoryId);
            if (category is null)
            {
                return ServiceResult<CategoryWithProductsDto>.Fail("Categori bulunamadı", HttpStatusCode.NotFound);
            }
            var categoryAsDto = mapper.Map<CategoryWithProductsDto>(category);
            return ServiceResult<CategoryWithProductsDto>.Success(categoryAsDto);
        }

        public async Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProducts()
        {
            var category = await categoryRepository.GetCategoryWithProducts().ToListAsync();
            
            var categoryAsDto = mapper.Map<List<CategoryWithProductsDto>>(category);
            return ServiceResult<List<CategoryWithProductsDto>>.Success(categoryAsDto);
        }

        public async Task<ServiceResult<List<CategoryDto>>> GetAllListAsync()
        {
            var categories = await categoryRepository.GetAll().ToListAsync(); 

            var categoriesAsDto = mapper.Map<List<CategoryDto>>(categories);
            return ServiceResult<List<CategoryDto>>.Success(categoriesAsDto);
        }

        // Removed duplicate method definition for GetByIdAsync(int id) that caused CS0111 error.
       
        public async  Task<ServiceResult<int>> Create(CreateCategoryRequest request)
        {
            var anyCategory = await categoryRepository.Where(x => x.Name == request.name).AnyAsync();
            if ((anyCategory))
            {
                return ServiceResult<int>.Fail("Categori ismi veritabanınıda bulunmaktadır", HttpStatusCode.NotFound);
            }




            var newCategory = mapper.Map<Category>(request);

            await categoryRepository.AddAsync(newCategory);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult<int>.SuccessAsCreated(newCategory.Id, $"api/categories/{newCategory.Id}");
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return ServiceResult.Fail("Categori bulunamadı", HttpStatusCode.NotFound);
            }
            
            var isCategoryNameExist = 
                await categoryRepository.Where(x => x.Name == request.Name && x.Id != id).AnyAsync();

            if (isCategoryNameExist)
            {
                return ServiceResult.Fail("Bu isimde bir kategori zaten var", HttpStatusCode.BadRequest);
            }

            category=mapper.Map(request, category);

            categoryRepository.Update(category);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.OK);

        }

        public async Task<ServiceResult<List<CategoryDto>>> GetByIdAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                return ServiceResult<List<CategoryDto>>.Fail("Categori bulunamadı", HttpStatusCode.NotFound);
            }
            var categoryAsDto = mapper.Map<List<CategoryDto>>(category);
            return ServiceResult<List<CategoryDto>>.Success(categoryAsDto);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                return ServiceResult.Fail("Categori bulunamadı", HttpStatusCode.NotFound);
            }
            categoryRepository.Delete(category);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

      
    }
}
