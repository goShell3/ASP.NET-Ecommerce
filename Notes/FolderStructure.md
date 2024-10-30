In a typical ASP.NET application following **Clean Architecture** or **Onion Architecture**, the code is divided into layers or projects: **API**, **Application**, **Domain**, and **Infrastructure (often called the Infrastructure or Data layer)**. Here’s what each layer includes:

---

### 1. **API Layer**
   - **Purpose**: The entry point for clients to interact with the application, often containing controllers to expose endpoints.
   - **Key Components**:
     - **Controllers**: Define routes and handle HTTP requests, delegating work to the Application layer.
     - **Dependency Injection Setup**: Registers services and configurations.
     - **Middleware**: Authentication, logging, exception handling, etc.
     - **DTOs (Data Transfer Objects)**: For request and response models.

   **Example Structure**:
   ```csharp
   // Controller for managing products
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
       public async Task<IActionResult> GetProduct(int id)
       {
           var product = await _productService.GetProductByIdAsync(id);
           return product == null ? NotFound() : Ok(product);
       }
   }
   ```

---

### 2. **Application Layer**
   - **Purpose**: Contains business logic and application services, coordinating work between the API and Domain layers.
   - **Key Components**:
     - **Service Interfaces and Implementations**: Define methods for application use cases.
     - **Command and Query Handlers**: Handle CQRS (Command-Query Responsibility Segregation) commands and queries.
     - **Validators**: Validate requests, often with libraries like FluentValidation.
     - **Mappers**: Map domain models to DTOs or ViewModels.
     - **DTOs**: Request and response models, used within application services.

   **Example Structure**:
   ```csharp
   public class ProductService : IProductService
   {
       private readonly IProductRepository _productRepository;

       public ProductService(IProductRepository productRepository)
       {
           _productRepository = productRepository;
       }

       public async Task<ProductDto> GetProductByIdAsync(int id)
       {
           var product = await _productRepository.GetByIdAsync(id);
           return product == null ? null : new ProductDto(product);
       }
   }
   ```

---

### 3. **Domain Layer**
   - **Purpose**: The core of the application, holding entities, value objects, aggregates, and core business rules.
   - **Key Components**:
     - **Entities**: Define core domain models and behavior.
     - **Value Objects**: Immutable types that represent a concept, such as Money or Address.
     - **Domain Services**: Business logic that doesn’t fit within a single entity.
     - **Interfaces**: Define repository interfaces or other core abstractions.
     - **Events**: Domain events to represent meaningful changes within the domain.

   **Example Structure**:
   ```csharp
   public class Product
   {
       public int Id { get; private set; }
       public string Name { get; private set; }
       public decimal Price { get; private set; }

       public Product(int id, string name, decimal price)
       {
           Id = id;
           Name = name;
           Price = price;
       }

       public void UpdatePrice(decimal newPrice)
       {
           if (newPrice <= 0) throw new ArgumentException("Price must be positive.");
           Price = newPrice;
       }
   }
   ```

---

### 4. **Infrastructure Layer**
   - **Purpose**: Handles data access, external integrations, and other low-level operations.
   - **Key Components**:
     - **Repositories**: Implement repository interfaces defined in the Domain layer to handle data persistence.
     - **Data Contexts**: For ORM (e.g., Entity Framework `DbContext` classes).
     - **External Services**: Integrations with third-party APIs or services.
     - **Configurations**: Settings for databases, caching, and other infrastructure-level dependencies.

   **Example Structure**:
   ```csharp
   public class ProductRepository : IProductRepository
   {
       private readonly AppDbContext _context;

       public ProductRepository(AppDbContext context)
       {
           _context = context;
       }

       public async Task<Product> GetByIdAsync(int id)
       {
           return await _context.Products.FindAsync(id);
       }

       public async Task AddAsync(Product product)
       {
           await _context.Products.AddAsync(product);
           await _context.SaveChangesAsync();
       }
   }
   ```

---

### Summary of Layers and Responsibilities

- **API Layer**: Handles client requests and responses.
- **Application Layer**: Contains business logic and mediates between the API and Domain.
- **Domain Layer**: Core domain logic, entities, and business rules.
- **Infrastructure Layer**: Data access and external integrations.

This structure allows the API layer to be easily tested or swapped out, maintains separation of concerns, and keeps core business logic in the Domain layer, making the application more modular and maintainable.