using AutoMapper;
using dashboardManger.Data;
using dashboardManger.DTOs;
using dashboardManger.Interfaces;
using dashboardManger.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace dashboardManger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(MyDbContext context, IUserService userService, IMapper mapper)
        {
            _context = context;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpGet("currentUser")]
        public ActionResult<UserDTO> GetCurrentUser()
        {
            var userGuid = User.FindFirstValue("Guid");

            if (string.IsNullOrEmpty(userGuid))
            {
                return Unauthorized(new ApiResponse<string>(401, "User is not authenticated", null));
            }

            var user = _userService.GetCurrentUser(userGuid);

            if (user == null)
            {
                return NotFound(new ApiResponse<string>(404, "User not found", null));
            }

            return Ok(new ApiResponse<UserDTO>(200, "get user successfully ", user));
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(new ApiResponse<string>(404, "User not found", null));
            }

            var userDto = _mapper.Map<UserDTO>(user);

            return Ok(new ApiResponse<UserDTO>(200, "successfully", userDto));
        }

        [HttpPost]
        public ActionResult<User> AddUser(User user)
        {
            _userService.AddUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            _userService.UpdateUser(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id);
            return NoContent();
        }
    }
}
