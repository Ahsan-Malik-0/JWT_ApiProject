using Microsoft.AspNetCore.Mvc;
using WebProjectAPIs.Models.DTOs;
using WebProjectAPIs.Services;

namespace WebProjectAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthServices _authServices;

        public AuthController(AuthServices authServices)
        {
            _authServices = authServices;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUser)
        {
            if (registerUser == null) return BadRequest(); // Return 400 if the request body is null

            await _authServices.RegisterAsync(registerUser);
            return Ok("User registered successfully"); // Return 200 with a success message
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUser)
        {
            if (loginUser == null) return BadRequest(); // Return 400 if the request body is null

            var token = await _authServices.LoginAsync(loginUser);
            if (token == null) return Unauthorized(); // Return 401 if authentication fails

            return Ok(token); // Return 200 with the JWT token
        }
    }
}
