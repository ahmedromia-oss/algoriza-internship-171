using Core.Domain;
using Core.DTOs.Auth;
using Core.DTOs.User;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ReboInterfaces
{
    public interface IUserRepository:IRepository<User , GetUserDto>
    {
        public Task<User> addUser(signUpDto signUpDto);

        public Task<bool> updateUser(updateUserDto updateUserDto , string id);

    }
}
