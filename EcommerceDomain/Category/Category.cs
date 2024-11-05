using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.Models;

// Category entity
public class Category : Entity<Guid>
{
    public string name { get; set; }
    public string description {get; set; }
    public List<Product> products { get; set; } = new List<Product>();

    public Category(Guid entityId, string name, string description, List<Product> products) : base(entityId)
    {
        
        if (string.IsNullOrWhiteSpace (name))
            throw new ArgumentException ("The category name cannot be null or empty");
        if (string.IsNullOrWhiteSpace (description))
            throw new ArgumentException ("The category description cannot be null or empty");

        this.name = name;
        this.description = description;
        this.products = products;
    }

}