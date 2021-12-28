using AutoMapper;
using Sample.UserManagement.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Sample.UserManagement.Service.Service;
using System.Threading.Tasks;
using Sample.UserManagement.DTO;
using Sample.UserManagement.Service.Model;
using Sample.UserManagement.Service.Service.Contract;

namespace Sample.UserManagement.Host.Test.Controllers 
{
    public class UserControllerTest : IClassFixture<UserControllerFixture>
    {
        private readonly UserControllerFixture _userControllerFixture;
        private readonly UserController _userController;
        private readonly UserController _mockUserController;
        public UserControllerTest(UserControllerFixture userControllerFixture)
        {
            _userControllerFixture = userControllerFixture;
            _userController = userControllerFixture.UserController;
            _mockUserController = userControllerFixture.MockUserController;
        }

        [Fact]
        public void When_TryToInstanciateUserControllerWithNullUserService_Then_ArgumentNullExceptionShouldBeThrown()
        {
            var mockMapper = new Mock<IMapper>().Object;
            Assert.Throws<ArgumentNullException>(()=>new UserController(null, mockMapper));
        }

        [Fact]
        public void When_TryToInstanciateUserControllerWithNullMapperParameter_Then_ArgumentNullExceptionShouldBeThrown()
        {
            var mockUserService = new Mock<IUserService>().Object;
            Assert.Throws<ArgumentNullException>(() => new UserController(mockUserService, null));
        }

        [Fact]
        public async Task When_TryToCallRegisterUserMethodWithNullParameter_Then_ArgumentNullExceptionShouldBeThrown()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _userController.RegisterUser(null));
        }


        public static IEnumerable<Object[]> InvalidRegisterUserDto =>
            new List<object[]>
            {
                new object[] { new RegisterUserDto{ FirstName = null,LastName="JJ",Email = "email" }},
                new object[] { new RegisterUserDto{ FirstName = "  ",LastName="JJ",Email = "email" }},
                new object[] { new RegisterUserDto{ FirstName = "Jacob",LastName=null,Email = "email" }},
                new object[] { new RegisterUserDto{ FirstName = "Jacob",LastName=" ",Email = "email" }},
                new object[] { new RegisterUserDto{ FirstName = "Jacob",LastName="JJ",Email = null }},
                new object[] { new RegisterUserDto{ FirstName = "Jacob",LastName="JJ",Email = "  "}},
            };

        [Theory]
        [MemberData(nameof(InvalidRegisterUserDto))]
        public async Task When_TryToCallRegisterUserMethodWithInvalidUserParameter_Then_ArgumentExceptionShouldBeThrown(RegisterUserDto userDto)
        {
            var registerUser = new RegisterUserDto()
            {
                FirstName = "Jacob",
                LastName = null,
                Email = "JJ@jHo.com"
            };
            await Assert.ThrowsAsync<ArgumentException>(() => _userController.RegisterUser(userDto));
        }

        [Fact]
        public async Task When_TryToCallRegisterUserMethodWithValidUserParameter_Then_RegisteredUserGUIDShouldBeReturned()
        {
            var registerUser = new RegisterUserDto()
            {
                FirstName = "Jacob",
                LastName = "Jalali",
                Email = "JJ@jHo.com"
            };

            var userId = await _userController.RegisterUser(registerUser);
            
            Assert.NotEqual(Guid.Empty, userId);
        }

        [Fact]
        public async Task When_CallRegisterUserMethodWithValidUserParameter_Then_RegisteredUserCouldBeRetrievedAndShouldBeSameIsDtoModel()
        {
            var registerUser = new RegisterUserDto()
            {
                FirstName = "Jacob",
                LastName = "Jalali",
                Email = "JJ@jHo.com"
            };

            var userId = await _userController.RegisterUser(registerUser);

            var user = await _userControllerFixture.UserService.GetUserAsync(userId);
            Assert.NotNull(user);

            Assert.True(user.FirstName.Equals(registerUser.FirstName)
                        && user.LastName.Equals(registerUser.LastName)
                        && user.Email.Equals(registerUser.Email));
        }

        [Fact]
        public async Task When_CallRegisterUserMethodWithValidUserParameter_Then_RegisterUserFromUserServiceShouldBeCalled()
        {
            var registerUser = new RegisterUserDto()
            {
                FirstName = "Jacob",
                LastName = "Jalali",
                Email = "JJ@jHo.com"
            };
            await _mockUserController.RegisterUser(registerUser);
            _userControllerFixture.MockUserService.Verify(m=>m.RegisterUser(It.IsAny<RegisterUser>()), Moq.Times.Once);
        }

        [Fact]
        public async Task When_TryToCallModifyUserMethodWithIvalidIdParameter_Then_ArgumentExceptionShouldBeThrown()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _userController.ModifyUser(Guid.Empty, null));
        }

        [Fact]
        public async Task When_TryToCallModifyUserMethodWithNullParameter_Then_ArgumentNullExceptionShouldBeThrown()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _userController.ModifyUser(Guid.NewGuid(),null));
        }

        public static IEnumerable<Object[]> InvalidModifyUserDto =>
           new List<object[]>
           {
                new object[] { new ModifyUserDto{ FirstName = null,LastName="JJ",Email = "email" }},
                new object[] { new ModifyUserDto{ FirstName = "  ",LastName="JJ",Email = "email" }},
                new object[] { new ModifyUserDto{ FirstName = "Jacob",LastName=null,Email = "email" }},
                new object[] { new ModifyUserDto{ FirstName = "Jacob",LastName=" ",Email = "email" }},
                new object[] { new ModifyUserDto{ FirstName = "Jacob",LastName="JJ",Email = null }},
                new object[] { new ModifyUserDto{ FirstName = "Jacob",LastName="JJ",Email = "  "}},
           };

        [Theory]
        [MemberData(nameof(InvalidModifyUserDto))]
        public async Task When_TryToCallModifyUserMethodWithInvalidUserParameter_Then_ArgumentExceptionShouldBeThrown(ModifyUserDto userDto)
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _userController.ModifyUser(Guid.NewGuid(),userDto));
        }

        [Fact]
        public async Task When_CallModifyUserMethodWithValidParameter_Then_UserShouldBeUpdated()
        {
            var registerUser = new RegisterUserDto()
            {
                FirstName = "Jacob",
                LastName = "Jalali",
                Email = "JJ@jHo.com"
            };
            var userId = await _userController.RegisterUser(registerUser);

            var modifyUser = new ModifyUserDto
            {
                FirstName = "Edited_Jacob",
                LastName = "Edited_Jalali",
                Email = "Edited_JJ@jHo.com"
            };

            await _userController.ModifyUser(userId, modifyUser);
            var user =await _userControllerFixture.UserService.GetUserAsync(userId);

            Assert.True(user.FirstName.Equals(modifyUser.FirstName)
                        && user.LastName.Equals(modifyUser.LastName)
                        && user.Email.Equals(modifyUser.Email));
        }

        [Fact]
        public async Task When_ModifyUserMethodWithValidUserParameterCalled_Then_ModifyUserMethodFromUserServiceShouldBeCalled()
        {
            var modifyUser = new ModifyUserDto()
            {
                FirstName = "Jacob",
                LastName = "Jalali",
                Email = "JJ@jHo.com"
            };
            await _mockUserController.ModifyUser(Guid.NewGuid(), modifyUser);
            _userControllerFixture.MockUserService.Verify(m => m.ModifyUser(It.IsAny<Guid>(),It.IsAny<ModifyUser>()), Moq.Times.Once);
        }

        [Fact]
        public async Task When_GetUsersMethodCalled_Then_ResultShouldBeListOfUsers()
        {
            var users = await _userController.GetUsers();
            Assert.NotNull(users);
            Assert.IsType<List<User>>(users);
        }

        [Fact]
        public async Task When_GetUsersMethodCalled_Then_GetAllUerFromUserServiceShouldBeCalled()
        {
            await _mockUserController.GetUsers();
            _userControllerFixture.MockUserService.Verify(m => m.GetAllUser(), Moq.Times.Once);
        }

    }
}
