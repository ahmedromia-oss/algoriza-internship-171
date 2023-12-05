using Core.DTOs.Doctor;
using Core.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IUserService
    {
        public Task<bool> updateUser(updateUserDto updateUserDto, string id);
        public Task<bool> deleteUser(string id);
    }
}
