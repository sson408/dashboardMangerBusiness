using AutoMapper;
using dashboardManger.Controllers;
using dashboardManger.Data;
using dashboardManger.Interfaces;
using Moq;
using Xunit;
using System.Linq;
using dashboardManger.Models;
using dashboardManger.DTOs;
using Microsoft.EntityFrameworkCore;

namespace dashboardManger.Tests.Unit
{
    public class PropertyServiceTests
    {
        private readonly MyDbContext _context;
        private readonly Mock<IPropertyService> _mockPropertyService;
        private readonly PropertyController _propertyController;
        private readonly IMapper _mapper;

        public PropertyServiceTests()
        {
            _mockPropertyService = new Mock<IPropertyService>();

            _propertyController = new PropertyController(_context, _mockPropertyService.Object, _mapper);
        }

        [Fact]
        public void TestGetPropertyByGuid()
        {
            // Arrange
            var propertyGuid = "9198696D-8F1C-4C47-B7E3-7ADF5582A4B2";

            var property = new RealEstateProperty()
            {
                Address = "property1"
            };

            _mockPropertyService.Setup(x => x.GetPropertyByGuid(propertyGuid)).Returns(property);

            // Act
            var result = _propertyController.GetPropertyByGuid(propertyGuid);

            // Assert
            Assert.Equal(property.Address, result.Value.Address);
        }


        [Fact]
        public void TestAddProperty()
        {
            // Arrange
            var property = new PropertyDTO.PropertyUpdateSummary()
            {
                Address = "property1"
            };

            var newProperty = new RealEstateProperty()
            {
                Address = property.Address
            };

            _mockPropertyService.Setup(x => x.AddProperty(property)).Returns(newProperty);

            // Act
            var result = _propertyController.AddProperty(property);

            // Assert
            Assert.Equal(property.Address, result.Value.Address);
        }

        [Fact]
        public void TestUpdateProperty()
        {
            // Arrange
            var propertyGuid = "9198696D-8F1C-4C47-B7E3-7ADF5582A4B2";

            var property = new RealEstateProperty()
            {
                Address = "property1"
            };

            _mockPropertyService.Setup(x => x.GetPropertyByGuid(propertyGuid)).Returns(property);

            var propertyUpdate = new PropertyDTO.PropertyUpdateSummary()
            {
                Address = "property2"
            };

            var updatedPropertyResult = false;

            _mockPropertyService.Setup(x => x.UpdateProperty(propertyUpdate)).Returns(updatedPropertyResult);

            // Act
            var result = _propertyController.UpdateProperty(propertyUpdate);
        }


        [Fact]
        public void TestDeleteProperty()
        {
            // Arrange
            var propertyGuid = "9198696D-8F1C-4C47-B7E3-7ADF5582A4B2";

            var property = new RealEstateProperty()
            {
                Address = "property1"
            };

            _mockPropertyService.Setup(x => x.GetPropertyByGuid(propertyGuid)).Returns(property);

            var deletedPropertyResult = false;

            _mockPropertyService.Setup(x => x.DeleteProperty(propertyGuid)).Returns(deletedPropertyResult);

            // Act
            var result = _propertyController.DeleteProperty(propertyGuid);
        }

    }


}
