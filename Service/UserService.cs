using Core.DTOs.User;
using Core.Interfaces;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService : IUserService
    {
        
        private readonly IUnitOfWork unitOfWork;
        private readonly IAuthService authService;

        public UserService(IUnitOfWork unitOfWork , IAuthService authService)
        {
            this.unitOfWork = unitOfWork;
            this.authService = authService;
        }

       

        public async Task<bool> updateUser(updateUserDto updateUserDto, string id)
        {
            await this.unitOfWork.users.updateUser(updateUserDto, id);
            return await this.unitOfWork.complete();
        }

        public async Task<bool> deleteUser(string id)
        {
            return await this.authService.deleteUser(id);
        }
       
    }
}
