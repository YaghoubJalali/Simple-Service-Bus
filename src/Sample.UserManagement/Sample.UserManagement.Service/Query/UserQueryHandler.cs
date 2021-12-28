using Sample.ServiceBus.Contract.QueryBus;
using Sample.UserManagement.Service.Common;
using Sample.UserManagement.Service.Model;
using Sample.UserManagement.Service.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.UserManagement.Service.Query
{
    public class UserQueryHandler : IQueryHandler<GetUserQuery, GetUserQueryResult>
                                , IQueryHandler<GetUsersQuery, GetUsersQueryResult>
    {
        private readonly IUserRepository _userRepository;

        public UserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUsersQueryResult> GetQueryAsync(GetUsersQuery query)
        {
            var users = GetAllUser();
            return new GetUsersQueryResult { Users = users };

        }

        public async Task<GetUserQueryResult> GetQueryAsync(GetUserQuery query)
        {
            if (query == null || query.UserId == Guid.Empty)
                throw new ArgumentException(nameof(query));

            var dbUser = await _userRepository.GetUserAsync(query.UserId);
            var user = Convertor.ConvertUserDbModelToUser(dbUser);

            return new GetUserQueryResult {  User = user };

        }

        private List<User> GetAllUser()
        {
            var tempUsers = _userRepository.GetAll().ToList();
            var users = tempUsers.ConvertAll(o => Convertor.ConvertUserDbModelToUser(o)).ToList();
            return users;
        }

        private async Task<User> GetUserAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            var dbUser = await _userRepository.GetUserAsync(userId);
            var user = Convertor.ConvertUserDbModelToUser(dbUser);

            return user;
        }

    }
}
