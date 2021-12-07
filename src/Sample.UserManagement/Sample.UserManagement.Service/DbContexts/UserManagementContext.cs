using Microsoft.EntityFrameworkCore;
using Sample.UserManagement.Service.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.UserManagement.Service.DbContexts
{
    public class UserManagementContext : DbContext
    {
        public UserManagementContext(DbContextOptions<UserManagementContext> options)
            : base(options)
        {
            
        }
        public DbSet<UserDbModel> Users { get; set; }

    }
}
