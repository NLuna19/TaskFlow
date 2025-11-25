using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Services.Interfaces;
using TaskFlow.Domain.Entities;

namespace TaskFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var users = await _userService.GetAllAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                // "Error interno"
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);

                if (user == null)
                {
                    // "El usuario no existe."
                    return NotFound("User not found.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                // "Error interno"
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            try
            {
                var created = await _userService.CreateAsync(user);

                return CreatedAtAction(
                    nameof(GetUser),
                    new { id = created.Id },
                    created
                );
            }
            catch (ArgumentException ex)
            {
                // "Ya existe un usuario con ese email."
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // "Error interno"
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                // "El ID no coincide."
                return BadRequest("User ID does not match.");
            }

            try
            {
                var updated = await _userService.UpdateAsync(user);

                if (!updated)
                {
                    // "El usuario no existe."
                    return NotFound("User not found.");
                }

                return Ok("User updated successfully.");
            }
            catch (Exception ex)
            {
                // "Error interno"
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var deleted = await _userService.DeleteAsync(id);

                if (!deleted)
                {
                    // "El usuario no existe."
                    return NotFound("User not found.");
                }

                return Ok("User deleted successfully.");
            }
            catch (Exception ex)
            {
                // "Error interno"
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
