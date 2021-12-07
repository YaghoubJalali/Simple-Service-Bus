using Sample.UserManagement.Service.Command.UserCommand;
using Sample.UserManagement.Service.DatabaseModel;
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

        [Fact]
        public void When_TryToInstantiateUserCommandHandlerWithNullParameter_Then_ArgumentNullExceptionShouldBeThrown()
        {
            Assert.Throws<ArgumentNullException>((Action)(() => new UserCommandHandler(null)));
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
