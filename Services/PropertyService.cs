using dashboardManger.Data;
using dashboardManger.Interfaces;
using dashboardManger.Models;
using System.Collections.Generic;
using System.Linq;
using dashboardManger.DTOs;
using System.Security.Claims;
using AutoMapper;
using static dashboardManger.DTOs.PropertyDTO;
using System;

namespace dashboardManger.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public PropertyService(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public RealEstateProperty GetPropertyByGuid(string guid)
        {
            var property = _context.Property.SingleOrDefault(p => p.GUID.ToString() == guid);
            if (property == null)
            {
                return null;
            }

            return property;
        }

        public bool DeleteProperty(string propertyGuid)
        {
            var property = _context.Property.SingleOrDefault(p => p.GUID == propertyGuid);
            if (property == null)
            {
                throw new Exception("Property not found");
            }

            _context.Property.Remove(property);
            var rowsAffected = _context.SaveChanges();

            return rowsAffected > 0;
        }

        public bool BatchDeleteProperties(List<string> propertyGuids)
        {
            var properties = _context.Property.Where(p => propertyGuids.Contains(p.GUID)).ToList();
            if (properties.Count == 0)
            {
                throw new Exception("Properties not found");
            }

            _context.Property.RemoveRange(properties);
            var rowsAffected = _context.SaveChanges();

            return rowsAffected > 0;
        }

        public RealEstateProperty AddProperty(PropertyUpdateSummary propertyUpdateSummary) {

            //convert datetime stamp to datetime
            var dateTimeStamp = propertyUpdateSummary.DateTimeStamp;
            //convert to nz time
            var dateTime = TimeZoneInfo.ConvertTimeFromUtc(
                DateTimeOffset.FromUnixTimeSeconds(dateTimeStamp.Value).UtcDateTime,
                TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time")
            );

            var newProperty = new RealEstateProperty()
            {
                Address = propertyUpdateSummary.Address,
                StatusId = propertyUpdateSummary.StatusId,
                TypeId = propertyUpdateSummary.TypeId,
                ListingAgent1Id = _context.User.SingleOrDefault(u => u.Guid.ToString() == propertyUpdateSummary.ListingAgent1Guid).Id,
                ListingAgent2Id = _context.User.SingleOrDefault(u => u.Guid.ToString() == propertyUpdateSummary.ListingAgent2Guid).Id,
                DateTime = dateTime,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Property.Add(newProperty);
            _context.SaveChanges();

            return newProperty;
        }

        public bool UpdateProperty(PropertyUpdateSummary propertyUpdateSummary) { 
            var property = _context.Property.SingleOrDefault(p => p.GUID == propertyUpdateSummary.Guid);
            if (property == null)
            {
                throw new Exception("Property not found");
            }

            var listingAgent1Guid = propertyUpdateSummary.ListingAgent1Guid;
            var listingAgent1Id = !string.IsNullOrEmpty(listingAgent1Guid) ? _context.User.SingleOrDefault(u => u.Guid.ToString() == listingAgent1Guid).Id :0;

            var listingAgent2Guid = propertyUpdateSummary.ListingAgent2Guid;
            var listingAgent2Id = !string.IsNullOrEmpty(listingAgent2Guid) ? _context.User.SingleOrDefault(u => u.Guid.ToString() == listingAgent2Guid).Id : 0;

            //convert datetime stamp to datetime
            var dateTimeStamp = propertyUpdateSummary.DateTimeStamp;
            //convert to nz time
            var dateTime = TimeZoneInfo.ConvertTimeFromUtc(
                DateTimeOffset.FromUnixTimeSeconds(dateTimeStamp.Value).UtcDateTime,
                TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time")
            );



            property.Address = propertyUpdateSummary.Address;
            property.StatusId = propertyUpdateSummary.StatusId;
            property.TypeId = propertyUpdateSummary.TypeId;
            property.ListingAgent1Id = listingAgent1Id;
            property.ListingAgent2Id = listingAgent2Id;
            property.DateTime = dateTime;
            property.UpdatedAt = DateTime.Now;


            _context.Property.Update(property);
            var rowsAffected = _context.SaveChanges();

            return rowsAffected > 0;
        }

        public string UploadImage(string propertyGuid, IFormFile file) {
            var property = _context.Property.SingleOrDefault(p => p.GUID == propertyGuid);
            if (property == null)
            {
                throw new Exception("Property not found");
            }


            //set file path
            var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "File", "Property");
            if (!Directory.Exists(uploadFolderPath))
            {
                throw new Exception("Upload folder not found");
            }

            //set file name(file name + userguid)
            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{file.Name}_{propertyGuid}{extension}";
            var filePath = Path.Combine(uploadFolderPath, fileName);

            // Check if the file exists and delete it
            if (File.Exists(filePath))
            {
                // Ensure the file is not being accessed by any process before deleting
                File.SetAttributes(filePath, FileAttributes.Normal); // Remove any read-only attribute
                File.Delete(filePath);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                //copy file to stream
                file.CopyTo(stream);
            }

            var imageUrl = $"/File/Property/{fileName}";
            property.ImageUrl = imageUrl;

            _context.Property.Update(property);
            var rowsAffected = _context.SaveChanges();

            if (rowsAffected > 0)
            {
                return imageUrl;
            }
            else
            {
                throw new Exception("Failed to upload image");
            }
            
        }

        public IEnumerable<RealEstateProperty> GetAllProperties()
        {
            return _context.Property.ToList();
        }

    }
}
