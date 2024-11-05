using Ecommerce.Constructs.Models;
using Microsoft.AspNetCore.Mvc;


namespace Ecommerce.Api.Controllers
{

    // list of product controller 
    [ApiController]
    [Route("/products/{hostId}/product")]
    public class ProductViewController : ControllerBase {
        // private readonly IAuthenticationService _authenticationService;

        // public ProductViewController(IAuthenticationService authenticationService) : base(authenticationService) {
        //     _authenticationService = authenticationService;
        // }

        [HttpGet]
        public IActionResult CreateProducts(CreateProductRequest request, string hostId) {

            return Ok(request);
        }

    }
    
}