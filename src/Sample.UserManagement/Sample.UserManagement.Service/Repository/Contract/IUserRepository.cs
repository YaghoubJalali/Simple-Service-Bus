using Sample.UserManagement.Service.Command.UserCommand;
using Sample.UserManagement.Service.DatabaseModel;
using Sample.UserManagement.Service.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.UserManagement.Service.Repository.Contract
{
    public interface IUserRepository : IRepository<UserDbModel>
    {
        Task<UserDbModel> GetUserAsync(Guid userGuid);
        Task<IEnumerable<UserDbModel>> GetUserAsync(int pageIndex,int pageSize);
    }
}
