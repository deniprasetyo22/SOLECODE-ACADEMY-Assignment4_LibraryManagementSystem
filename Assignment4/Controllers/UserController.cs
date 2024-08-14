using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment4_LibraryManagementSystem.Models;
using Assignment4_LibraryManagementSystem.Services;
using System.Net;

namespace Assignment4_LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }


        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> AddUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid input data. Please check the user data");
            }
            await _userService.AddUser(user);
            return Ok("User added successfully.");
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        // GET: api/User/5
        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetUserById(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("Invalid ID. The ID must grather than zero");
            }
            var user = await _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound($"User with ID {userId} not found.");
            }
            return Ok(user);
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid input data. Please check the user data.");
            }
            var success = await _userService.UpdateUser(userId, user);
            if (!success)
            {
                return NotFound("Employee not found.");
            }
            return Ok("User updated successfully.");
        }

        // DELETE: api/User/5
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var success = await _userService.DeleteUser(userId);
            if (!success)
            {
                return NotFound("User not found.");
            }

            return Ok("User deleted successfully.");
        }
    }
}
