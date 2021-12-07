using Microsoft.EntityFrameworkCore;
using Sample.UserManagement.Service.DatabaseModel;
using Sample.UserManagement.Service.DbContexts;
using Sample.UserManagement.Service.Repository;
using Sample.UserManagement.Service.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Sample.UserManagement.Service.Test.Repository
{
    public class RepositoryFixture : IDisposable
    {
        public IUserRepository UserRepository { get; private set; }
        public UserRepository DisposableUserRepository { get; private set; }
        private UserManagementContext _dbContext;
        private const int _seedDataCount = 30;
        public RepositoryFixture()
        {
            UserRepository = GetIUerRepositoryInstance();
            DisposableUserRepository = GetUerRepositoryInstance();
            IngestTestSeedData(); 
        }

        private IUserRepository GetIUerRepositoryInstance()
        {
            var contextOptions = new DbContextOptionsBuilder<UserManagementContext>()
                    .UseInMemoryDatabase("SimpleInMemoryDataBase").Options;

            _dbContext = new UserManagementContext(contextOptions);
            var userRepository = new UserRepository(_dbContext); 
            return userRepository; 
        }

        private UserRepository GetUerRepositoryInstance()
        {
            var contextOptions = new DbContextOptionsBuilder<UserManagementContext>()
                    .UseInMemoryDatabase("SimpleInMemoryDataBase").Options;

            _dbContext = new UserManagementContext(contextOptions);
            var userRepository = new UserRepository(_dbContext);
            return userRepository;
        }

        private void IngestTestSeedData()
        {
            for (int i = 0; i < _seedDataCount; i++)
            {
                var randomString = GetRandomString(5);
                _dbContext.Users.Add(new UserDbModel
                {
                    Id = Guid.NewGuid(),
                    FirstName = $"FirstName_{randomString}",
                    LastName = $"LastName_{randomString}",
                    Email = $"Email_{randomString}",
                });
                
            }
            _dbContext.SaveChanges();
        }

        private string GetRandomString(int length)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bit_count = (length * 6);
                var byte_count = ((bit_count + 7) / 8);
                var bytes = new byte[byte_count];
                rng.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }

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
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }


    }
}
