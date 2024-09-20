using System.ComponentModel.DataAnnotations;

namespace dashboardManger.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        public Guid Guid { get; private set; } = Guid.NewGuid();

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string? Password { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public int UserRoleId { get; set; }

        [Required]
        public int StateId { get; set; }

        public int? DepartmentId { get; set; }

        [StringLength(1000)]
        public string? AvatarUrl { get; set; }

        [StringLength(15)]
        public string? PhoneNumber { get; set; }

        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }

        public DateTime? Deleted { get; set; }

    }
}
