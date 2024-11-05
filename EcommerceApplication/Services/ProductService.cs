using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Application.Contracts;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces.Repositories;
using Ecommerce.Application.Exceptions;
using Ecommerce.Domain.Product;

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
            if (product == null)
                throw new ProductNotFoundException($"Product with ID {id} not found.");

            return new ProductDto
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                StockQuantity = product.StockQuantity
            };
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var productDtos = new List<ProductDto>();

            foreach (var product in products)
            {
                productDtos.Add(new ProductDto
                {
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    StockQuantity = product.StockQuantity
                });
            }

            return productDtos;
        }

        public async Task<Guid> CreateProductAsync(ProductDto productDto)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
                StockQuantity = productDto.StockQuantity
            };

            await _productRepository.AddAsync(product);
            return product.Id;
        }

        public async Task<bool> UpdateProductAsync(Guid id, ProductDto productDto)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new ProductNotFoundException($"Product with ID {id} not found.");

            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.Description = productDto.Description;
            product.StockQuantity = productDto.StockQuantity;

            await _productRepository.UpdateAsync(product);
            return true;
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new ProductNotFoundException($"Product with ID {id} not found.");

            await _productRepository.DeleteAsync(id);
            return true;
        }
    }
}
