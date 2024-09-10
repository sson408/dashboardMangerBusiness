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
            return _context.Users.ToList();
        }

        public List<UserDTO> ListAllUsers()
        {
            var users = _context.Users.ToList();
            return _mapper.Map<List<UserDTO>>(users);
        }
        public UserDTO GetCurrentUser(string userGuid)
        {
            if (string.IsNullOrEmpty(userGuid))
            {
                return null; 
            }

            var user = _context.Users.SingleOrDefault(u => u.Guid.ToString() == userGuid);

            if (user == null)
            {
                return null;
            }

            // 将 User 实体映射为 UserDTO
            return _mapper.Map<UserDTO>(user);
        }

        public User GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
