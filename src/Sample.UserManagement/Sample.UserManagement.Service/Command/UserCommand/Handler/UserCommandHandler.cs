using Sample.ServiceBus.Contract;
using Sample.UserManagement.Service.DatabaseModel;
using Sample.UserManagement.Service.Event;
using Sample.UserManagement.Service.Repository;
using Sample.UserManagement.Service.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.UserManagement.Service.Command.UserCommand
{
    public class UserCommandHandler : ICommandHandler<RegisterUserCommandMessage>
                                ,ICommandHandler<ModifyUserCommandMessage>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventAggregator _eventAggregator;
        public UserCommandHandler(IUserRepository userRepository, IEventAggregator eventAggregator)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
        }

        public async Task HandelAsync(RegisterUserCommandMessage addUser)
        {
            if (addUser is null)
            {
                throw new ArgumentNullException(nameof(addUser));
            }

            var user = new UserDbModel
            {
                Id = addUser.Id,
                FirstName = addUser.FirstName,
                LastName = addUser.LastName,
                Email = addUser.Email
            };

            await _userRepository.AddAsync(user);
            await _eventAggregator.Publish(new UserCreatedEvent(user.Id));
        }

        public async Task HandelAsync(ModifyUserCommandMessage modifyUser)
        {
            if (modifyUser is null)
            {
                throw new ArgumentNullException(nameof(modifyUser));
            }

            var user = new UserDbModel
            {
                Id = modifyUser.Id,
                FirstName = modifyUser.FirstName,
                LastName = modifyUser.LastName,
                Email = modifyUser.Email
            };
            await _userRepository.UpdateAsync(user);
        }
    }
}
