The **API Layer** is the entry point for external clients to interact with your application. In Clean Architecture, this layer is responsible for handling HTTP requests, orchestrating calls to the **Application Layer**, and returning responses to clients. It should contain **Controllers**, **Request Models**, **Response Models**, **Filters** for exception handling, and **Dependency Injection Configurations**. 

For our e-commerce example, here’s a potential structure for the **API Layer**.

---

### API Layer Structure

```
Ecommerce.API
│
├── Controllers             // Handles HTTP requests and maps them to application services
│   ├── ProductsController.cs
│   └── OrdersController.cs
│
├── Models                  // Defines API-specific request and response models
│   ├── Requests
│   │   ├── CreateProductRequest.cs
│   │   └── UpdateProductStockRequest.cs
│   ├── Responses
│   │   ├── ProductResponse.cs
│   │   └── OrderResponse.cs
│
├── Filters                 // Exception filters for handling and logging errors
│   └── ApiExceptionFilter.cs
│
├── Extensions              // Dependency injection and configuration setup
│   └── ServiceExtensions.cs
│
└── Program.cs              // Sets up the application and its configurations
└── Startup.cs              // Configures services and middleware
```

---

### 1. **Controllers**

Controllers handle HTTP requests, interact with the **Application Layer**, and format responses for the client. They should be lightweight, containing only code for request mapping, validation, and response handling.

#### Example: `ProductsController`

```csharp
// ProductsController.cs
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Application.Aggregates.Product.Interfaces;
using Ecommerce.API.Models.Requests;
using Ecommerce.API.Models.Responses;
using System.Threading.Tasks;

namespace Ecommerce.API.Controllers
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

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();

            return Ok(new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock
            });
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            var productId = await _productService.CreateProductAsync(request.Name, request.Price, request.Stock);
            return CreatedAtAction(nameof(GetProduct), new { id = productId }, null);
        }

        [HttpPut("{id}/stock")]
        public async Task<ActionResult> UpdateProductStock(int id, [FromBody] UpdateProductStockRequest request)
        {
            await _productService.UpdateProductStockAsync(id, request.Stock);
            return NoContent();
        }
    }
}
```

---

### 2. **Models**

Models in the **API Layer** serve as data transfer objects for requests and responses. They translate between HTTP request payloads and the data structures needed by the **Application Layer**.

#### Requests

Request models define the structure and validation of input data. For example, the `CreateProductRequest` represents the data required to create a product.

```csharp
// CreateProductRequest.cs
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.Models.Requests
{
    public class CreateProductRequest
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }
    }
}
```

#### Responses

Response models define the structure of data returned to clients. For example, `ProductResponse` returns product details to the client.

```csharp
// ProductResponse.cs
namespace Ecommerce.API.Models.Responses
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
```

---

### 3. **Filters**

Filters handle cross-cutting concerns like error handling and logging. An `ApiExceptionFilter` can catch exceptions globally and return custom error responses.

```csharp
// ApiExceptionFilter.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Ecommerce.API.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var response = new
            {
                Message = "An error occurred while processing your request.",
                Details = context.Exception.Message
            };

            context.Result = new JsonResult(response)
            {
                StatusCode = 500
            };
        }
    }
}
```

To use the filter globally, add it in `Startup.cs` under `ConfigureServices`.

```csharp
// Startup.cs (partial)
services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilter>();
});
```

---

### 4. **Extensions**

The **Extensions** folder organizes dependency injection configurations for controllers and services.

```csharp
// ServiceExtensions.cs
using Microsoft.Extensions.DependencyInjection;
using Ecommerce.Application.Aggregates.Product.Interfaces;
using Ecommerce.Application.Aggregates.Product.Services;

namespace Ecommerce.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
```

Then, in `Startup.cs`, call `services.AddApiServices()` within `ConfigureServices`.

---

### 5. **Program.cs and Startup.cs**

The `Program.cs` file initializes the application, and `Startup.cs` configures services, middleware, and routes.

#### Program.cs

```csharp
// Program.cs
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Ecommerce.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
```

#### Startup.cs

```csharp
// Startup.cs
using Ecommerce.API.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApiServices();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
```

---

### Summary

Here’s a breakdown of each component in the **API Layer**:

- **Controllers**: Handle HTTP requests, call application services, and return HTTP responses.
- **Models**: Define request and response formats, validating inputs and structuring responses.
- **Filters**: Manage error handling, logging, and cross-cutting concerns.
- **Extensions**: Manage dependency injection configurations for services and controllers.
- **Program.cs and Startup.cs**: Initialize and configure the application, setting up middleware and routes.

This structure makes the **API Layer** clean and focused on interacting with the **Application Layer**, facilitating easy client interaction while enforcing consistent request/response patterns and handling errors gracefully.