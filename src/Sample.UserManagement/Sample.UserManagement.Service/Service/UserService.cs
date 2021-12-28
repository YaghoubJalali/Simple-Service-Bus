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
using Sample.UserManagement.Service.Common;
using Sample.ServiceBus.Contract.QueryBus;

namespace Sample.UserManagement.Service.Service
{
    public class UserService : IUserService
    {
        private readonly ICommandBus _commandBus;

        private IQueryDispatcher _queryDispatcher { get; }

        public UserService(ICommandBus commandBus, IQueryDispatcher queryDispatcher)
        {
            _commandBus = commandBus ?? throw new ArgumentNullException($"{nameof(ICommandBus)} should not be null!");
            _queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
        }

        public async Task<Guid> RegisterUser(RegisterUser addUser)
        {
            if (addUser is null)
            {
                throw new ArgumentNullException(nameof(addUser));
            }

            var addUserCommand = new RegisterUserCommandMessage(addUser.FirstName, addUser.LastName, addUser.Email);
            await _commandBus.DispatchAsync(addUserCommand);
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
            await _commandBus.DispatchAsync(modifyUserCommand);
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            //add extra filter later
            GetUsersQuery usersFilter=new GetUsersQuery();
            var tempUsers = await _queryDispatcher.DispatchAsync<GetUsersQuery,GetUsersQueryResult>(usersFilter);
            return tempUsers?.Users??new List<User>();
        }

        

        public async Task<User> GetUserAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            GetUserQuery userFilter = new GetUserQuery { UserId = userId };
            var tempUsers = await _queryDispatcher.DispatchAsync<GetUserQuery, GetUserQueryResult>(userFilter);

            return tempUsers?.User;
        }
    }
}
