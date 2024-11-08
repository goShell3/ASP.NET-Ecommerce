using Ecommerce.Domain.Models;
using EcommerceApplication.DTOs;
using EcommerceApplication.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> AddOrder([FromBody] Order order)
        {
            if (order is null) return BadRequest("Order is required.");

            var orderDto = await _orderService.AddOrderAsync(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = orderDto.Id }, orderDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderDTO>> DeleteOrder(Guid id)
        {
            var orderDto = await _orderService.DeleteOrderAsync(id);
            if (orderDto == null) return NotFound("Order not found.");
            return Ok(orderDto);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<OrderDTO>> GetOrderAll(Guid userId)
        {
            var orderDto = await _orderService.GetOrderAllAsync(userId);
            if (orderDto == null) return NotFound("No orders found for the user.");
            return Ok(orderDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrderById(Guid id)
        {
            var orderDto = await _orderService.GetOrderByIdAsync(id);
            if (orderDto == null) return NotFound("Order not found.");
            return Ok(orderDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderDTO>> UpdateOrder(Guid id, [FromBody] Order order)
        {
            if (id != order.Id) return BadRequest("Order ID mismatch.");
            var orderDto = await _orderService.UpdateOrderAsync(order);
            if (orderDto == null) return NotFound("Order not found.");
            return Ok(orderDto);
        }
    }
}
