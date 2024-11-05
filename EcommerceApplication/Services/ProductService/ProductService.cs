using Ecommerce.Application.Contracts;
using Ecommerce.Application.DTOs;


using Ecommerce.Application.Interfaces.Repositories;

namespace Ecommerce.Application.IServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<Guid> CreateProductAsync(ProductDto productDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProductAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProductAsync(Guid id, ProductDto productDto)
        {
            throw new NotImplementedException();
        }
    }
}