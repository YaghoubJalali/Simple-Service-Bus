using Microsoft.EntityFrameworkCore;
using Sample.UserManagement.Service.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sample.UserManagement.Service.Test.DbContexts
{
    public class UserManagementContextTest:IClassFixture<UserManagementContextFixture>
    {
        private readonly UserManagementContextFixture _userManagementContextFixture;
        public UserManagementContextTest(UserManagementContextFixture userManagementContextFixture)
        {
            _userManagementContextFixture = userManagementContextFixture;
        }

        [Fact]
        public void When_DbContextCreated_Then_DbSetOfUserShouldNotBeNull()
        {
            Assert.NotNull(_userManagementContextFixture.DbContext.Users);

        }
    }
}
