using dashboardManger.DTOs;
using dashboardManger.Models;
using static dashboardManger.DTOs.PropertyDTO;

namespace dashboardManger.Interfaces
{
    public interface IPropertyService
    {
        IEnumerable<RealEstateProperty> GetAllProperties();

        RealEstateProperty GetPropertyByGuid(string guid);

        RealEstateProperty AddProperty(PropertyUpdateSummary propertyUpdateSummary);

        bool SetSold(PropertyUpdateSummary propertyUpdateSummary);
        bool UpdateProperty(PropertyUpdateSummary propertyUpdateSummary);
        bool DeleteProperty(string propertyGuid);

        bool BatchDeleteProperties(List<string> propertyGuids);

        string UploadImage(string propertyGuid, IFormFile file);
    }
}
