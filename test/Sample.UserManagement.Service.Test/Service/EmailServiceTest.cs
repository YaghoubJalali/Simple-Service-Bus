using Moq;
using Sample.UserManagement.Service.DatabaseModel;
using Sample.UserManagement.Service.Repository;
using Sample.UserManagement.Service.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sample.UserManagement.Service.Test.Service
{
    public class EmailServiceTest : IClassFixture<ServiceMockFixture>
    {
        private readonly ServiceMockFixture _serviceMockFixture;
        public EmailServiceTest(ServiceMockFixture serviceMockFixture)
        {
            _serviceMockFixture = serviceMockFixture;
        }

        [Fact]
        public void When_TryToInstantiateEmailServiceWithNullParameter_Then_ArgumentNullExceptionShouldBeThrown()
        {

            Assert.Throws<ArgumentNullException>((Action)(() => new EmailService(null)));
        }

        [Fact]
        public async Task When_CallSendWelcomeMailToWithInvalidUserId_Then_ArgumentExceptionShouldBeThrown()
        {
            async Task func() => await _serviceMockFixture.EmailService.SendWelcomeMailTo(Guid.Empty);
            await Assert.ThrowsAsync<ArgumentException>(func);
        }

        [Fact]
        public async Task When_CallSendWelcomeMailToWithInvalidUserThatDoesNotExists_Then_ArgumentNullExceptionShouldBeThrown()
        {
            async Task func() => await _serviceMockFixture.EmailService.SendWelcomeMailTo(Guid.NewGuid());
            await Assert.ThrowsAsync<ArgumentNullException>(func);
        }

        [Fact]
        public async Task When_CallSendWelcomeMailToWithValidParameterAndEveryThingWorkWell_Then_TrueResultShouldBeReturned()
        {
            var tempUserId = Guid.NewGuid();

            _serviceMockFixture.MockUserRepository.Setup(x => x.GetUserAsync(tempUserId)).ReturnsAsync(
               () =>
               {
                   return new UserDbModel
                   {
                       Id = tempUserId,
                       FirstName = "Jacob",
                       LastName = "Jalali",
                       Email = "JJ@ybo.com"
                   };
               });
            
            var sendMailStatus = await _serviceMockFixture.EmailService.SendWelcomeMailTo(tempUserId);
            Assert.True(sendMailStatus);
        }
    }
}
