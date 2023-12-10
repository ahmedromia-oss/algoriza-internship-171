using AutoMapper;
using Core.Domain;
using Core.DTOs.Auth;
using Core.DTOs.User;
using Core.Models;
using Core.ReboInterfaces;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : Repository<User, GetUserDto>, IUserRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;
        private readonly IFileOperation fileOperation;

        public UserRepository(AppDbContext appDbContext , IMapper mapper , IFileOperation fileOperation):base(appDbContext , mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
            this.fileOperation = fileOperation;
        }
        public async Task<User> addUser(signUpDto signUpDto , int userType)
        {
            if (!(await this.appDbContext.users.Where(e => e.Email == signUpDto.Email).AnyAsync()))
            {


                User user = mapper.Map<User>(signUpDto);
                if (signUpDto.Image != null)
                {
                    string uniqueFileName = this.fileOperation.AddFile(signUpDto.Image, "lib/Images");
                    user.ImageLink = uniqueFileName;
                }
                user.UserType = userType;
                return user;
            }
            throw new Exception("Email Already Taken");
        }

        public async Task<bool> updateUser(updateUserDto updateUserDto , string id)
        {
            User user = await this.appDbContext.users.FindAsync(id);
            user = mapper.Map<updateUserDto , User>(updateUserDto , user);
            if (updateUserDto.Image != null)
            {
                string uniqueFileName = this.fileOperation.AddFile(updateUserDto.Image, "lib/Images");
                user.ImageLink = uniqueFileName;
            }
            this.appDbContext.Users.Update(user);
            return true;
        }

    }
}
