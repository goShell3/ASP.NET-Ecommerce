using Ecoimmerce.Domian.Product.ValueObjects;
using Ecommerce.Domain.Common.Models;
using EcommerceDomain.Product.Entities;

namespace Ecommerce.Domain.Entities
{
    public class Product : AggregateRoot<ProductId>
    {

        private readonly List<Category> categories = new();
        private readonly List<Stock> stocks = new();
        private readonly List<Specifications> specifications = new();        private readonly List<ProductImage> productImages = new();       
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public string Currency { get; private set; }

        public Category Category { get; private set; }
        public string Brand {get; private set;}
        public IReadOnlyList<Stock> Stock => stocks.AsReadOnly();
        public IReadOnlyList<Specifications> Specifications => specifications.AsReadOnly();
        public ProductImage ProductImage {get; private set;}

     
        public Product(Guid id, string name, string description, decimal price, string currency, Category category, string brand, ProductImage productImage) : base(id)
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

           
            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency cannot be empty.");
            
            
            if (string.IsNullOrWhiteSpace(brand))
                throw new ArgumentException("Brand cannot be empty.");

            Currency = currency;
            Category = category;
            Brand = brand;
            if (productImage == null)
                throw new ArgumentException("Product image cannot be null.");

            ProductImage = productImage;
        }

    }
}

