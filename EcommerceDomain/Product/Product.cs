using System;
using Ecommerce.Domain.Models;  // Base entity class and value object

namespace Ecommerce.Domain.Entities
{
    public class Product : Entity<Guid>  
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }

     
        public Product(Guid id, string name, string description, decimal price, int stockQuantity) : base(id)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be empty.");
            
            if (price < 0)
                throw new ArgumentException("Price cannot be negative.");

            Name = name;
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Product description cannot be empty.");

            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
        }

        // Business method to adjust stock when an order is placed
        public void ReduceStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");
            
            if (quantity > StockQuantity)
                throw new InvalidOperationException("Insufficient stock available.");

            StockQuantity -= quantity;
        }

        // Business method to change the price with validation
        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice < 0)
                throw new ArgumentException("New price cannot be negative.");
            
            Price = newPrice;
        }

        // Optional: Method to restock inventory
        public void Restock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Restock quantity must be positive.");
            
            StockQuantity += quantity;
        }
    }
}
