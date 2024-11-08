using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecoimmerce.Domian.Product.ValueObjects;
using EcommerceDomain.Order.Entities;

namespace EcommerceApplication.DTOs
{
    public class OrderDTO
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? TotalAmount { get; set; }
        public List<OrderItem>? Items { get; set; }
        public User? User { get; set; }
        
        
    }

    public class OrderItemDTO
    {
        public string? ProductId { get; set; }
        public string? Quantity { get; set; }
        
    }

    public class UserDTO
    {
        public string? Id { get; set; }
    }
}