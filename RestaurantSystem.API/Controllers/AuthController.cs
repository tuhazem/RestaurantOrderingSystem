using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Application.DTOs;
using RestaurantSystem.Application.Interfaces;

namespace RestaurantSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService auth;

        public AuthController(IAuthService auth)
        {
            this.auth = auth;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO dto) {

            var res = await auth.RegisterAsync(dto);
            return Ok(res);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO dto) { 
            var result = await auth.LoginAsync(dto);
            if (result == null)
                return BadRequest("Invalid Email Or Password");
            return Ok(new { result });
        }


    }
}
