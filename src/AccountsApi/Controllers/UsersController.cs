using AccountsApi.Models.User;
using AccountsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserAsync(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest user)
        {
            await _userService.AddUserAsync(user);
            return Ok(new { message = "User Created!"});
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateUser(int id, [FromBody]UpdateUserRequest user)
        {
            await _userService.UpdateUserAsync(id, user);
            return Ok(new { message = "User Updated!"});
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok(new { message = "User Deleted!"});
        }
    }
}