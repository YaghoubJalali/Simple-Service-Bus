using AutoMapper;
using Sample.UserManagement.DTO;
using Sample.UserManagement.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.UserManagement.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserDto, RegisterUser>();
            CreateMap<ModifyUserDto, ModifyUser>();
        }
    }
}
