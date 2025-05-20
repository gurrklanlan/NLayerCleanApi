using App.Application.Contracts.Persistance;
using App.Application.Features.Products.Create;
using App.Application.Features.Products.Update;
using App.Domain.Products;


using AutoMapper;

namespace App.Services.Products
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
