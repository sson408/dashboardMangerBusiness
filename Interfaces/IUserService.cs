using dashboardManger.DTOs;
using dashboardManger.Models;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace dashboardManger.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetUserByGuid(string guid);
        UserDTO GetCurrentUser(string userGuid);
        User AddUser(UserUpdateSummary userUpdateSummary);
        bool UpdateUser(UserUpdateSummary userUpdateSummary);
        bool DeleteUser(string userGuid);
        bool BatchDeleteUsers(List<string> userGuids);
        List<UserDTO> ListAllUsers();  
        string UploadAvatar(string userGuid, IFormFile file);

    }
}
