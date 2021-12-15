using Moq;
using Sample.Framework.Common.ServiceProvider;
using Sample.ServiceBus.Contract;
using Sample.ServiceBus.Contract.EventBus;
using Sample.ServiceBus.Handler;
using Sample.ServiceBus.Test.Handler.ExtraTemplateClassForTest;
using Sample.UserManagement.Service.Event;
using Sample.UserManagement.Service.Event.Handler;
using Sample.UserManagement.Service.Service.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace Sample.ServiceBus.Test.Handler
{
    public class ActionEventHandlerFixture
    {
        public IEventAggregator EventAggregator { get; private set; }
        public Mock<IServicesProvider> MockServiceProvider { get; private set; }
        
        public bool IsCalled { get; set; }

        public ActionEventHandlerFixture()
        {
            InitiateServices();
        }

        private void InitiateServices()
        {
            MockServiceProvider = new Mock<IServicesProvider>();
            EventAggregator = new EventAggregator(MockServiceProvider.Object);
        }
    }
}
