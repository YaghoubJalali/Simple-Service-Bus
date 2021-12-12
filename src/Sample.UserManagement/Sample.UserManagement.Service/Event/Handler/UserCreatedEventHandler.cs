using Sample.ServiceBus.Contract;
using Sample.UserManagement.Service.Service.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.UserManagement.Service.Event.Handler
{
    public class UserCreatedEventHandler : IEventHandler<UserCreatedEvent>
    {
        private readonly IEmailService _emailService;
        public UserCreatedEventHandler(IEmailService emailService)
        {
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        public async Task Handle(UserCreatedEvent eventToHandle)
        {
            Validate(eventToHandle);
            await _emailService.SendWelcomeMailTo(eventToHandle.Id);
        }

        private void Validate(UserCreatedEvent eventToHandle)
        {
            if (eventToHandle == null)
                throw new ArgumentNullException(nameof(eventToHandle));
        }
    }
}
