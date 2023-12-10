using AutoMapper;
using Core.Domain;
using Core.DTOs.Auth;
using Core.DTOs.Discount;
using Core.DTOs.Doctor;
using Core.DTOs.User;
using Core.Interfaces;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Vezeeta.Filters;
using Vezeeta.ResponseShape;

namespace Vezeeta.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]


    public class AdminController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IAuthService authService;
        private readonly IUserService userService;
        private readonly IDoctorService doctorService;
        private readonly IDiscountService discountService;
        private readonly IRequestService requestService;

        public AdminController(IMapper mapper , IAuthService authService, IUserService userService, IDoctorService doctorService , IDiscountService discountService , IRequestService requestService)
        {
            this.mapper = mapper;
            this.authService = authService;
            this.userService = userService;
            this.doctorService = doctorService;
            this.discountService = discountService;
            this.requestService = requestService;
        }
        [HttpPost]
       
        public async Task<IActionResult> addDoctor([FromForm] addDoctorDto addDoctorDto, [FromForm] signUpDto signUpDto)
        {

            User user = await this.authService.signUp(signUpDto , Convert.ToInt32(Enums.UserTypes.Doctor));
            await this.authService.AddToRole(Convert.ToInt32(Enums.UserTypes.Doctor), user);
            var result = await this.doctorService.addDoctor(addDoctorDto, user);
            return CreatedAtAction(nameof(addDoctor), new serviceResponse<GetDoctorDto> { statusCode = Convert.ToInt32(Enums.StatusCode.created), data = mapper.Map<GetDoctorDto>(result) });

        }

        [HttpDelete("{id}")]
      

        public async Task<IActionResult> DeleteDoctor(string id)
        {

            
            
                if (!(await this.requestService.checkIfHaveRequests(id)))
                {
                
                    var result = await this.userService.deleteUser(id);
                if (result)
                {
                    return NotFound(new serviceResponse<string> { statusCode = Convert.ToInt32(Enums.StatusCode.NotFound), data = "doctor Not Found" });

                }

                return Ok(new serviceResponse<string> { statusCode = Convert.ToInt32(Enums.StatusCode.Success), data = "doctor Removed" });
                }
                throw new Exception("Can't delete doctor with Appending Appointments");
            
        }

        [HttpPut("{id}")]
       


        public async Task<IActionResult> updateDoctor([FromForm] updateDoctorDto updateDoctorDto, [FromForm] updateUserDto updateUserDto, string id)
        {

            await this.userService.updateUser(updateUserDto, id);
            var result = await this.doctorService.updateDoctor(updateDoctorDto, id);



            return Ok(new serviceResponse<string> { statusCode = Convert.ToInt32(Enums.StatusCode.Success), data = result });



        }
        [HttpPost]
       
        public async Task<IActionResult> addDiscount([FromForm] addDiscountDto addDiscountDto)
        {
            await this.discountService.addDiscount(addDiscountDto);
            return CreatedAtAction( nameof(addDiscount) , new serviceResponse<string>{statusCode = Convert.ToInt32(Enums.StatusCode.created) , data = "Discount added"});
        }
        [HttpGet]
        
        public async Task<IActionResult> getDiscount([FromQuery] int page , [FromQuery] int pageSize)
        {
            return Ok(new serviceResponse<ICollection<getDiscountDto>> { statusCode = Convert.ToInt32(Enums.StatusCode.Success), data = await this.discountService.getDiscounts(new PaginationModel { Page = page , PageSize = pageSize}) });
        }
        [HttpPut("{discountId}")]
        
        public async Task<IActionResult> updateDiscount(string discountId , [FromBody] updateDiscountDto updateDiscountDto)
        {
            await this.discountService.updateDiscount(updateDiscountDto , discountId);
            return CreatedAtAction(nameof(updateDiscount), new serviceResponse<string> { statusCode = Convert.ToInt32(Enums.StatusCode.created), data = "Discount Updated" });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteDiscount(string id)
        {
            await this.discountService.deleteDiscount(id);
            return Ok( new serviceResponse<string> { statusCode = Convert.ToInt32(Enums.StatusCode.Success), data = "Discount Deleted" });
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> deactivateDiscount(string id)
        {
            await this.discountService.deActivateDiscount(id);
            return Ok(new serviceResponse<string> { statusCode = Convert.ToInt32(Enums.StatusCode.Success), data = "Discount Deactivated" });
        }


    }
}
