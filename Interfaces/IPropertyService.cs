using dashboardManger.DTOs;
using dashboardManger.Models;
using static dashboardManger.DTOs.PropertyDTO;

namespace dashboardManger.Interfaces
{
    public interface IPropertyService
    {
        IEnumerable<RealEstateProperty> GetAllProperties();

        RealEstateProperty GetPropertyByGuid(string guid);
    }
}
