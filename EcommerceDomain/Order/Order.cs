using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.Models
{
    public class Order : Entity<Guid>
    {
        public string Name { get; private set; }
        public decimal TotalAmount { get; private set; }
        public List<OrderItem> Items { get; private set; } = new List<OrderItem>();
        public User? User { get; private set; }

        public Order(Guid id, string name, List<OrderItem> items, User user) : base(id)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Order name cannot be null or empty");
            if (items is null || items.Count == 0)
                throw new ArgumentException("Order items cannot be null or empty");

            Name = name;
            Items = items;
            User = user;

            // Associate each item with this order
            foreach (var item in items)
            {
                item.AssignOrder(this);
            }

            CalculateTotalAmount();
        }

        private void CalculateTotalAmount()
        {
            TotalAmount = 0;
            foreach (var item in Items)
            {
                TotalAmount += item.Product.Price * item.Quantity;
            }
        }
    }

    public class OrderItem
    {
        public required Product Product { get; set; }
        public int Quantity { get; set; }

        // Optional association back to the Order if needed
        public Order? Order { get; private set; }

        public OrderItem(Product product, int quantity)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            Quantity = quantity > 0 ? quantity : throw new ArgumentException("Quantity must be greater than zero.");
        }

        public void AssignOrder(Order order)
        {
            Order = order;
        }
    }
}
