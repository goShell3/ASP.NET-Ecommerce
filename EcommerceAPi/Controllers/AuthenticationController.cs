using Ecommerce.Application.Services.Authenticaton;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Constructs.Authentication;
using RegisterRequest = Ecommerce.Constructs.Authentication.RegisterRequest;
using LoginRequest = Ecommerce.Constructs.Authentication.LoginRequest;


namespace Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase 
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService) 
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request) 
        {

            var authRequest = _authenticationService.Register(
                request.FirstName, 
                request.LastName, 
                request.Email, 
                request.Password);

            var response = new AuthenticationResponse (
                authRequest.Id,
                authRequest.firstName,
                authRequest.lastName,
                authRequest.email,
                authRequest.token
            );

            return Ok(response);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var authRequest = _authenticationService.Login(
                request.Email, 
                request.Password
            );

            if (authRequest == null) 
            {
                return Unauthorized("Invalid email or password.");
            }

            var response = new AuthenticationResponse(
                authRequest.Id,
                authRequest.firstName,
                authRequest.lastName,
                authRequest.email,
                authRequest.token 
            );

            return Ok(response);
        } 
    }
}
