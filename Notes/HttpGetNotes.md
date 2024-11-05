In a clean architecture setup, Dependency Injection (DI) facilitates data flow from the database (Infrastructure Layer) to the API controller (Presentation Layer) by injecting dependencies in each layer. Let's walk through an example of how to set up DI in each layer to enable an HTTP `GET` request for data retrieval in an ASP.NET Core application.

### Scenario: Retrieve a List of Products in an Ecommerce Application

The goal is to:
1. Define data models in the **Domain Layer**.
2. Implement data access using `DbContext` in the **Infrastructure Layer**.
3. Define an application service to handle business logic in the **Application Layer**.
4. Expose data through an API endpoint in the **Presentation Layer**.

---

### Step 1: Define the Domain Layer

Create a **Product** entity in the **Domain Layer**.

```csharp
// Domain/Entities/Product.cs
namespace Ecommerce.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
```

### Step 2: Set Up the Infrastructure Layer

1. Define the **DbContext** to access the database.
2. Create a repository interface and its implementation.

```csharp
// Infrastructure/Data/EcommerceDbContext.cs
using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Data
{
    public class EcommerceDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options)
            : base(options)
        {
        }
    }
}

// Infrastructure/Repositories/IProductRepository.cs
using Ecommerce.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
    }
}

// Infrastructure/Repositories/ProductRepository.cs
using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommerceDbContext _context;

        public ProductRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
```

### Step 3: Implement the Application Layer

Define a **ProductService** to handle business logic and use the repository.

```csharp
// Application/Services/IProductService.cs
using Ecommerce.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
    }
}

// Application/Services/ProductService.cs
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }
    }
}
```

### Step 4: Set Up the Presentation Layer (API Controller)

In the **API Controller**, inject `IProductService` to handle HTTP requests.

```csharp
// Presentation/Controllers/ProductsController.cs
using Ecommerce.Application.Services;
using Ecommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }
    }
}
```

### Step 5: Configure Dependency Injection in Startup or Program.cs

Register all services, repositories, and `DbContext` in the **DI Container**.

```csharp
// Program.cs or Startup.cs
using Ecommerce.Application.Services;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure DbContext
builder.Services.AddDbContext<EcommerceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EcommerceDatabase")));

// Register repositories and services
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

// Add Controllers
builder.Services.AddControllers();

var app = builder.Build();

// Middleware pipeline
app.UseRouting();
app.MapControllers();
app.Run();
```

### Data Flow Summary

1. **HTTP Request**: A client sends a `GET` request to `api/products`.
2. **API Controller (Presentation Layer)**: `ProductsController` receives the request and calls `_productService.GetAllProductsAsync()`.
3. **Application Layer**: `ProductService` in the Application Layer processes the request and uses `_productRepository` to fetch data.
4. **Infrastructure Layer**: `ProductRepository` accesses the database through `EcommerceDbContext` and retrieves the product data.
5. **Response**: The data flows back through each layer to the controller, which then returns the data to the client.

---

### Benefits of Using Dependency Injection

- **Loose Coupling**: Each layer relies on abstractions (interfaces) rather than specific implementations.
- **Testability**: Services and repositories can be mocked easily for unit testing.
- **Maintainability**: Changing implementations in one layer does not affect other layers directly. 

This setup enables a clean, modular architecture, leveraging Dependency Injection to ensure each component has its dependencies managed centrally, allowing easy testing and modifications.