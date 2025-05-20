using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Repositories.Products;
using App.Services.Create;
using App.Services.Products.Create;
using App.Services.Products.Update;
using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;

namespace App.Services.Mapping
{
    public class MappingProfile : Profile
    {
        protected MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateProductRequest, Product>().ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));

            CreateMap<UpdateProductRequest, Product>().ForMember(dest => dest.Name,
               opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
        }

      
    }
}
