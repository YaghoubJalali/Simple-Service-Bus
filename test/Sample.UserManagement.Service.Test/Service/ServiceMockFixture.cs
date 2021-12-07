using Moq;
using Sample.UserManagement.Service.Repository;
using Sample.UserManagement.Service.Repository.Contract;
using Sample.UserManagement.Service.Service;
using Sample.UserManagement.Service.Service.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.UserManagement.Service.Test.Service
{
    public class ServiceMockFixture
    {
        public Mock<IUserRepository> MockUserRepository { get; private set; }
        public IEmailService EmailService { get; private set; }
        public ServiceMockFixture()
        {
            InitiateServices();
        }

        private void InitiateServices()
        {
            MockUserRepository = new Mock<IUserRepository>();
            EmailService = new EmailService(MockUserRepository.Object);
        }
    }
}
