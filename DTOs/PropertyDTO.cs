using System.Text.Json.Serialization;

namespace dashboardManger.DTOs
{
    public class PropertyDTO
    {
        public string Guid { get; set; }
        public string Address { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public string ListingAgent1Guid { get; set; }
        public string ListingAgent1Name { get; set; }
        public string ListingAgent2Guid { get; set; }  
        public string ListingAgent2Name { get; set; }
        public string Buyer1Guid { get; set; }
        public string Buyer1Name { get; set; }
        public string Buyer2Guid { get; set; }
        public string Buyer2Name { get; set; }
        public string OriginalOwnerGuid { get; set; }
        public string OriginalOwnerName { get; set; }
        public string ImageUrl { get; set; }
        public string ListingPrice { get; set; }
        public decimal SoldPrice { get; set; }
        public string Type { get; set; }
        public double FloorArea { get; set; }
        public double TotalArea { get; set; }
        public string DateTimeDisplay{ get; set; }
        public string UpdatedDateTimeDisplay { get; set; }

        [JsonIgnore]
        public string FilterWord { get; set; }
    }

    public class PropertySearchSummary
    {
        public string? FilterWord { get; set; }
        public int? StatusId { get; set; }
        public int? TypeId { get; set; }
    }

    public class PropertyUpdateSummary
    {
        public string Guid { get; set; }
        public string Address { get; set; }
        public int StatusId { get; set; }
        public string? ListingAgent1Guid { get; set; }
        public string? ListingAgent2Guid { get; set; }
        public string ImageUrl { get; set; }
        public string ListingPrice { get; set; }
        public decimal SoldPrice { get; set; }
        public int TypeId { get; set; }
        public double FloorArea { get; set; }
        public double TotalArea { get; set; }
        public DateTime DateTime { get; set; }
    }
}
