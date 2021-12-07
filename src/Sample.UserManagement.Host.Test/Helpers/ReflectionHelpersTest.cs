using Sample.UserManagement.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using Sample.ServiceBus.Contract;
using Sample.UserManagement.Service.Command.UserCommand;

namespace Sample.UserManagement.Host.Test.Helpers
{
    public class ReflectionHelpersTest
    {
        [Theory]
        [InlineData(typeof(IEnumerable<>), typeof(List<>))]
        [InlineData(typeof(ICommandHandler<>), typeof(UserCommandHandler))]
        public void When_RequestedToGetTypeOfGenericImplementedClasses_Then_AllReturnedTypesShouldBeImplementedGenericType
            (Type interfaceType,Type onOfImplementedClassType)
        {

            var implementedTypes = ReflectionHelpers.GetTypeOfImplementedClassesFromInterface(interfaceType, onOfImplementedClassType);

            var interfaceFullName = interfaceType.GetInterfaces()[0].FullName;
            implementedTypes.ToList().ForEach(type =>
            {
                var implemented = type.GetInterface(interfaceFullName);
                Assert.NotNull(implemented);
            });
        }
    }
}
