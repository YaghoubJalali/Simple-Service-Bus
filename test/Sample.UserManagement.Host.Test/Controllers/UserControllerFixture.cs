using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Sample.Framework.Common.ServiceProvider;
using Sample.ServiceBus.Contract;
using Sample.UserManagement.Configuration;
using Sample.UserManagement.Controllers;
using Sample.UserManagement.Service.Repository;
using Sample.UserManagement.Service.Service;
using Sample.UserManagement.Service.Service.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.UserManagement.Host.Test.Controllers
{
    public class UserControllerFixture
    {
        public IServiceCollection Services { get; private set; }
        public IServicesProvider ServicesProvider { get; private set; }
        public UserController UserController { get; private set; }
        public UserController MockUserController { get; private set; }

        public IUserService UserService { get; private set; }
        public Mock<IUserService> MockUserService { get; private set; }

        public IMapper Mapper { get; private set; }

        public UserControllerFixture()
        {
            Initiate();
        }

        private void Initiate()
        {
            Services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();

            Services.AddConfig();
            ServicesProvider = new ServicesProvider(Services.BuildServiceProvider());

            UserService = ServicesProvider.GetService<IUserService>();
            Mapper = ServicesProvider.GetService<IMapper>();
            UserController = new UserController(UserService, Mapper);

            MockUserService = new Mock<IUserService>();
            var mockMapper = new Mock<IMapper>();
            MockUserController = new Mock<UserController>(MockUserService.Object,mockMapper.Object).Object;
        }
    }
}
