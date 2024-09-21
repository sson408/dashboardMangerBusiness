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

        [HttpGet("{guid}")]
        public ActionResult<User> GetUserByGuid(string guid)
        {
            var user = _context.User.SingleOrDefault(u => u.Guid.ToString() == guid);
            if (user == null)
            {
                return NotFound(new ApiResponse<string>(404, "User not found", null));
            }

            var userDto = _mapper.Map<UserDTO>(user);

            return Ok(new ApiResponse<UserDTO>(200, "successfully", userDto));
        }


        [HttpPost("listAll")]
        public ActionResult<PagedApiResponse<UserDTO>> ListAll([FromBody] UserSearchSummary searchSummary, [FromQuery] int pageNum = 1, int pageSize = 10) {
            try
            {
                var dataList = _context.User.AsQueryable().OrderBy(l => l.StateId).ThenBy(l => l.Username);
                var totalItems = dataList.Count();          

                //map data
                var userDtos = _mapper.Map<List<UserDTO>>(dataList);

                //filter data
                if (searchSummary != null) {
                    if (searchSummary.StateId > 0) { 
                        userDtos.RemoveAll(l => l.StateId != searchSummary.StateId);

                        totalItems = userDtos.Count();
                    }

                    if (!string.IsNullOrEmpty(searchSummary.FilterWord))
                    {
                        var filterWordList = searchSummary.FilterWord.ToLower().Split(' ');
                        foreach (var word in filterWordList) {
                            userDtos.RemoveAll(l => !l.FilterWord.Contains(word));
                        }

                        totalItems = userDtos.Count();
                    }              
                }

                var users = userDtos.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();

                // Create pagination information
                var pageInfo = new PageInfo
                {
                    PageNum = pageNum,
                    PageSize = pageSize,
                    Total = totalItems
                };

                var response = new PagedApiResponse<UserDTO>(200, "Success", pageInfo, userDtos);

                return Ok(response);

            }
            catch (Exception ex) {
                //return error message
                return StatusCode(500, new ApiResponse<string>(500, "An error occurred while processing your request", null));
            }

        
        }

        [HttpPost("create")]
        public ActionResult<User> AddUser([FromBody] UserUpdateSummary userUpdateSummary)
        {
            var newUser = _userService.AddUser(userUpdateSummary);
            var newUserGuid = newUser.Guid.ToString();
            if (string.IsNullOrEmpty(newUserGuid))
            {
                return BadRequest(new ApiResponse<string>(400, "User not added", null));
            }

            return  Ok(new ApiResponse<string>(200, "User added successfully", newUserGuid));
        }

        [HttpPost("update")]
        public ActionResult UpdateUser([FromBody] UserUpdateSummary userUpdateSummary)
        {
            var success = _userService.UpdateUser(userUpdateSummary);
            if (!success)
            {
                return BadRequest(new ApiResponse<string>(400, "User not updated", null));
            }
            return Ok(new ApiResponse<bool>(200, "User updated successfully", success));
        }

        [HttpDelete("{guid}")]
        public ActionResult DeleteUser(string guid)
        {
            try {
                var success = _userService.DeleteUser(guid);
                if (!success)
                {
                    return BadRequest(new ApiResponse<string>(400, "User not deleted", null));
                }
                return Ok(new ApiResponse<bool>(200, "User deleted successfully", success));
            }
            catch (Exception ex) {
                return StatusCode(500, new ApiResponse<string>(500, ex.Message, null));
            }

        }

        [HttpPost("batchDelete")]
        public ActionResult BatchDelete([FromBody] List<string> guids) {
            try { 
                if (guids == null || guids.Count == 0) {
                    return BadRequest(new ApiResponse<string>(400, "No user selected", null));
                }

                var success = _userService.BatchDeleteUsers(guids);
                if (!success)
                {
                    return BadRequest(new ApiResponse<string>(400, "Users not deleted", null));
                }
                return Ok(new ApiResponse<bool>(200, "Users deleted successfully", success));
            }
            catch (Exception ex) {
                return StatusCode(500, new ApiResponse<string>(500, ex.Message, null));
            }
        
        }

        [HttpPost("{userGuid}/uploadAvatar")]
        public IActionResult UploadAvatar(string userGuid, IFormFile file)
        {
            try
            {
                var avatarUrl = _userService.UploadAvatar(userGuid, file);

                return Ok(new ApiResponse<string>(200, "Avatar uploaded successfully", avatarUrl));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>(500, ex.Message, null));
            }
        }
    }
}
