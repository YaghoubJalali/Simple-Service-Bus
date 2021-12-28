using Moq;
using Sample.ServiceBus.Contract;
using Sample.ServiceBus.Handler;
using Sample.UserManagement.Service.Command.UserCommand;
using Sample.UserManagement.Service.DatabaseModel;
using Sample.UserManagement.Service.Event;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Sample.ServiceBus.Test.Handler
{
    public class CommandBusTest:IClassFixture<CommandBusFixture>
    {
        private readonly CommandBusFixture _servicesProviderFixture;
        private readonly ICommandBus _commandBus;
        public CommandBusTest(CommandBusFixture servicesProviderFixture)
        {
            _servicesProviderFixture = servicesProviderFixture;
            _commandBus = servicesProviderFixture.CommandBus;
        }

        [Fact]
        public void When_TryToCreateCommandBusWithNullParameter_Then_ArgumentNullExceptionShouldBeThrown()
        {
            Action act = (
                () => new CommandBus(null)
               );
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public async Task When_TryToDispatchNullCommand_Then_ArgumentNullExceptionShouldBeThrown()
        {
            ICommandMessage command = null;
            async Task act() => await _commandBus.DispatchAsync(command);

            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }

        [Fact]
        public async Task When_CouldNotResolveService_Then_ArgumentNullExceptionShouldBeThrown()
        {
            var command = new NullCommandMessageForTest();
            async Task act() => await _commandBus.DispatchAsync(command);
            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }

        [Fact]
        public async Task When_CallDispatchMethodWithRegisterUserParameter_Then_UserCommandHandlerToAddUserShouldBeCalled()
        {
            RegisterUserCommandMessage command = new RegisterUserCommandMessage("Jacob", "Jalali", "JJ@Jbo.com");
            await _commandBus.DispatchAsync(command);
            _servicesProviderFixture.MockUserRepository.Verify(m =>
                        m.AddAsync(It.IsAny<UserDbModel>())
                        , Moq.Times.Once);
            _servicesProviderFixture.MockEventAggregator.Verify(m =>
                        m.PublishAsync<UserCreatedEvent>(It.IsAny<UserCreatedEvent>())
                        , Moq.Times.Once);
        }

        [Fact]
        public async Task When_CallDispatchMethodWithModifyUserParameter_Then_UserCommandHandlerToModifyUserShouldBeCalled()
        {
            var modifyCommand = new ModifyUserCommandMessage(Guid.NewGuid(),"Edited_Jacob", "Edited_Jalali", "Edited_JJ@Jbo.com");
            await _commandBus.DispatchAsync(modifyCommand);
            _servicesProviderFixture.MockUserRepository.Verify(m =>
                        m.UpdateAsync(It.IsAny<UserDbModel>())
                        , Moq.Times.Once);
        }
    }
}
