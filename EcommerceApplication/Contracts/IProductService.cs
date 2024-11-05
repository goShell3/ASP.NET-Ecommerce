using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Application.DTOs;

namespace Ecommerce.Application.Contracts
{
    public interface IProductService
    {
        Task<ProductDto> GetProductByIdAsync(Guid id);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<Guid> CreateProductAsync(ProductDto productDto);
        Task<bool> UpdateProductAsync(Guid id, ProductDto productDto);
        Task<bool> DeleteProductAsync(Guid id);
    }
}
