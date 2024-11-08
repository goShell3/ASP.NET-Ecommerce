using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.DTOs;

namespace EcommerceApplication.Interface
{
    public interface IProductService
    {
        Task<ProductDto> GetProductByIdAsync(Guid id);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<Guid> CreateProduct(ProductDto productDto);
        Task<bool> UpdateProductAsync(Guid id, ProductDto productDto);
        Task<bool> DeleteProductAsync(Guid id);
        Task CreateProductAsync(object name, object price);
        Task GetProductByIdAsync(int id);
        Task DeleteProductAsync(string name);
    }
}