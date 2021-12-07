using Microsoft.EntityFrameworkCore;
using Sample.UserManagement.Service.Command.UserCommand;
using Sample.UserManagement.Service.DatabaseModel;
using Sample.UserManagement.Service.DbContexts;
using Sample.UserManagement.Service.Model;
using Sample.UserManagement.Service.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.UserManagement.Service.Repository
{
    public class UserRepository : Repository<UserDbModel>, IUserRepository
    {
        public UserRepository(UserManagementContext dbContext)
            :base(dbContext)
        {
            
        }

        public async Task<UserDbModel> GetUserAsync(Guid userGuid)
        {
            var tempUser = await GetAll().FirstOrDefaultAsync(o => o.Id.Equals(userGuid));
            return tempUser;
        }

        public async Task<IEnumerable<UserDbModel>> GetUserAsync(int pageIndex, int pageSize)
        {
            pageIndex--;
            var users = await GetAll().Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();       
            return users;
        }
    }
}
