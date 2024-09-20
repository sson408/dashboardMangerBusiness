using System.Text.Json.Serialization;

namespace dashboardManger.DTOs
{
    public class UserDTO
    {      
        public string Guid { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int UserRoleId { get; set; }
        public string UserRole { get; set; }
        public int StateId { get; set; }
        public string State { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarUrl { get; set; }
        [JsonIgnore]
        public string FilterWord { get; set; }
    }

    public class UserSearchSummary { 
        public string? FilterWord { get; set; }
        public int? StateId { get; set; }
        
    }

    public class UserUpdateSummary {

        public string Guid { get; set; }
        public string Password { get ; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int UserRoleId { get; set; }
        public int StateId { get; set; }
        public int DepartmentId { get; set; }
        public string PhoneNumber { get; set; }    
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
