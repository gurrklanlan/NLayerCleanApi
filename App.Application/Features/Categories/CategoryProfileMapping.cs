using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Application.Features.Categories.Dto;
using App.Domain.Categories;
using App.Services.Categories.Create;
using App.Services.Categories.Update;
using AutoMapper;

namespace App.Application.Features.Categories
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
