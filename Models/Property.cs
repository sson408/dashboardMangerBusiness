using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dashboardManger.Models
{
    [Table("Property")]
    public class RealEstateProperty
    {
        [Key]
        public long Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string GUID { get; private set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(1024)]
        public string? Address { get; set; }

        [Required]
        public int StatusId { get; set; }
        public long? ListingAgent1Id { get; set; }

        [ForeignKey("ListingAgent1Id")]
        public User? ListingAgent1 { get; set; }

        public long? ListingAgent2Id { get; set; }

        [ForeignKey("ListingAgent2Id")]
        public User? ListingAgent2 { get; set; }

        // Buyer 1 Information
        [StringLength(50)]
        public string? Buyer1FirstName { get; set; }

        [StringLength(50)]
        public string? Buyer1LastName { get; set; }

        [StringLength(15)]
        public string? Buyer1PhoneNumber { get; set; }

        [StringLength(100)]
        public string? Buyer1Email { get; set; }

        // Buyer 2 Information
        [StringLength(50)]
        public string? Buyer2FirstName { get; set; }

        [StringLength(50)]
        public string? Buyer2LastName { get; set; }

        [StringLength(15)]
        public string? Buyer2PhoneNumber { get; set; }

        [StringLength(100)]
        public string? Buyer2Email { get; set; }

        // Original Owner Information
        [StringLength(50)]
        public string? OriginalOwnerFirstName { get; set; }

        [StringLength(50)]
        public string? OriginalOwnerLastName { get; set; }

        [StringLength(15)]
        public string? OriginalOwnerPhoneNumber { get; set; }

        [StringLength(100)]
        public string? OriginalOwnerEmail { get; set; }

        // Property Images (URL)
        [StringLength(2048)]
        public string? ImageUrl { get; set; }

        public decimal? SoldPrice { get; set; }
        public decimal? FirstPartPrice { get; set; }
        public decimal? FirstPartPercentage { get; set; }
        public decimal? RestPartPrice { get; set; }
        public decimal? RestPartPercentage { get; set; }
        public decimal? Commission { get; set; }

        // Property Type and Area
        public int TypeId { get; set; } 

        //listing date
        public DateTime DateTime { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }

}
