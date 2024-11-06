The **Infrastructure Layer** is responsible for handling external concerns like data persistence, API calls, messaging, and other integrations. This layer implements interfaces defined in the **Domain Layer** and provides concrete functionality, allowing the application to interact with external resources.

For our **e-commerce** example, here’s what the Infrastructure Layer might look like:

---

### Infrastructure Layer Structure

```
Ecommerce.Infrastructure
│
├── Persistence                // Data persistence and repository implementations
│   ├── Repositories
│   │   ├── ProductRepository.cs
│   │   └── OrderRepository.cs
│   ├── Configurations
│   │   └── ProductConfiguration.cs
│   └── EcommerceDbContext.cs
│
├── Services                   // External service implementations (e.g., payment, email)
│   ├── PaymentService.cs
│   └── EmailService.cs
│
├── Mapping                    // Data mapping profiles between domain entities and database models
│   └── ProductMappingProfile.cs
│
├── Extensions                 // Dependency injection and configuration setup
│   └── DependencyInjection.cs
│
└── Logging                    // Infrastructure-specific logging implementation
    └── FileLogger.cs
```

---

### 1. **Persistence**

The **Persistence** folder contains database-related configurations, the data context, and implementations of repository interfaces defined in the **Domain Layer**.

#### Repositories

Repositories manage database operations like CRUD actions. Here’s the `ProductRepository` implementing `IProductRepository`.

```csharp
// ProductRepository.cs
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommerceDbContext _context;

        public ProductRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetByIdAsync(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
```

#### DbContext

The `EcommerceDbContext` class uses **Entity Framework Core** to manage the database context and configuration.

```csharp
// EcommerceDbContext.cs
using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Persistence
{
    public class EcommerceDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EcommerceDbContext).Assembly);
        }
    }
}
```

#### Configurations

Entity configurations define mappings for entities, specifying constraints and relationships. Here’s an example for the `Product` entity.

```csharp
// ProductConfiguration.cs
using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.OwnsOne(p => p.Price, price =>
            {
                price.Property(p => p.Amount).HasColumnName("Amount");
                price.Property(p => p.Currency).HasColumnName("Currency").HasMaxLength(3);
            });
        }
    }
}
```

---

### 2. **Services**

This folder contains implementations for external services like payment gateways or email notifications.

#### Example: Payment Service

```csharp
// PaymentService.cs
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        public async Task<bool> ProcessPayment(decimal amount, string currency)
        {
            // Call to an external payment provider API
            return true; // Assuming payment was successful
        }
    }
}
```

---

### 3. **Mapping**

Mapping profiles translate between **domain models** and **database models** or **DTOs**. Here, **AutoMapper** is used to define mappings.

```csharp
// ProductMappingProfile.cs
using AutoMapper;
using Ecommerce.Domain.Entities;
using Ecommerce.Application.Aggregates.Product.DTOs;

namespace Ecommerce.Infrastructure.Mapping
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Price.Currency));
        }
    }
}
```

---

### 4. **Extensions**

The **Extensions** folder includes methods for dependency injection, configuring services and repositories in `Startup.cs`.

```csharp
// DependencyInjection.cs
using Microsoft.Extensions.DependencyInjection;
using Ecommerce.Application.Aggregates.Product.Interfaces;
using Ecommerce.Infrastructure.Persistence.Repositories;

namespace Ecommerce.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPaymentService, PaymentService>();

            return services;
        }
    }
}
```

---

### 5. **Logging**

The **Logging** folder contains custom logging implementations if you need special logging strategies.

```csharp
// FileLogger.cs
using System;
using System.IO;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Logging
{
    public class FileLogger : IFileLogger
    {
        private readonly string _logFilePath;

        public FileLogger(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public async Task LogAsync(string message)
        {
            using (StreamWriter writer = new StreamWriter(_logFilePath, append: true))
            {
                await writer.WriteLineAsync($"{DateTime.Now}: {message}");
            }
        }
    }
}
```

---

### Summary

Here's an overview of each component in the **Infrastructure Layer**:

- **Persistence**: Manages database configurations, repositories, and DbContext.
  - **Repositories**: Implement database access methods to fulfill repository interfaces from the **Domain Layer**.
  - **DbContext**: Manages the connection to the database and entity configurations.
  - **Configurations**: Set up mappings and constraints for each entity.

- **Services**: Contains implementations of external services (e.g., payment, email).
- **Mapping**: Defines profiles for mapping domain entities to DTOs or database models.
- **Extensions**: Provides dependency injection configurations.
- **Logging**: Provides custom logging solutions, such as file logging.

This layer helps maintain clean separation between business logic (Domain Layer) and the infrastructure and technology-specific concerns. By depending on interfaces, the Infrastructure Layer can be replaced or modified without affecting the core application logic.