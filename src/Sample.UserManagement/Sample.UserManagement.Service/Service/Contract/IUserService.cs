using Sample.UserManagement.Service.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.UserManagement.Service.Service.Contract
{
    public interface IUserService: IService
    {
        Task<Guid> RegisterUser(RegisterUser addUser);
        Task ModifyUser(Guid userGuid, ModifyUser modifyUser);
        Task<IEnumerable<User>> GetAllUser();
        Task<User> GetUserAsync(Guid userId);
    }
}
