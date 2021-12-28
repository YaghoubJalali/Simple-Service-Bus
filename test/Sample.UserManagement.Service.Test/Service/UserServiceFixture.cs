using Moq;
using Sample.ServiceBus.Contract;
using Sample.ServiceBus.Contract.QueryBus;
using Sample.UserManagement.Service.Repository;
using Sample.UserManagement.Service.Repository.Contract;
using Sample.UserManagement.Service.Service;
using Sample.UserManagement.Service.Service.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.UserManagement.Service.Test.Service
{
    public class UserServiceFixture
    {
        public IUserService UserService { get; private set; }
        public Mock<ICommandBus> MockCommandBus { get; private set; }
        public Mock<IQueryDispatcher> MockQueryDispatcher { get; private set; }
        public UserServiceFixture()
        {
            InitiateService();
        }

        private void InitiateService()
        {
            MockCommandBus = new Mock<ICommandBus>();
            MockQueryDispatcher = new Mock<IQueryDispatcher>();
            UserService = new UserService(MockCommandBus.Object, MockQueryDispatcher.Object);
        }

        
    }
}
