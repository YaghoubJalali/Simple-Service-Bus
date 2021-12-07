using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.UserManagement.Service.Service.Contract
{
    public interface IEmailService: IService
    {
        Task<bool> SendWelcomeMailTo(Guid userGuid);
    }
}
