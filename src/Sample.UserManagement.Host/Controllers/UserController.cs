using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sample.UserManagement.DTO;
using Sample.UserManagement.Service.Model;
using Sample.UserManagement.Service.Service;
using Sample.UserManagement.Service.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.UserManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Register New User
        /// </summary>
        /// <param name="addUser"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Guid> RegisterUser(RegisterUserDto addUser)
        {
            if (addUser is null)
            {
                throw new ArgumentNullException(nameof(addUser));
            }

            var userToCreate = _mapper.Map<RegisterUser>(addUser);
            var userId = await _userService.RegisterUser(userToCreate);
            return userId;
        }

        /// <summary>
        /// Update User Info
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="modifyUser"></param>
        /// <returns></returns>
        [HttpPut("{userId}")]
        public async Task ModifyUser(Guid userId, ModifyUserDto modifyUser)
        {
            if (Guid.Empty == userId)
                throw new ArgumentException(nameof(User));

            if (modifyUser is null)
            {
                throw new ArgumentNullException(nameof(modifyUser));
            }

            var userToModify= _mapper.Map<ModifyUser>(modifyUser);
            await _userService.ModifyUser(userId, userToModify);
        }

        /// <summary>
        /// Get All Users For Test
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _userService.GetAllUser();
            return users;
        }


        [HttpGet("{userId}")]
        public async Task<User> GetUsers(Guid userId)
        {
            var user = await _userService.GetUserAsync(userId);
            return user;
        }
    }
}
