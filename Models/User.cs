using System.ComponentModel.DataAnnotations;

namespace dashboardManger.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public Guid Guid { get; private set; } = Guid.NewGuid();

        [Required]
        [StringLength(50)]
        public string? Username { get; set; }

        [Required]
        [StringLength(100)]
        public string? Password { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
    }
}
