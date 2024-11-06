using Ecoimmerce.Domian.Product.ValueObjects;
using Ecommerce.Domain.Models;

namespace EcommerceDomain.Product.Entities
{
    public class Category : Entity<CategoryId>
    {
        public string? Name {get; private set;}
        public string? Description {get; private set;}

        public Category(CategoryId id, string name, string description) : base(id)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Category name cannot be empty.");
            
            Name = name;
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Category description cannot be empty.");
            
            Description = description;
        }
    }
}