using Moq;
using Sample.ServiceBus.Contract;
using Sample.UserManagement.Service.Command.UserCommand;
using Sample.UserManagement.Service.DatabaseModel;
using Sample.UserManagement.Service.Event;
using Sample.UserManagement.Service.Repository.Contract;
using Sample.UserManagement.Service.Test.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sample.UserManagement.Service.Test.Command.UserCommand.Handler
{
    public class UserCommandHandlerTest : IClassFixture<UserCommandHandlerFixture>
    {
        private readonly UserCommandHandlerFixture _userCommandHandlerFixture;

        public UserCommandHandlerTest(UserCommandHandlerFixture userCommandHandler)
        {
            _userCommandHandlerFixture = userCommandHandler;
        }

        public static IEnumerable<Object[]> InvalidConstructorParameterValue =>
            new List<object[]>
            {
                new object[] { null ,null},
                new object[] { null, new Mock<IEventAggregator>().Object },
                new object[] { new Mock<IUserRepository>().Object,null },
            };
        [Theory]
        [MemberData(nameof(InvalidConstructorParameterValue))]
        public void When_TryToInstantiateUserCommandHandlerWithNullParameter_Then_ArgumentNullExceptionShouldBeThrown
            (IUserRepository userRepository,IEventAggregator eventAggregator)
        {
            Assert.Throws<ArgumentNullException>((Action)(() => new UserCommandHandler(userRepository,eventAggregator)));
        }

        [Fact]
        public async Task When_CallHandleAsyncMethodWithRegisterUserCommandMessageNullParamter_Then_ArgumentNullExceptionShouldBeThrown()
        {
            RegisterUserCommandMessage registerUser = null;
            async Task act() => await _userCommandHandlerFixture.UserCommandHandler.HandelAsync(registerUser);
            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }

        [Fact]
        public async Task When_CallHandleAsyncMethodWithRegisterUserCommandMessageParamter_Then_AddAsyncInUserRepositoryShouldBeCalledOnce()
        {
            var registerUser = new RegisterUserCommandMessage("Jacob", "Jalali", "JJ@jBo.com");

            var user = new UserDbModel
            {
                Id = registerUser.Id,
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                Email = registerUser.Email
            };

            await _userCommandHandlerFixture.UserCommandHandler.HandelAsync(registerUser);
            _userCommandHandlerFixture.MockUserRepository.Verify(m => m.AddAsync(user), Moq.Times.Once);

            _userCommandHandlerFixture.MockEventAggregator.Verify(
                m => m.Publish(It.Is<UserCreatedEvent>(o=>o.Id.Equals(user.Id))), Moq.Times.Once);
        }

        [Fact]
        public async Task When_CallHandleAsyncMethodWithModifyUserCommandMessageNullParamter_Then_ArgumentNullExceptionShouldBeThrown()
        {
            ModifyUserCommandMessage registerUser = null;
            async Task act() => await _userCommandHandlerFixture.UserCommandHandler.HandelAsync(registerUser);
            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }

        [Fact]
        public async Task When_CallHandleAsyncMethodWithModifyUserCommandMessageParamter_Then_AddAsyncInUserRepositoryShouldBeCalledOnce()
        {

            var modifyUser = new ModifyUserCommandMessage(Guid.NewGuid(),"Jacob", "Jalali", "JJ@jBo.com");

            var user = new UserDbModel
            {
                Id = modifyUser.Id,
                FirstName = modifyUser.FirstName,
                LastName = modifyUser.LastName,
                Email = modifyUser.Email
            };

            await _userCommandHandlerFixture.UserCommandHandler.HandelAsync(modifyUser);
            _userCommandHandlerFixture.MockUserRepository.Verify(m => m.UpdateAsync(user), Moq.Times.Once);
        }
    }
}
