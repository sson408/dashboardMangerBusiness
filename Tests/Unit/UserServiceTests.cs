using AutoMapper;
using dashboardManger.Controllers;
using dashboardManger.Data;
using dashboardManger.Interfaces;
using Moq;
using Xunit;
using dashboardManger.Data;
using dashboardManger.Models;

namespace dashboardManger.Tests.Unit
{
    public class UserServiceTests
    {
        private readonly MyDbContext _context;
        private readonly Mock<IUserService> _mockUserService;
        private readonly UserController _userController;
        private readonly IMapper _mapper;


        public UserServiceTests()
        {
            _mockUserService = new Mock<IUserService>();

            _userController = new UserController(_context, _mockUserService.Object, _mapper);
        }

        [Fact]
        public void TestGetUserByGuid()
        {
            // Arrange
            var userGuid = "9198696D-8F1C-4C47-B7E3-7ADF5582A4B2";

            var user = new User()
            {
                Username = "admin"
            };

            _mockUserService.Setup(x => x.GetUserByGuid(userGuid)).Returns(user);

            // Act
            var result = _userController.GetUserByGuid(userGuid);

            // Assert
            Assert.Equal(user.Username, result.Value.Username);
        }
    }
}
