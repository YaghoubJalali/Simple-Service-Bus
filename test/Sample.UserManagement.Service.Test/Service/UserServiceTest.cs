using Moq;
using Sample.ServiceBus.Contract;
using Sample.ServiceBus.Contract.QueryBus;
using Sample.ServiceBus.Handler;
using Sample.UserManagement.Service.Command.UserCommand;
using Sample.UserManagement.Service.Model;
using Sample.UserManagement.Service.Repository;
using Sample.UserManagement.Service.Repository.Contract;
using Sample.UserManagement.Service.Service;
using Sample.UserManagement.Service.Service.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sample.UserManagement.Service.Test.Service
{
    public class UserServiceTest : IClassFixture<UserServiceFixture>
    {
        private readonly UserServiceFixture _userServiceFixture;
        private readonly IUserService _userService;
        private readonly Mock<ICommandBus> _mockCommandBus;
        
        public UserServiceTest(UserServiceFixture userServiceFixture)
        {
            _userServiceFixture = userServiceFixture;
            _userService = userServiceFixture.UserService;
            _mockCommandBus = userServiceFixture.MockCommandBus;
        }

       [Fact]
        public void When_TryToInstanciateUserServiceClassWithNullCommandBusParameter_Then_ArgumentNullExceptionShouldBeThrown()
        {
            var queryDispatcher = new Mock<IQueryDispatcher>().Object;
            Assert.Throws<ArgumentNullException>(() => new UserService(null, queryDispatcher));
        }

        [Fact]
        public void When_TryToInstanciateUserServiceClassWithNullUserParameterParameter_Then_ArgumentNullExceptionShouldBeThrown()
        {
            var commandBus = new Mock<ICommandBus>().Object;
            Assert.Throws<ArgumentNullException>(() => new UserService(commandBus, null));
        }

        [Fact]
        public async Task When_CallRegisterUserWithNullParameter_Then_ArgumentNullExceptionShouldBeThrown()
        {
            async Task func() => await _userService.RegisterUser(null);
            await Assert.ThrowsAsync<ArgumentNullException>(func);
        }

        [Fact]
        public async Task When_CallRegisterUserWithValidParameter_Then_DispatchMethodOfCommandBusForRegisterUserShouldBeCalled()
        {
            var registerUser = new RegisterUser("Jacob", "Jalali", "JJ@jBo.com");

            await _userService.RegisterUser(registerUser);

            _mockCommandBus.Verify(m =>
                        m.DispatchAsync(It.IsAny<RegisterUserCommandMessage>())
                        , Moq.Times.Once);
        }

        [Fact]
        public async Task When_CallModifyUserWithInvalidId_Then_ArgumentExceptionShouldBeCalled()
        {
            async Task act() => await _userService.ModifyUser(Guid.Empty, null);
            await Assert.ThrowsAsync<ArgumentException>(act);
        }

        [Fact]
        public async Task When_CallModifyUserWithInValidModifyUser_Then_ArgumentNullExceptionShouldBeCalled()
        {
            async Task act() => await _userService.ModifyUser(Guid.NewGuid(), null);
            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }


        [Fact]
        public async Task When_CallModifyUserWithValidParameter_Then_DispatchMethodOfCommandBusForModifyUserShouldBeCalled()
        {
            var modifyUser = new ModifyUser("Jacob", "Jalali", "JJ@jBo.com");

            await _userService.ModifyUser(Guid.NewGuid(), modifyUser);

            _mockCommandBus.Verify(m =>
                        m.DispatchAsync(It.IsAny<ModifyUserCommandMessage>())
                        , Moq.Times.Once);
        }


        [Fact]
        public async Task When_CallGetAllUser_Then_ValidListOfUserSHouldBeReturned()
        {
            var users = await _userService.GetAllUser();

            Assert.NotNull(users);
            Assert.IsType<List<User>>(users);
        }
        
        [Fact]
        public async Task When_GetUserAsyncMethodWithInvalidParameterCalled_Then_ArgumentExceptionShouldBeThrow()
        {
            async Task act() => await _userService.GetUserAsync(Guid.Empty);
            await Assert.ThrowsAsync<ArgumentException>(act);
        }

        [Fact]
        public async Task When_GetUserAsyncMethodWithValidParameterForNotExistUserCalled_Then_NullShouldBeReturn()
        {
            var user = await _userService.GetUserAsync(Guid.NewGuid());
            Assert.Null(user);
        }


        [Fact]
        public void When_CallGetAllUser_Then_GetAllMethodFromUserRepositorySHouldBeCalled()
        {
            _userService.GetAllUser();
            _userServiceFixture.MockQueryDispatcher.Verify(m =>
                        m.DispatchAsync<GetUsersQuery, GetUsersQueryResult>(It.IsAny<GetUsersQuery>())
                        , Moq.Times.Once);
        }

        [Fact]
        public async Task When_GetUserAsyncMethodWithValidParameterCalled_Then_GetUserAsyncMethodFromUserRepositoryShouldBeCalled()
        {
            await _userService.GetUserAsync(Guid.NewGuid());
            _userServiceFixture.MockQueryDispatcher.Verify(m =>
                         m.DispatchAsync<GetUserQuery, GetUserQueryResult>(It.IsAny<GetUserQuery>())
                         , Moq.Times.Once);
        }
    }
}
