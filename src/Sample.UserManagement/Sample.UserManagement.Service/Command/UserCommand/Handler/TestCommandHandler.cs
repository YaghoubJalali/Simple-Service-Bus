using Sample.ServiceBus.Contract;
using Sample.UserManagement.Service.DatabaseModel;
using Sample.UserManagement.Service.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.UserManagement.Service.Command.UserCommand
{
    public class TestCommandHandler : ICommandHandler<TestCommandMessage>
    {
        public Task HandelAsync(TestCommandMessage command)
        {
            throw new NotImplementedException("This command handler implemented for Test!");
        }
    }
}
