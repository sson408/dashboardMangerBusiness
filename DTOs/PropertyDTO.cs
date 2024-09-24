using dashboardManger.Models;
using System.Net;
using System;
using System.Text.Json.Serialization;

namespace dashboardManger.DTOs
{
    public class PropertyDTO
    {
        public class PropertyDTOSummary
        {
            public PropertyDTOSummary() { }
          
            public PropertyDTOSummary(RealEstateProperty realEstateProperty)
            {
                Guid = realEstateProperty.GUID;
                Address = realEstateProperty.Address;
                StatusId = realEstateProperty.StatusId;
                Status = Enum.GetName(typeof(ProperyStatus), realEstateProperty.StatusId);
                ListingAgent1Id = realEstateProperty.ListingAgent1Id;
                ListingAgent2Id = realEstateProperty.ListingAgent2Id;
                Buyer1FirstName = realEstateProperty.Buyer1FirstName;
                Buyer1LastName = realEstateProperty.Buyer1LastName;
                Buyer1PhoneNumber = realEstateProperty.Buyer1PhoneNumber;
                Buyer1Email = realEstateProperty.Buyer1Email;
                Buyer2FirstName = realEstateProperty.Buyer2FirstName;
                Buyer2LastName = realEstateProperty.Buyer2LastName;
                Buyer2PhoneNumber = realEstateProperty.Buyer2PhoneNumber;
                Buyer2Email = realEstateProperty.Buyer2Email;
                OriginalOwnerFirstName = realEstateProperty.OriginalOwnerFirstName;
                OriginalOwnerLastName = realEstateProperty.OriginalOwnerLastName;
                OriginalOwnerPhoneNumber = realEstateProperty.OriginalOwnerPhoneNumber;
                OriginalOwnerEmail = realEstateProperty.OriginalOwnerEmail;
                ImageUrl = realEstateProperty.ImageUrl;
                ListingPrice = realEstateProperty.ListingPrice;
                SoldPrice = realEstateProperty.SoldPrice;
                TypeId = realEstateProperty.TypeId;
                FloorArea = realEstateProperty.FloorArea;
                TotalArea = realEstateProperty.TotalArea;
                DateTimeDisplay = realEstateProperty.DateTime.ToString("dd MMM yyyy");
                UpdatedDateTimeDisplay = realEstateProperty.UpdatedAt.ToString("dd MMM yyyy HH:mm");
            }
            [JsonIgnore]
            public long Id { get; set; }
            public string Guid { get; set; }
            public string Address { get; set; }
            public int StatusId { get; set; }
            public string Status { get; set; }
            [JsonIgnore]
            public long? ListingAgent1Id { get; set; }
            public string? ListingAgent1Guid { get; set; }
            public string? ListingAgent1Name { get; set; }
            [JsonIgnore]
            public long? ListingAgent2Id { get; set; }
            public string? ListingAgent2Guid { get; set; }
            public string? ListingAgent2Name { get; set; }
            public string? ListingAgentNameDisplay { get; set; }
            public string? Buyer1FirstName { get; set; }
            public string? Buyer1LastName { get; set; }
            public string? Buyer1PhoneNumber { get; set; }
            public string? Buyer1Email { get; set; }
            public string? Buyer2FirstName { get; set; }
            public string? Buyer2LastName { get; set; }
            public string? Buyer2PhoneNumber { get; set; }
            public string? Buyer2Email { get; set; }
            public string? OriginalOwnerFirstName { get; set; }
            public string? OriginalOwnerLastName { get; set; }
            public string? OriginalOwnerPhoneNumber { get; set; }
            public string? OriginalOwnerEmail { get; set; }
            public string? ImageUrl { get; set; }
            public string? ListingPrice { get; set; }
            public decimal? SoldPrice { get; set; }
            public int? TypeId { get; set; }
            public string? Type { get; set; }
            public double? FloorArea { get; set; }
            public double? TotalArea { get; set; }
            public string? DateTimeDisplay { get; set; }
            public string? UpdatedDateTimeDisplay { get; set; }

            [JsonIgnore]
            public string? FilterWord { get; set; }
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
            public string? ListingAgent1Name { get; set; }
            public string? ListingAgent2Guid { get; set; }
            public string? ListingAgent2Name { get; set; }
            public string? Buyer1FirstName { get; set; }
            public string? Buyer1LastName { get; set; }
            public string? Buyer1PhoneNumber { get; set; }
            public string? Buyer1Email { get; set; }
            public string? Buyer2FirstName { get; set; }
            public string? Buyer2LastName { get; set; }
            public string? Buyer2PhoneNumber { get; set; }
            public string? Buyer2Email { get; set; }
            public string? OriginalOwnerFirstName { get; set; }
            public string? OriginalOwnerLastName { get; set; }
            public string? OriginalOwnerPhoneNumber { get; set; }
            public string? OriginalOwnerEmail { get; set; }
            public string ImageUrl { get; set; }
            public string ListingPrice { get; set; }
            public decimal SoldPrice { get; set; }
            public int TypeId { get; set; }
            public double FloorArea { get; set; }
            public double TotalArea { get; set; }
            public DateTime DateTime { get; set; }
        }
    }
}


