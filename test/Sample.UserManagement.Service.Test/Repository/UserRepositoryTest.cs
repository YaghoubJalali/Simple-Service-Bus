using Microsoft.EntityFrameworkCore;
using Sample.UserManagement.Service.DatabaseModel;
using Sample.UserManagement.Service.DbContexts;
using Sample.UserManagement.Service.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using Sample.UserManagement.Service.Repository.Contract;

namespace Sample.UserManagement.Service.Test.Repository
{
    public class UserRepositoryTest : IClassFixture<RepositoryFixture>
                                        ,IClassFixture<MockRepositoryFixture>
    {
        private readonly IUserRepository _userRepository;
        private readonly RepositoryFixture _repositoryFixture;
        private readonly MockRepositoryFixture _mockRepositoryFixture;

        public UserRepositoryTest(RepositoryFixture repositoryFixture,MockRepositoryFixture mockRepositoryFixture)
        {
            _repositoryFixture = repositoryFixture; 
            _userRepository = repositoryFixture.UserRepository; 
            _mockRepositoryFixture = mockRepositoryFixture;
        }
        
        [Fact]
        public void When_TryToCreateUerRepositoryWithInvalidDBContext_Then_ArgumentNullExceptionShouldBeThrown()
        {
            Action act = (() => new UserRepository(null));
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public async Task When_CallAddUserAsyncMethodWithNullParameter_Then_ExceptionShouldBeThrown()
        {
            async Task act() => await _userRepository.AddAsync(null);
            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }

        [Fact]
        public async Task When_CallAddUserMethod_Then_UserShouldBeInserted()
        {
            var userGuid = Guid.NewGuid();
            var userToAdd = new UserDbModel()
            {
                Id = userGuid,
                FirstName = "Jacob",
                LastName = "Jalali",
                Email = "Test@Gmail.com"
            };

            var insertedUser = await _userRepository.AddAsync(userToAdd);
            Assert.NotNull(insertedUser);
            Assert.True(userToAdd.Equals(insertedUser));

            var retrievedUser = await _userRepository.GetUserAsync(userGuid);
            Assert.NotNull(retrievedUser);
            Assert.True(userToAdd.Equals(retrievedUser));
        }

        [Fact]
        public async Task When_InAddUserMethodErrorOccured_Then_ExceptionShouldBeThrown()
        {
            var userGuid = Guid.NewGuid();
            var validUser = new UserDbModel()
            {
                Id = userGuid,
                FirstName = "Jacob",
                LastName = "Jalali",
                Email = "Test@Gmail.com"
            };
            
            

            async Task act() => await _mockRepositoryFixture.UserRepository.AddAsync(validUser);

            await Assert.ThrowsAsync<Exception>(act);
        }

        [Fact]
        public async Task When_CallUpdateUserAsyncMethodWithNullParameter_Then_ExceptionShouldBeThrown()
        {
            async Task act() => await _userRepository.UpdateAsync(null);
            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }

        [Fact]
        public async Task When_CallUpdateUserMethod_Then_UserInformationShouldBeUpdated()
        {
            var userGuid = Guid.NewGuid();
            var userToAdd = new UserDbModel()
            {
                Id = userGuid,
                FirstName = "Jacob",
                LastName = "Jalali",
                Email = "Test@Gmail.com"
            };

            await _userRepository.AddAsync(userToAdd);

            var userToUpdate = new UserDbModel()
            {
                Id = userGuid,
                FirstName = "UpdateFirstName",
                LastName = "UpdateLastName",
                Email = "UpdateEmail"
            };

            var updatedUser = await _userRepository.UpdateAsync(userToUpdate);
            Assert.NotNull(updatedUser);
            Assert.True(userToUpdate.Equals(updatedUser));

            var retrievedUser = await _userRepository.GetUserAsync(userGuid);
            Assert.NotNull(retrievedUser);
            Assert.True(userToUpdate.Equals(retrievedUser));
        }

        [Fact]
        public async Task When_InUpdateUserMethodErrorOccured_Then_ExceptionShouldBeThrown()
        {
            var userGuid = Guid.NewGuid();
            var validUser = new UserDbModel()
            {
                Id = userGuid,
                FirstName = "Jacob",
                LastName = "Jalali",
                Email = "Test@Gmail.com"
            };

            async Task act() => await _mockRepositoryFixture.UserRepository.UpdateAsync(validUser);
            //Could check for any type of exceptions
            await Assert.ThrowsAsync<Exception>(act);
        }

        [Fact]
        public async Task When_GetUserWithGuid_Then_ReturnNullIfUserDoesNotExist()
        {
            var user = await _userRepository.GetUserAsync(Guid.NewGuid());
            Assert.Null(user);
        }

        [Fact]
        public async Task When_GetExistUserWithGuid_Then_CorrectUserShouldBeReturned()
        {

            var userGuid = Guid.NewGuid();
            var userToAdd = new UserDbModel()
            {
                Id = userGuid,
                FirstName = "Jacob",
                LastName = "Jalali",
                Email = "Test@Gmail.com"
            };

            await _userRepository.AddAsync(userToAdd);

            var retrieveUser = await _userRepository.GetUserAsync(userGuid);
            Assert.NotNull(retrieveUser);
            Assert.IsAssignableFrom<UserDbModel>(retrieveUser);
            Assert.True(userToAdd.Equals(retrieveUser));
        }

        [Fact]
        public async Task When_GetUserWithPaginationInfo_Then_EmptyListOrListOfUserModelShouldBeReturned()
        {
            var users = await _userRepository.GetUserAsync(1, 8);
            Assert.NotNull(users);
            Assert.IsType<List<UserDbModel>>(users);
        }

        [Fact]
        public void When_CallGetAllMethod_Then_IQueryableResultShouldBeReturned()
        {
            var users = _userRepository.GetAll();
            Assert.IsAssignableFrom<IQueryable<UserDbModel>>(users);
        }

        [Fact]
        public void When_DisposeMethodOfUserRepositoryCalled_Then_ContextShouldBeDisposed()
        {
            var isDisposed = _repositoryFixture.DisposableUserRepository.Disposed;
            Assert.False(isDisposed);

            _repositoryFixture.DisposableUserRepository.Dispose();

            Assert.Throws<ObjectDisposedException>(() => _repositoryFixture.DisposableUserRepository.Disposed);
        }
    }
}
