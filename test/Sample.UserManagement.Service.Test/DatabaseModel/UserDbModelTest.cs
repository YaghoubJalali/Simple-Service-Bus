using Sample.UserManagement.Service.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sample.UserManagement.Service.Test.DatabaseModel
{
    public class UserDbModelTest
    {
        [Fact]
        public void When_TryCompareEqualityOfUserDbModelWithNullValue_Then_FalseAsAResultShouldBeReturn()
        {
            var user = new UserDbModel()
            {
                Id = Guid.NewGuid()
            };

            var isEqual = user.Equals(null);
            Assert.False(isEqual);
        }

        [Theory]
        [ClassData(typeof(InvalidUserClassData))]
        public void When_TryCompareEqualityOfTwoUserDbModelWithDifferentProperty_Then_FalseAsAResultShouldBeReturn
            (UserDbModel firstUser,UserDbModel secondUser)
        {
            var isEqual = firstUser.Equals(secondUser);
            Assert.False(isEqual);
        }

        [Fact]
        public void When_TryCompareEqualityOfTwoUserDbModelWithSameParameter_Then_TrueAsAResultShouldBeReturn()
        {
            var userId = Guid.NewGuid();

            var firstUser = new UserDbModel()
            {
                Id = userId,
                FirstName = "Jacob",
                LastName = "Jalali",
                Email = ""
            };

            var secondUser = new UserDbModel()
            {
                Id = userId,
                FirstName = "Jacob",
                LastName = "Jalali",
                Email = ""
            };

            var isEqual = firstUser.Equals(secondUser);
            Assert.True(isEqual);
        }


        [Theory]
        [ClassData(typeof(InvalidUserClassData))]
        public void When_TryCompareHashcodeOfTwoUserDbModelWithDifferentProperty_Then_HashCodeShouldBeDifferent
           (UserDbModel firstUser, UserDbModel secondUser)
        {
            var firstUserHashcode = firstUser.GetHashCode();
            var seconduserHashCode = secondUser.GetHashCode();

            Assert.NotEqual(firstUserHashcode, seconduserHashCode);
        }

        [Fact]
        public void When_TryCompareHashCodeOfTwoUserDbModelWithSameParameter_Then_HashCodeShouldBeSame()
        {
            var userId = Guid.NewGuid();

            var firstUser = new UserDbModel()
            {
                Id = userId,
                FirstName = "Jacob",
                LastName = "Jalali",
                Email = ""
            };

            var secondUser = new UserDbModel()
            {
                Id = userId,
                FirstName = "Jacob",
                LastName = "Jalali",
                Email = ""
            };

            var firstUserHashcode = firstUser.GetHashCode();
            var seconduserUserHashCode = secondUser.GetHashCode();

            Assert.Equal(firstUserHashcode, seconduserUserHashCode);
        }


        [Fact]
        public void When_TwoUserDbModelIsEqual_Then_HashCodeShouldBeSame()
        {
            var userId = Guid.NewGuid();

            var firstUser = new UserDbModel()
            {
                Id = userId,
                FirstName = "Jacob",
                LastName = "Jalali",
                Email = ""
            };

            var secondUser = new UserDbModel()
            {
                Id = userId,
                FirstName = "Jacob",
                LastName = "Jalali",
                Email = ""
            };

            var isEqual = firstUser.Equals(secondUser);
            Assert.True(isEqual);

            var firstUserHashcode = firstUser.GetHashCode();
            var seconduserUserHashCode = secondUser.GetHashCode();

            Assert.Equal(firstUserHashcode, seconduserUserHashCode);
        }
    }
}
