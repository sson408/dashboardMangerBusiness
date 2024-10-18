using AutoMapper;
using dashboardManger.Controllers;
using dashboardManger.Data;
using dashboardManger.Interfaces;
using Moq;
using Xunit;
using System.Linq;
using dashboardManger.Models;
using dashboardManger.DTOs;

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

        [Fact]
        public void TestGetCurrentUser()
        {
            // Arrange
            var userGuid = "9198696D-8F1C-4C47-B7E3-7ADF5582A4B2";

            var user = new UserDTO()
            {
               UserName = "admin"
            };

            _mockUserService.Setup(x => x.GetCurrentUser(userGuid)).Returns(user);

            // Act
            var result = _userController.GetCurrentUser();

            // Assert
            Assert.Equal(user.UserName, result.Value.UserName);
        }

        [Fact]
        public void TestGetUsers()
        {
            // Arrange
            var users = new List<User>()
            {
                new User()
                {
                    Username = "admin"
                },
                new User()
                {
                    Username = "user"
                }
            };

            _mockUserService.Setup(x => x.GetAllUsers()).Returns(users);

            // Act
            var result = _userController.GetUsers();

            // Assert
            Assert.Equal(users.Count, result.Value.Count());
        }

        [Fact]
        public void TestSimpleListUser()
        {
            // Arrange
            var users = new List<UserDTO>()
            {
                new UserDTO()
                {
                    UserName = "admin"
                },
                new UserDTO()
                {
                    UserName = "user1"
                }
            };

            _mockUserService.Setup(x => x.ListAllUsers()).Returns(users);

            // Act
            var result = _userController.SimpleListUser();

            // Assert
            Assert.Equal(users.Count, result.Value.Data.Count);
           
        }

        [Fact]
        public void TestAddUser()
        {
            // Arrange
            var userUpdateSummary = new UserUpdateSummary()
            {
                UserName = "admin",
                Email = "admin@localhost",
                UserRoleId = 1,
                StateId = 1,
                DepartmentId = 1,
                PhoneNumber = "123456789",
                FirstName = "admin",
                LastName = "admin",
                Password = "admin"
            };

            var user = new User()
            {
                Username = "admin"
            };

            _mockUserService.Setup(x => x.AddUser(userUpdateSummary)).Returns(user);

            // Act
            var result = _userController.AddUser(userUpdateSummary);

            // Assert
            Assert.Equal(user.Username, result.Value.Username);
        }

        [Fact]
        public void TestListAll()
        {
            // Arrange
            var userSearchSummary = new UserSearchSummary()
            {
                FilterWord = "admin"
            };

            var users = new List<UserDTO>()
            {
                new UserDTO()
                {
                    UserName = "admin"
                },
                new UserDTO()
                {
                    UserName = "user1"
                }
            };

            _mockUserService.Setup(x => x.ListAllUsers()).Returns(users);

            // Act
            var result = _userController.ListAll(userSearchSummary);

            // Assert
            Assert.Equal(users.Count, result.Value.Data.Count);
        }


        [Fact]
        public void TestListAllWithFilter()
        {
            // Arrange
            var userSearchSummary = new UserSearchSummary()
            {
                FilterWord = "admin"
            };

            var users = new List<UserDTO>()
            {
                new UserDTO()
                {
                    UserName = "admin"
                },
                new UserDTO()
                {
                    UserName = "user1"
                }
            };

            _mockUserService.Setup(x => x.ListAllUsers()).Returns(users);

            // Act
            var result = _userController.ListAll(userSearchSummary);

            // Assert
            Assert.Equal(users.Count, result.Value.Data.Count);
        }

        [Fact]
        public void TestListAllWithStateId()
        {
            // Arrange
            var userSearchSummary = new UserSearchSummary()
            {
                StateId = 1
            };

            var users = new List<UserDTO>()
            {
                new UserDTO()
                {
                    UserName = "admin"
                },
                new UserDTO()
                {
                    UserName = "user1"
                }
            };

            _mockUserService.Setup(x => x.ListAllUsers()).Returns(users);

            // Act
            var result = _userController.ListAll(userSearchSummary);

            // Assert
            Assert.Equal(users.Count, result.Value.Data.Count);
        }

        [Fact]
        public void TestListAllWithStateIdAndFilter()
        {
            // Arrange
            var userSearchSummary = new UserSearchSummary()
            {
                StateId = 1,
                FilterWord = "admin"
            };

            var users = new List<UserDTO>()
            {
                new UserDTO()
                {
                    UserName = "admin"
                },
                new UserDTO()
                {
                    UserName = "user1"
                }
            };

            _mockUserService.Setup(x => x.ListAllUsers()).Returns(users);

            // Act
            var result = _userController.ListAll(userSearchSummary);

            // Assert
            Assert.Equal(users.Count, result.Value.Data.Count);
        }
    }
}
