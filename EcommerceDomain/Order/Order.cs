using Ecommerce.Domain.O;
using EcommerceDomain.Order.Entities;

namespace Ecommerce.Domain.Models
{
    public class Order : Entity<OrderId>
    {
        public string id {get; set;}
        public readonly Guid UserId;
        public readonly OrderId orderId = new OrderId(Guid.NewGuid());
        public readonly Guid Id;

        public string Name { get; private set; }
        public decimal TotalAmount { get; private set; }
        public List<OrderItem>? Items { get; private set; } = new List<OrderItem>();
        public User? User { get; private set; }

        public Order(string id, string name, List<OrderItem> items, User user) : base(new OrderId(Guid.NewGuid()))
        {
            this.id = id;
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Order name cannot be null or empty");
            if (items is null || items.Count == 0)
                throw new ArgumentException("Order items cannot be null or empty");

            UserId = user.Id;
            Name = name;
            Items = items;
            User = user;

            
        }
    }
}
}
