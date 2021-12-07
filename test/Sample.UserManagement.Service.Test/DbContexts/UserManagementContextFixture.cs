using Microsoft.EntityFrameworkCore;
using Sample.UserManagement.Service.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.UserManagement.Service.Test.DbContexts
{
    public class UserManagementContextFixture : IDisposable
    {
        public UserManagementContext DbContext{ get; private set; }
        public UserManagementContextFixture()
        {
            this.DbContext = GetUserManagementContexttance();
        }

        private UserManagementContext GetUserManagementContexttance()
        {
            var contextOptions = new DbContextOptionsBuilder<UserManagementContext>()
                    .UseInMemoryDatabase("SimpleInMemoryDataBase").Options;

            var context = new UserManagementContext(contextOptions);
            return context;
        }

        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                }
            }
            this.disposed = true;
        }

    }
}
