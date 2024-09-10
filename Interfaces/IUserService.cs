using dashboardManger.DTOs;
using dashboardManger.Models;
using System.Collections.Generic;

namespace dashboardManger.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        UserDTO GetCurrentUser(string userGuid);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        
        List<UserDTO> ListAllUsers();  
    }
}
