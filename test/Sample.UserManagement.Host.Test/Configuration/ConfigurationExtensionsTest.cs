using Microsoft.Extensions.DependencyInjection;
using Sample.Framework.Common.ServiceProvider;
using Sample.ServiceBus.Command;
using Sample.ServiceBus.Contract;
using Sample.UserManagement.Configuration;
using Sample.UserManagement.Helpers;
using Sample.UserManagement.Service.Command.UserCommand;
using Sample.UserManagement.Service.Event.Handler;
using Sample.UserManagement.Service.Repository;
using Sample.UserManagement.Service.Repository.Contract;
using Sample.UserManagement.Service.Service;
using Sample.UserManagement.Service.Service.Contract;
using System;
using System.Linq;
using Xunit;

namespace Sample.UserManagement.Host.Test.Configuration
{
    public class ConfigurationExtensionsTest
    {

        [Theory]
        [InlineData(typeof(IService))]
        [InlineData(typeof(IUserRepository))]
        [InlineData(typeof(ICommandBus))]
        //[InlineData(typeof(IServicesProvider))]
        public void When_RegisteredServicesWithInterfaceInServiceCollection_Then_ServiceShouldBeRetrieveWithImplementedClassFromServiceCollection(Type interfaceType)
        {
            IServiceCollection services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            services.AddConfig();

            var implementedClassType = System.Reflection.Assembly.GetAssembly(interfaceType)
            .GetTypes().Where(o=>o.IsClass)
            .Where(o => o.GetInterfaces()
                        .Any(o => !String.IsNullOrWhiteSpace(o.FullName) 
                               && o.FullName.Equals(interfaceType.FullName))).ToList();

            foreach (var cl in implementedClassType)
            {
                var relatedService = services.FirstOrDefault(o => o.ImplementationType != null && o.ImplementationType.FullName.Equals(cl.FullName));
                Assert.NotNull(relatedService);
            }
        }

        [Theory]
        [InlineData(typeof(ICommandHandler<>),typeof(UserCommandHandler))]
        [InlineData(typeof(IEventHandler<>),typeof(UserCreatedEventHandler))]
        public void When_RegisteredGenericeTypesInServiceCollection_Then_AllChildShouldBeExistInCollection
            (Type interfaceType,Type onOfImplementedClassType)
        {
            IServiceCollection services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();

            var implementedTypes = ReflectionHelpers.GetTypeOfImplementedClassesFromInterface(interfaceType, onOfImplementedClassType);

            Assert.True(implementedTypes.Count > 0, $"Can't find implemented class From {interfaceType}!");

            ConfigurationExtensions.RegisterAllGenericeType(services, interfaceType, onOfImplementedClassType, ServiceLifetime.Scoped);

            implementedTypes.ForEach(assignedTypes =>
            {
                var implementedInterfacesType = assignedTypes.GetImplementInterfacesOfGenericType(interfaceType);
                implementedInterfacesType.ForEach(impInterface =>
                {
                    var relatedService = services.FirstOrDefault(o => o.ServiceType.FullName.Equals(impInterface.FullName));

                    Assert.NotNull(relatedService);
                });
            });
        }


    }
}
