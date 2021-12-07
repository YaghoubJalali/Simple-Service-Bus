using Microsoft.EntityFrameworkCore;
using Moq;
using Sample.UserManagement.Service.DbContexts;
using Sample.UserManagement.Service.Repository;
using Sample.UserManagement.Service.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.UserManagement.Service.Test.Repository
{
    public class MockRepositoryFixture
    {
        public IUserRepository UserRepository { get; private set; }
        public Mock<UserManagementContext> MockDbContext;
        private const int _seedDataCount = 30;
        public MockRepositoryFixture()
        {
            UserRepository = GetUerRepositoryInstance();
        }

        private IUserRepository GetUerRepositoryInstance()
        {
            //declared valid contextOptions to avoid construct exception 
            var contextOptions = new DbContextOptionsBuilder<UserManagementContext>()
                    .UseInMemoryDatabase("SimpleInMemoryDataBase").Options;

            MockDbContext = new Mock<UserManagementContext>(contextOptions);

            var userRepository = new UserRepository(MockDbContext.Object);
            return userRepository;
        }


    }
}
