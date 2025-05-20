using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Application.Contracts.Persistance;
using App.Application.Features.Products.Create;
using FluentValidation;

namespace App.Services.Products.Create
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        private readonly IProductRepository _productRepository;
        public CreateProductRequestValidator(IProductRepository productRepository)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ürüm ismi gereklidir.")
                .Length(3, 10).WithMessage("Ürüm ismi 3 ile 10 karakter arasında olmalıdır.");
                 //.Must(MustUniqueProductName).WithMessage("Ürüm ismi daha önce alınmış. Lütfen başka bir isim deneyin.");
            //price validation
            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("CategoryId must be greater than 0.");

            //stock inclusiveBetween validation

            RuleFor(x => x.Stock)
                .InclusiveBetween(1, 100)
                .WithMessage("Stock must be between 1 and 100.");
            _productRepository = productRepository;
        }
        

        //1.way  sync validation!!!!!!!!!!!!!!!!!
        //private bool MustUniqueProductName(string name)
        //{
       // return   !_productRepository.Where(x => x.Name == name).Any();
        //}
    }
   
}
