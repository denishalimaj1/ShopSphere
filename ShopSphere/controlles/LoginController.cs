using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.Models;
using ShopSphere.Services;
using System.Threading.Tasks;

namespace ShopSphere.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly IAuthService _authService;

        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel loginModel)
        {
            var user = await _authService.AuthenticateAsync(loginModel);

            if (user == null)
                return Unauthorized("Invalid username or password");

            var token = _authService.GenerateJwtToken(user);
            return Ok(new { token });
        }
        [Authorize]
        [HttpPut("reset-password")]
public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
{
    var result = await _authService.ResetPasswordAsync(resetPasswordModel);
    if (!result)
    {
        return NotFound("User not found.");
    }
    return NoContent();
}
    }
}
