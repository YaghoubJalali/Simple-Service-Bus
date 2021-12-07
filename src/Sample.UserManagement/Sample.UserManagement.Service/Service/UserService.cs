using Sample.ServiceBus.Contract;
using Sample.UserManagement.Service.Command.UserCommand;
using Sample.UserManagement.Service.Model;
using Sample.UserManagement.Service.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Sample.UserManagement.Service.DatabaseModel;
using Sample.UserManagement.Service.Service.Contract;
using Sample.UserManagement.Service.Repository.Contract;

namespace Sample.UserManagement.Service.Service
{
    public class UserService : IUserService
    {
        private readonly ICommandBus _commandBus;

        private IUserRepository _userRepository { get; }

        public UserService(ICommandBus commandBus,IUserRepository userRepository)
        {
            _commandBus = commandBus ?? throw new ArgumentNullException($"{nameof(ICommandBus)} should not be null!");
            _userRepository = userRepository ?? throw new ArgumentNullException($"{nameof(IUserRepository)} should not be null!");
        }

        public async Task<Guid> RegisterUser(RegisterUser addUser)
        {
            if (addUser is null)
            {
                throw new ArgumentNullException(nameof(addUser));
            }

            var addUserCommand = new RegisterUserCommandMessage(addUser.FirstName, addUser.LastName, addUser.Email);
            await _commandBus.Dispatch(addUserCommand);
            return addUserCommand.Id;
        }

        public async Task ModifyUser(Guid userGuid, ModifyUser modifyUser)
        {
            if (userGuid == Guid.Empty)
                throw new ArgumentException();

            if (modifyUser is null)
            {
                throw new ArgumentNullException(nameof(modifyUser));
            }

            var modifyUserCommand = new ModifyUserCommandMessage
                (userGuid, modifyUser.FirstName, modifyUser.LastName, modifyUser.Email);
            await _commandBus.Dispatch(modifyUserCommand);
        }

        public IEnumerable<User> GetAllUser()
        {
            var tempUsers = _userRepository.GetAll().ToList();
            var users = tempUsers.ConvertAll(o => ConvertUserDbModelToUser(o)).ToList();
            return users;
        }

        private User ConvertUserDbModelToUser(UserDbModel model)
        {
            if (model == null)
                return null;

            return new User(model.Id,model.FirstName,model.LastName,model.Email);
        }

        public async Task<User> GetUserAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            var dbUser =await _userRepository.GetUserAsync(userId);
            var user = ConvertUserDbModelToUser(dbUser);

            return user;
        }
    }
}
