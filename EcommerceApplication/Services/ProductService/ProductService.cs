using Ecoimmerce.Domian.Product.ValueObjects;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Interfaces.Repositories;
using Ecommerce.Domain.Entities;
using EcommerceApplication.Interface;
using EcommerceDomain.Product.Entities;

namespace Ecommerce.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            // if (product == null)
            //     throw new Exceptions.ProductNotFoundException($"Product with ID {id} not found.");

            return new ProductDto
            {
                
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Currency = product.Currency,
                category = product.Category,
                Brand = product.Brand,
                stocks = product.Stock.ToList()
            };
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(product => new ProductDto
            {
              
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Currency = product.Currency,
                category = product.Category,
                Brand = product.Brand,
                stocks = product.Stock.ToList()
            });
        }

    
        public async Task<Guid> CreateProduct(ProductDto productDto)
        {
            if (string.IsNullOrEmpty(productDto.Name))
            {
                throw new ArgumentException("Product name cannot be null or empty.", nameof(productDto.Name));
            }

            var product = new Product(
                new ProductId(Guid.NewGuid()), // Assuming ProductId is a value object
                productDto.Name,
                productDto.Description ?? string.Empty,
                productDto.Price,
                productDto.Currency ?? string.Empty,
                productDto.category ?? throw new ArgumentNullException(nameof(productDto.category)),
                productDto.Brand ?? throw new ArgumentNullException(nameof(productDto.Brand)),
                productDto.productImage ?? throw new ArgumentNullException(nameof(productDto.productImage))
                
               
            );

            await _productRepository.AddAsync(product);
            return product.EntityId.Value; 
        }

        public async Task<bool> UpdateProductAsync(Guid id, ProductDto productDto)
        {
            var product = await _productRepository.GetByIdAsync(id);
            // if (product == null)
            //     throw new ProductNotFoundException($"Product with ID {id} not found.");

            // product.Name = productDto.Name;
            // product.Price = productDto.Price;
            // product.Description = productDto.Description;

            // await _productRepository.UpdateAsync(product);
            // return true;
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            // if (product == null)
            //     throw new ProductNotFoundException($"Product with ID {id} not found.");

            await _productRepository.DeleteAsync(id);
            return true;
        }

        public Task CreateProductAsync(object name, object price)
        {
            throw new NotImplementedException();
        }

        public Task GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }


        Task IProductService.DeleteProductAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}