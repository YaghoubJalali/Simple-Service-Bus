using Microsoft.Extensions.DependencyInjection;
using Sample.ServiceBus.Contract;
using Sample.ServiceBus.Handler;
using Sample.UserManagement.Service.Command.UserCommand;
using Sample.UserManagement.Service.Repository;
using Sample.UserManagement.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sample.UserManagement.Service.DbContexts;
using Sample.ServiceBus.Command;
using Sample.Framework.Common.ServiceProvider;
using Sample.UserManagement.Helpers;
using Sample.UserManagement.Service.Service.Contract;
using Sample.UserManagement.Service.Repository.Contract;
using Sample.UserManagement.Service.Event.Handler;
using Sample.UserManagement.Service.Event;

namespace Sample.UserManagement.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddConfig(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDbContext<UserManagementContext>(opt => opt.UseInMemoryDatabase("SimpleDataBase"));

            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();

            RegisterAllGenericeType(services, typeof(ICommandHandler<>), typeof(UserCommandHandler), ServiceLifetime.Scoped);
            RegisterAllGenericeType(services, typeof(IEventHandler<>), typeof(UserCreatedEventHandler), ServiceLifetime.Scoped);



            services.AddSingleton<ICommandBus, CommandBus>();
            services.AddSingleton<IEventAggregator, EventAggregator>();

            services.AddSingleton(services);

            services.AddSingleton<IServicesProvider, ServicesProvider>(
                srv => {
                    return new ServicesProvider(services.BuildServiceProvider());
                });

            //
            var provider = services.BuildServiceProvider();
            var eAggregator = (IEventAggregator)provider.GetService(typeof(IEventAggregator));
            SubscribeEventHandler(eAggregator);

            return services;
        }

        /// <summary>
        /// Register dynamically all implemented of generice type
        /// </summary>
        /// <param name="services"></param>
        /// <param name="interfaceType"></param>
        /// <param name="implementedClassType"></param>
        /// <param name="serviceLifetime"></param>
        public static void RegisterAllGenericeType(IServiceCollection services
            , Type interfaceType
            , Type implementedClassType
            , ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        {
            var implementedTypesOfInterface = ReflectionHelpers.GetTypeOfImplementedClassesFromInterface(interfaceType, implementedClassType);
            implementedTypesOfInterface.ForEach(assignedTypes =>
            {
                var implementedInterfacesType = assignedTypes.GetImplementInterfacesOfGenericType(interfaceType);
                implementedInterfacesType.ForEach(impInterface =>
                {
                    services.Add(new ServiceDescriptor(impInterface, assignedTypes, serviceLifetime));
                });
            });
        }
        
        public static List<Type> GetImplementInterfacesOfGenericType(this Type type, Type interfaceType)
        {
            var types = type.GetInterfaces().Where(o => o.IsGenericType && o.GetGenericTypeDefinition() == interfaceType).ToList();
            return types;
        }

        private static void SubscribeEventHandler(IEventAggregator eventAggregator)
        {
            eventAggregator.SubscribeEventHandler<UserCreatedEventHandler, UserCreatedEvent>();
        } 

    }
}
