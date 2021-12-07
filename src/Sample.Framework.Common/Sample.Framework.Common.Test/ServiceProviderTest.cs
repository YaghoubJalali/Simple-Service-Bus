using Microsoft.Extensions.DependencyInjection;
using Sample.Framework.Common.ServiceProvider;
using Sample.ServiceBus.Contract;
using Sample.UserManagement.Configuration;
using Sample.UserManagement.Helpers;
using Sample.UserManagement.Service.Command.UserCommand;
using System;
using Xunit;
using System.Linq;
using System.Reflection;
using Sample.UserManagement.Service.Repository;
using Sample.UserManagement.Service.Service;
using Sample.UserManagement.Service.Service.Contract;
using Sample.UserManagement.Service.Repository.Contract;

namespace Sample.Framework.Common.Test
{
    public class ServiceProviderTest : IClassFixture<ServiceCollection>
    {
        private readonly IServiceCollection _services;
        private readonly IServicesProvider _servicesProvider;

        public ServiceProviderTest(ServiceCollection services)
        {
            _services = services;
            _services.AddConfig();
            _servicesProvider = new ServicesProvider(_services.BuildServiceProvider());
        }

        [Theory]
        [InlineData(typeof(IUserService))]
        [InlineData(typeof(IEmailService))]
        [InlineData(typeof(ICommandBus))]
        [InlineData(typeof(IUserRepository))]
        [InlineData(typeof(ICommandHandler<RegisterUserCommandMessage>))]
        public void When_RequestForService_Then_RelatedInstanceOfServiceShouldBeReturn(Type interfaceType)
        {
            MethodInfo method = _servicesProvider.GetType().GetMethod("GetService");
            var genericMethod = method.MakeGenericMethod(interfaceType);
            var service = genericMethod.Invoke(_servicesProvider, null);
            Assert.NotNull(service);
        }

        [Fact]
        public void When_TryToCreateServiceProviderWithNullCollection_Then_ExceptionShouldBeThrown()
        {
            Action act = ()=> {new ServicesProvider(null);};
            Assert.Throws<ArgumentNullException>(act);
        }

    }
}
