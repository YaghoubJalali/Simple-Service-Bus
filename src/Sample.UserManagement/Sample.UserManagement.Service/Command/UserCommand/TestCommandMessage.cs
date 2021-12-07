using Sample.ServiceBus.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.UserManagement.Service.Command.UserCommand
{
    public class TestCommandMessage : CommandMessage
    {
        public TestCommandMessage()
        {
            throw new NotImplementedException("This command message just created for dependency injection  test!");
        }
    }
}
