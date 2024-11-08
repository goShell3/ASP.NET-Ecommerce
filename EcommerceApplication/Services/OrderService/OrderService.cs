using Ecommerce.Domain.Models;  // Domain model for Order
using EcommerceApplication.DTOs;
using EcommerceApplication.Interface;
using Ecommerce.Domain.Repositories; // The IOrderRepository interface
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecoimmerce.Domian.Product.ValueObjects;
using Ecommerce.Domain.O;
using EcommerceDomain.Order.Entities;

namespace EcommerceApplication.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;  // Repository to interact with the database
        private readonly IUserRepository _userRepository;    // Assuming you have a user repository for User data

        public OrderService(IOrderRepository orderRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        // Add a new order
        public async Task<OrderDTO> AddOrderAsync(Order order)
        {
            if (order is null)
                throw new ArgumentNullException(nameof(order));

        
            await _orderRepository.AddAsync(order);
            return await ConvertToDTOAsync(order);
        }

        // Delete an order by its ID
        public async Task<OrderDTO> DeleteOrderAsync(Guid id)
        {
            var order = await _orderRepository.GetById(id);
            // if (order is null)
            //     return null;

            // Delete the order from the repository
            await _orderRepository.Delete(id);

            return await ConvertToDTOAsync(order);
        }

        // Get all orders (No user filter)
        public async Task<OrderDTO> GetOrderAllAsync(Guid userId)
        {
            var orders = await _orderRepository.GetAllAsync();
            if (orders == null || !orders.Any())
                throw new ArgumentNullException();

            // Retrieve the first order (or all orders, based on your requirements)
            var firstOrder = orders.First();

            return await ConvertToDTOAsync(firstOrder);
        }

        // Get an order by its ID
        public async Task<OrderDTO> GetOrderByIdAsync(Guid id)
        {
            var order = await _orderRepository.GetById(id);
            // if (order is null)
            //     return null;

            return await ConvertToDTOAsync(order);
        }

        // Update an existing order
        public Task<OrderDTO> UpdateOrderAsync(Order order)
        {
            // if (order is null)
            //     throw new ArgumentNullException(nameof(order));

            // var existingOrder = await _orderRepository.GetById(order.orderId);
            // // if (existingOrder == null)
            // //     return null;

            // // Update the existing order with new data
            // existingOrder.Name = order.Name;
            // existingOrder.TotalAmount = order.TotalAmount;
            // existingOrder.UserId = order.User;
            // existingOrder.Items = order.Items?.Select(i => new OrderItem(i.productId, i.Quantity)).ToList() ?? new List<OrderItem>();

            // // Save updated order to the repository
            // await _orderRepository.UpdateAsync(existingOrder);

            // return await ConvertToDTOAsync(existingOrder);
            throw new NotImplementedException();
        }

        // Helper method to convert an Order to OrderDTO
        private async Task<OrderDTO> ConvertToDTOAsync(Order order)
        {
            var user = await _userRepository.GetByIdAsync(order.UserId); // Assuming you have a method to get User by ID

            return new OrderDTO
            {
                Id = order.id.ToString(),
                Name = order.Name,
                TotalAmount = order.TotalAmount.ToString("F2"),
                User = user,
                // Items = order.Items.Select(i => new OrderItemDTO
                // {
                //     ProductId = i.productId.ToString(),
                //     Quantity = i.Quantity.ToString()
                // }).ToList()
                
            };
        }
    }
}
