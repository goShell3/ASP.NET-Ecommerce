using Ecommerce.Domain.Models;
using EcommerceApplication.DTOs;

namespace EcommerceApplication.Interface
{
    public interface IOrderService
    {
        Task<OrderDTO> AddOrderAsync(Order order);
        Task<OrderDTO> DeleteOrderAsync(Guid id);
        Task<OrderDTO> GetOrderAllAsync(Guid id);
        Task<OrderDTO> GetOrderByIdAsync(Guid id);
        Task<OrderDTO> UpdateOrderAsync(Order order);
        
    }
}