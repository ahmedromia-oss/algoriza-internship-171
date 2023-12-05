using Core.Domain;
using Core.DTOs.Auth;
using Core.DTOs.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IAuthService
    {
         
        public Task<bool> deleteUser(string userId);
        public Task<string> signIn(signInDto signInDto);
        public Task<User> signUp(signUpDto signUpDto);
        protected string getToken(List<Claim> authClaims);
        protected Task<List<Claim>> checkRoleToken(User user);

        public Task<bool> AddToRole(int Role , User user);

    }
}
