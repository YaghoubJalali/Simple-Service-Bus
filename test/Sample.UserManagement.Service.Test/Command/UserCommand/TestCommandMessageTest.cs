using Sample.UserManagement.Service.Command.UserCommand;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sample.UserManagement.Service.Test.Command.UserCommand
{
    public class TestCommandMessageTest
    {
        [Fact]
        public void When_TryToInstanciateTestCommandMessage_Then_NotImplementedExceptionShouldBeThrown()
        {
            Action act = (()=> {
                new TestCommandMessage();
            });
            Assert.Throws<NotImplementedException>(act);
        }
    }
}
