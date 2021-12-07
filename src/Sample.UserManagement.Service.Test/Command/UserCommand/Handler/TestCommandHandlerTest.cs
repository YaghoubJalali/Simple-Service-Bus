using Sample.UserManagement.Service.Command.UserCommand;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sample.UserManagement.Service.Test.Command.UserCommand.Handler
{
    public class TestCommandHandlerTest
    {
        [Fact]
        public async Task When_CallHandleCommandMethodInTestCommandHandler_Then_NotImplementedExceptionShouldBeThrown()
        {
            var commandHandler = new TestCommandHandler();
            TestCommandMessage command = null;

            async Task act() => await commandHandler.HandelAsync(command);
            await Assert.ThrowsAsync<NotImplementedException>(act);
        }
    }
}
