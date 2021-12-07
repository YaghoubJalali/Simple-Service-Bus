using Sample.UserManagement.Service.DatabaseModel;
using Sample.UserManagement.Service.Repository;
using Sample.UserManagement.Service.Repository.Contract;
using Sample.UserManagement.Service.Service.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.UserManagement.Service.Service
{
    public class EmailService : IEmailService
    {
        private readonly IUserRepository _userRepository;

        public EmailService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<bool> SendWelcomeMailTo(Guid userGuid)
        {
            ValidateUserGuid(userGuid);

            var user = await _userRepository.GetUserAsync(userGuid);
            ValidateUser(user);

            string mailMessage = $"Welcome {user.FirstName} {user.LastName} :)";

            //send welcom mail to User
            return true;
        }

        private void ValidateUserGuid(Guid userGuid)
        {
            if (userGuid == Guid.Empty)
                throw new ArgumentException("User guid is invalid!");
        }

        private void ValidateUser(UserDbModel user)
        {
            if (user == null)
                throw new ArgumentNullException($"{nameof(UserDbModel)} is null!");
        }
    }
}
