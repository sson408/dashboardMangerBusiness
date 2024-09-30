using dashboardManger.Data;
using dashboardManger.Interfaces;
using dashboardManger.Models;
using System.Collections.Generic;
using System.Linq;
using dashboardManger.DTOs;
using System.Security.Claims;
using AutoMapper;

namespace dashboardManger.Services
{
    public class UserService : IUserService
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public UserService(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.User.ToList();
        }

        public List<UserDTO> ListAllUsers()
        {
            var users = _context.User.ToList();
            return _mapper.Map<List<UserDTO>>(users);
        }

        public UserDTO GetCurrentUser(string userGuid)
        {
            if (string.IsNullOrEmpty(userGuid))
            {
                return null; 
            }

            var user = _context.User.SingleOrDefault(u => u.Guid.ToString() == userGuid);

            if (user == null)
            {
                return null;
            }

            // 将 User 实体映射为 UserDTO
            return _mapper.Map<UserDTO>(user);
        }

        public User GetUserByGuid(string guid)
        {
            return _context.User.Find(guid);
        }

        public User AddUser(UserUpdateSummary userUpdateSummary)
        {
            var newUser = new User()
            {
                Username = userUpdateSummary.UserName,
                Email = userUpdateSummary.Email,
                UserRoleId = userUpdateSummary.UserRoleId,
                StateId = userUpdateSummary.StateId,
                DepartmentId = userUpdateSummary.DepartmentId,
                PhoneNumber = userUpdateSummary.PhoneNumber,
                FirstName = userUpdateSummary.FirstName,
                LastName = userUpdateSummary.LastName,
                Password = BCrypt.Net.BCrypt.HashPassword(userUpdateSummary.Password),
            };

            _context.User.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }

        public bool UpdateUser(UserUpdateSummary userUpdateSummary)
        {
            var user = _context.User.SingleOrDefault(u => u.Guid.ToString() == userUpdateSummary.Guid);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            user.Username = userUpdateSummary.UserName;
            user.Email = userUpdateSummary.Email;
            user.UserRoleId = userUpdateSummary.UserRoleId;
            user.StateId = userUpdateSummary.StateId;
            user.DepartmentId = userUpdateSummary.DepartmentId;
            user.PhoneNumber = userUpdateSummary.PhoneNumber;
            user.FirstName = userUpdateSummary.FirstName;
            user.LastName = userUpdateSummary.LastName;
            if (!string.IsNullOrEmpty(userUpdateSummary.Password)) {
                user.Password = BCrypt.Net.BCrypt.HashPassword(userUpdateSummary.Password);
            }

            _context.User.Update(user);
            var rowsAffected = _context.SaveChanges();

            return rowsAffected > 0;
        }

        public bool DeleteUser(string userGuid)
        {
            try
            {
                var user = _context.User.SingleOrDefault(l => l.Guid.ToString() == userGuid);
                if (user != null)
                {
                    //set state to inactive and update deleted column
                    user.StateId = (int)State.Inactive;
                    user.Deleted = DateTime.Now;
                    _context.User.Update(user);
                    var affected = _context.SaveChanges();
                    return affected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            return false;
        }

        public bool BatchDeleteUsers(List<string> guids)
        {
            try
            {
                if (guids == null || guids.Count == 0)
                {
                    return false;
                }

                var users = _context.User.Where(u => guids.Contains(u.Guid.ToString())).ToList();
                if (users.Count == 0)
                {
                    return false;
                }

                //set state to inactive and update deleted column
                foreach (var user in users)
                {
                    user.StateId = (int)State.Inactive;
                    user.Deleted = DateTime.Now;
                    _context.User.Update(user);
                }
                var affected = _context.SaveChanges();
                return affected > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            return false;
        }

        public string UploadAvatar(string userGuid, IFormFile file) { 
            var user = _context.User.SingleOrDefault(u => u.Guid.ToString() == userGuid);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            //set file path
            var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "File", "Image");
            if (!Directory.Exists(uploadFolderPath))
            {
                throw new Exception("Upload folder not found");
            }

            //set file name(file name + userguid)
            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{file.Name}_{userGuid}{extension}";
            var filePath = Path.Combine(uploadFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                //copy file to stream
                file.CopyTo(stream);
            }

            var avatarUrl = $"/File/Image/{fileName}";
            user.AvatarUrl = avatarUrl;

            _context.User.Update(user);
            var rowsAffected = _context.SaveChanges();

            if (rowsAffected > 0)
            {
                return avatarUrl;
            }
            else {
                throw new Exception("Failed to upload avatar");
            }               
        }
    }
}
