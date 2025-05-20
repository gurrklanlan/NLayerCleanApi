using FluentValidation;

namespace App.Application.Features.Products.Update
{
    public class UpdateProductRequestValidator: AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Ürüm ismi gereklidir.")
               .Length(3, 10).WithMessage("Ürüm ismi 3 ile 10 karakter arasında olmalıdır.");
            //.Must(MustUniqueProductName).WithMessage("Ürüm ismi daha önce alınmış. Lütfen başka bir isim deneyin.");


            RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("CategoryId must be greater than 0.");


            //price validation
            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0.");

            //stock inclusiveBetween validation

            RuleFor(x => x.Stock)
                .InclusiveBetween(1, 100)
                .WithMessage("Stock must be between 1 and 100.");
        }
    }
}
