using AutoMapper;
using Core.Domain;
using Core.DTOs.Auth;
using Core.DTOs.User;
using Core.Interfaces;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public AuthService(IConfiguration configuration , UserManager<User> userManager  , IMapper mapper , IUnitOfWork unitOfWork)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> AddToRole(int Role, User user)
        {
            var result = await this.userManager.AddToRolesAsync(user , new List<string> { ((Enums.UserTypes)Role).ToString() });
            return result.Succeeded;
        }

        public async Task<List<Claim>> checkRoleToken(User user)
        {
            var role = await userManager.GetRolesAsync(user);
            List<Claim> authClaims = new List<Claim>();
            IdentityOptions options = new IdentityOptions();
            authClaims = new List<Claim>() {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            if (role.Count > 0)
            {

                authClaims.Add(new Claim(options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault()));

            }
            return authClaims;
        }

        public async Task<bool> deleteUser(string Id)
        {
            try
            {
                User user = await userManager.FindByIdAsync(Id);
                var result = await this.userManager.DeleteAsync(user);
                return result.Succeeded;
            }
            catch
            {
                return false;
            }
        }

        public string getToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:secretToken"]));

            var token = new JwtSecurityToken(

                expires: DateTime.Now.AddDays(Convert.ToDouble(configuration["JWT:days"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> signIn(signInDto model)
        {
            User user = await userManager.FindByEmailAsync(model.Email);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                List<Claim> authClaims =  await this.checkRoleToken(user);
                var token = this.getToken(authClaims);
                return token;
            }
            throw new Exception("No user with this email");
        }

        public async Task<User> signUp(signUpDto signUpDto)

        {
            User userEmail = await userManager.FindByEmailAsync(signUpDto.Email);
           
            if (userEmail == null)
            {
                User user =await this.unitOfWork.users.addUser(signUpDto);

                var result = await userManager.CreateAsync(user, signUpDto.Password);
                if (result.Succeeded)
                {
                   return user;
                }
                throw new Exception(result.Errors.First().Description.ToString());
                

            }
            throw new Exception("Email already Taken");
        }
        
       

        
    }
}
