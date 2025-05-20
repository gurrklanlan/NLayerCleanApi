using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Repositories.Categories;
using App.Repositories.Products;
using App.Services.Categories.Create;
using App.Services.Categories.Update;
using App.Services.Products.Create;
using App.Services.Products.Update;
using AutoMapper;

namespace App.Services.Categories
{
    public class CategoryProfileMapping:Profile
    {
        public CategoryProfileMapping()
        {
            CreateMap<CategoryDto, Category>().ReverseMap();

            CreateMap<Category, CategoryWithProductsDto>().ReverseMap();


            CreateMap<CreateCategoryRequest, Category>().ForMember(dest => dest.Name,
               opt => opt.MapFrom(src => src.name.ToLowerInvariant()));

            CreateMap<UpdateCategoryRequest, Category>().ForMember(dest => dest.Name,
               opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
        }
    }
}
