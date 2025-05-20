using System.Net;
using App.Application.Contracts.Persistance;
using App.Application.Contracts.ServiceBus;
using App.Application.Features.Products.Create;
using App.Application.Features.Products.Update;
using App.Application.Features.Products.UpdateStock;
using App.Domain.Events;
using App.Domain.Products;
using AutoMapper;
using FluentValidation;


namespace App.Application.Features.Products
{
    public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork,IValidator
        <CreateProductRequest> createProductRequestValidator, IMapper mapper,
        IServiceBus busService) : IProductService
    {

        private const string ProductListCacheKey = "productListCacheKey";

        public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
        {
            var products = await productRepository.GetTopPriceProductAsync(count);

            var productAsDto=mapper.Map<List<ProductDto>>(products); //AutoMapper ile mapping yapıldı

           


            return new ServiceResult<List<ProductDto>>()
            {
                Data = productAsDto
            };
        }

        public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
        {
            var products = await productRepository.GetAllAsync();


            //manuel mapping
            //var productAsDto = products.Select(p => new ProductDto
            //(p.Id, p.Name, p.Price, p.Stock)).ToList();


            var productAsDto = mapper.Map<List<ProductDto>>(products);  


            return ServiceResult<List<ProductDto>>.Success(productAsDto); 

        }

        public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize)
        {
            //1-10 => ilk 10 kaydı alır skip(0).Take(10)
            //2-10 => 11-20 arası kayıtları alır skip(10).Take(10)
            //3-10 => 21-30 arası kayıtları alır skip(20).Take(10)


            var products = await productRepository.GetAllPagedAsync(pageNumber, pageSize);
            

            //manuel mapping
            // var productAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

            var productAsDto = mapper.Map<List<ProductDto>>(products);
            return ServiceResult<List<ProductDto>>.Success(productAsDto);
        }

        public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product is null)
            {
                return ServiceResult<ProductDto?>.Fail("Product not found", HttpStatusCode.NotFound);
            }

            //manuel mapping
            //var productAsDto = new ProductDto(id, product!.Name, product.Price, product.Stock);

            var productAsDto = mapper.Map<ProductDto>(product); //AutoMapper ile mapping yapıldı
            return ServiceResult<ProductDto>.Success(productAsDto)!;
        }
        public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
        {
            //2.way async manuel service business check 
            //var anyProduct=await productRepository.Where(x=>x.Name == request.Name).AnyAsync();
            //if (anyProduct)
            // {
            //   return ServiceResult<CreateProductResponse>.Fail("Product name already exists", HttpStatusCode.BadRequest);
            // }







            var product = mapper.Map<Product>(request);
            await productRepository.AddAsync(product);
            await unitOfWork.SaveChangesAsync();

            await busService.PublishAsync(new ProductAddedEvent(product.Id, product.Name, product.Price));

            return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id),
               $"api/products/{product.Id}");
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product is null)
            {
                return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
            }

            var isProductNameExist = await productRepository.AnyAsync(x => x.Name == request.Name && x.Id != product.Id);
            if (isProductNameExist)
            {
                return ServiceResult.Fail("Product name already exists", HttpStatusCode.BadRequest);
            }



            product=mapper.Map(request, product); //AutoMapper ile mapping yapıldı


            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request)
        {
            var product = await productRepository.GetByIdAsync(request.ProductId);
            if (product is null)
            {
                return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
            }
            product.Stock = request.Quantity;
            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product is null)
            {
                return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
            }
            productRepository.Delete(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }


    }
   
}
