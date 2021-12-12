using Moq;
using Sample.UserManagement.Service.Event.Handler;
using Sample.UserManagement.Service.Service.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sample.UserManagement.Service.Test.Event.Handler
{
    public class UserCreatedEventHandlerFixture
    {
        public Mock<IEmailService> MockEmailService { get; private set; }
        public UserCreatedEventHandler MockUserCreatedEventHandler { get;private set; }

        public UserCreatedEventHandlerFixture()
        {
            InitiateServices();
        }

        private void InitiateServices()
        {
            MockEmailService = new Mock<IEmailService>();
            MockUserCreatedEventHandler = new Mock<UserCreatedEventHandler>(MockEmailService.Object).Object;
        }
    }


}
