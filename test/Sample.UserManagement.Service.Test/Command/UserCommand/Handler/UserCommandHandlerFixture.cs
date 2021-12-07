using Moq;
using Sample.UserManagement.Service.Command.UserCommand;
using Sample.UserManagement.Service.Repository;
using Sample.UserManagement.Service.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.UserManagement.Service.Test.Command.UserCommand.Handler
{
    public class UserCommandHandlerFixture
    {
        public Mock<IUserRepository> MockUserRepository { get; private set; }
        public UserCommandHandler UserCommandHandler { get; set; }
        public UserCommandHandlerFixture()
        {
            InitiateServices();
        }


        private void InitiateServices()
        {
            MockUserRepository = new Mock<IUserRepository>();
            UserCommandHandler = new UserCommandHandler(MockUserRepository.Object);
        }
    }
}
