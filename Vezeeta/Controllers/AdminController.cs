using Core.Domain;
using Core.DTOs.Auth;
using Core.DTOs.Doctor;
using Core.DTOs.User;
using Core.Interfaces;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Vezeeta.ResponseShape;

namespace Vezeeta.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AdminController:ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IUserService userService;
        private readonly IDoctorService doctorService;

        public AdminController(IAuthService authService, IUserService userService , IDoctorService doctorService)
        {
            this.authService = authService;
            this.userService = userService;
            this.doctorService = doctorService;
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer" , Roles = "Admin")]
        public async Task<IActionResult> addDoctor([FromForm] addDoctorDto addDoctorDto, [FromForm] signUpDto signUpDto)
        {
            try
            {
                signUpDto.UserType = Convert.ToInt32(Enums.UserTypes.Doctor);
                User user = await this.authService.signUp(signUpDto);
                await this.authService.AddToRole(Convert.ToInt32(Enums.UserTypes.Doctor) , user);
                var result = await this.doctorService.addDoctor(addDoctorDto, user);
                return CreatedAtAction(nameof(addDoctor), new serviceResponse<GetDoctorDto> { statusCode = Convert.ToInt32(Enums.StatusCode.created), data = result });

            }
            catch (FileNotFoundException ex)
            {
                return NotFound(new ErrorResponse { statusCode = Convert.ToInt32(Enums.StatusCode.NotFound), Errors = new { message = ex.Message } });

            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse { statusCode = Convert.ToInt32(Enums.StatusCode.BadRequest), Errors = new { message = ex.Message } });
            }
            
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]


        public async Task<IActionResult> DeleteDoctor(string id)
        {

            var result = await this.userService.deleteUser(id);
            if (result)
            {
                return Ok(new serviceResponse<string> { statusCode = Convert.ToInt32(Enums.StatusCode.Success), data = "doctor Removed" });
            }
            return NotFound(new ErrorResponse { statusCode = Convert.ToInt32(Enums.StatusCode.NotFound), Errors = new { message = "UnAble To delete" } });
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]


        public async Task<IActionResult> updateDoctor([FromForm] updateDoctorDto updateDoctorDto, [FromForm] updateUserDto updateUserDto, string id)
        {
            try
            {
                await this.userService.updateUser(updateUserDto, id);
                var result = await this.doctorService.updateDoctor(updateDoctorDto, id);



                return Ok(new serviceResponse<string> { statusCode = Convert.ToInt32(Enums.StatusCode.Success), data = result });
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorResponse { statusCode = Convert.ToInt32(Enums.StatusCode.BadRequest), Errors = new { message = ex.Message } });
            }
        }
    }
}
