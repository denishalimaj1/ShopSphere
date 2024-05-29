using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.Models;
using ShopSphere.Services;
using System;
using System.Threading.Tasks;

namespace ShopSphere.Controllers
{
    [ApiController]
    [Route("api/")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserRegistrationModel>> Register(User user)
        {
            try
            {
                var registeredUser = await _userService.RegisterAsync(user);
                return Ok(registeredUser);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to register user: {ex.Message}");
            }
        }
        [Authorize]
        [HttpPut("update/{id}")]
        public async Task<ActionResult<UserRegistrationModel>> Update(int id, UpdateUserModel userDto)
        {
            try
            {
                var updatedUser = await _userService.UpdateUserAsync(id, userDto);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to update user: {ex.Message}");
            }
        }
        [Authorize]
        [HttpDelete("user/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
        try
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to delete user: {ex.Message}");
        }
        }
        [Authorize]
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }
        [Authorize]
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<GetUserDetails>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
}

    }

