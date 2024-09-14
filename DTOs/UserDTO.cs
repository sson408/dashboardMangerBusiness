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
        [JsonIgnore]
        public string FilterWord { get; set; }
    }

    public class UserSearchSummary { 
        public string? FilterWord { get; set; }
        public int? StateId { get; set; }
        
    }
}
