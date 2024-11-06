In Clean Architecture, the **Domain Layer** is the core of the application, containing business logic and rules. It consists of **Entities**, **Value Objects**, **Aggregates**, **Repositories** (interfaces only), **Domain Services** (if needed), and **Domain Exceptions**.

Let’s continue with the **Product** aggregate as an example to illustrate the structure and content of each component in the Domain Layer.

---

### Domain Layer Structure (Product Aggregate)

```
Ecommerce.Domain
│
├── Entities                // Core domain entities representing business concepts
│   └── Product.cs
│
├── ValueObjects            // Domain-specific value objects with behavior
│   └── Price.cs
│
├── Aggregates              // Aggregate roots defining boundaries and rules for consistency
│   └── ProductAggregate.cs
│
├── Interfaces              // Domain-level interfaces for repositories
│   └── IProductRepository.cs
│
├── Services                // Domain services for complex domain logic not suited for entities
│   └── ProductDomainService.cs
│
└── Exceptions              // Custom domain exceptions for business rule violations
    └── ProductStockException.cs
```

---

### 1. **Entities**

Entities are objects with a distinct identity, represented by a primary key, and encapsulate business rules. In our e-commerce domain, `Product` is an entity with attributes like `Id`, `Name`, `Price`, and `Stock`.

```csharp
// Product.cs
namespace Ecommerce.Domain.Entities
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public Price Price { get; private set; }
        public int Stock { get; private set; }

        public Product(int id, string name, Price price, int stock)
        {
            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
        }

        public void UpdateStock(int newStock)
        {
            if (newStock < 0)
                throw new ProductStockException("Stock cannot be negative.");

            Stock = newStock;
        }

        public void ChangePrice(Price newPrice)
        {
            Price = newPrice;
        }
    }
}
```

---

### 2. **Value Objects**

Value Objects define characteristics with no identity and are defined by their properties and behaviors. For example, `Price` could be a Value Object to represent a monetary value with currency.

```csharp
// Price.cs
namespace Ecommerce.Domain.ValueObjects
{
    public class Price
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }

        public Price(decimal amount, string currency)
        {
            if (amount < 0) throw new ArgumentException("Price cannot be negative.");
            Amount = amount;
            Currency = currency ?? throw new ArgumentNullException(nameof(currency));
        }

        public Price Add(Price other)
        {
            if (other.Currency != Currency)
                throw new InvalidOperationException("Cannot add prices in different currencies.");

            return new Price(Amount + other.Amount, Currency);
        }
    }
}
```

---

### 3. **Aggregates**

Aggregates are root entities that ensure the consistency of operations on related entities. The `ProductAggregate` serves as the root for the `Product` entity and encapsulates all business logic that must be consistent within this aggregate.

```csharp
// ProductAggregate.cs
namespace Ecommerce.Domain.Aggregates
{
    public class ProductAggregate
    {
        public Product Product { get; private set; }

        public ProductAggregate(Product product)
        {
            Product = product;
        }

        public void Restock(int additionalUnits)
        {
            Product.UpdateStock(Product.Stock + additionalUnits);
        }

        public void ChangeProductPrice(Price newPrice)
        {
            Product.ChangePrice(newPrice);
        }
    }
}
```

---

### 4. **Interfaces**

In the Domain Layer, interfaces for repositories define the methods for data access. These are implemented in the Infrastructure layer. Here, `IProductRepository` defines the contract for storing and retrieving `Product` entities.

```csharp
// IProductRepository.cs
namespace Ecommerce.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(int productId);
        Task<IEnumerable<Product>> GetAllAsync();
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int productId);
    }
}
```

---

### 5. **Domain Services**

Domain Services are used to handle complex business logic that doesn't belong within an entity or value object. For example, if restocking involves complex rules (like checking supplier availability), a `ProductDomainService` would handle this logic.

```csharp
// ProductDomainService.cs
using System.Threading.Tasks;

namespace Ecommerce.Domain.Services
{
    public class ProductDomainService
    {
        private readonly IProductRepository _productRepository;

        public ProductDomainService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task RestockProductAsync(int productId, int additionalStock)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            product.UpdateStock(product.Stock + additionalStock);
            await _productRepository.UpdateAsync(product);
        }
    }
}
```

---

### 6. **Exceptions**

Custom domain exceptions represent business rule violations or other domain-specific errors. For example, if there’s an attempt to set a negative stock level, a `ProductStockException` would be thrown.

```csharp
// ProductStockException.cs
using System;

namespace Ecommerce.Domain.Exceptions
{
    public class ProductStockException : Exception
    {
        public ProductStockException(string message) : base(message)
        {
        }
    }
}
```

---

### Summary

Here's a summary of each component in the Domain Layer:

- **Entities**: Represent core domain concepts with unique identities and encapsulate business logic.
- **Value Objects**: Represent descriptive characteristics without an identity, encapsulating behaviors and ensuring immutability.
- **Aggregates**: Ensure consistency rules for a group of related entities. The aggregate root (e.g., `ProductAggregate`) is the single entry point for operations on the aggregate.
- **Interfaces**: Define repository contracts for persistence, which are implemented in the Infrastructure layer.
- **Domain Services**: Encapsulate domain logic that doesn’t belong within entities, such as operations that require multiple repositories.
- **Exceptions**: Custom exceptions represent errors related to specific domain rules or constraints.

This structure keeps the Domain Layer isolated and focused purely on business logic and rules, making it reusable and consistent across different application layers.