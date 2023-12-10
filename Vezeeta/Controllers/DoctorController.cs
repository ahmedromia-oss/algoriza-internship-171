using Core.Domain;
using Core.DTOs.Appointments;
using Core.DTOs.Auth;
using Core.DTOs.bookers;
using Core.DTOs.Doctor;
using Core.DTOs.User;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vezeeta.ResponseShape;

namespace Vezeeta.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService doctorService;
        private readonly IHttpContextAccessor httpContextAccessor;
        

        public DoctorController(IDoctorService doctorService , IHttpContextAccessor httpContextAccessor)
        {
            this.doctorService = doctorService;
            this.httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        
        public async Task<IActionResult> getAllDoctors([FromQuery] int pageSize , [FromQuery] int page , [FromQuery] string searchTerm = "")
        {
            return Ok(new serviceResponse<ICollection<GetDoctorDto>> { statusCode = Convert.ToInt32(Enums.StatusCode.Success), data = await this.doctorService.getAll(new PaginationModel { Page = page, PageSize = pageSize } , searchTerm) });

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getDoctorById(string id)
           
        {
            var result = await this.doctorService.getDoctorById(e => e.userId == id);
            if (result != null)
            {
                return Ok(new serviceResponse<GetDoctorDto> {statusCode = Convert.ToInt32(Enums.StatusCode.Success), data = result});
            }
            return NotFound(new ErrorResponse{ statusCode = Convert.ToInt32(Enums.StatusCode.NotFound), Errors =  new {message ="Doctor Not Found"} });
        }
       
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer" , Roles = "Doctor")]
        public async Task<IActionResult> addAppointment([FromBody] AddApointments addApointments)

        {

          
            
                string userEmail = this.httpContextAccessor.HttpContext.User.Identity.Name;
                await this.doctorService.addAppointment(addApointments, userEmail);
            
                return CreatedAtAction(nameof(addAppointment), new serviceResponse<string> { statusCode = Convert.ToInt32(Enums.StatusCode.created), data = "Appointment Added" });
            
           
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer" , Roles = "Doctor")]

        public async Task<IActionResult> getBookers([FromQuery] int searchDay , [FromQuery] int page , [FromQuery] int pageSize)
        {
            string userEmail = this.httpContextAccessor.HttpContext.User.Identity.Name;

            return Ok(new serviceResponse<ICollection<getBookers>> { statusCode = Convert.ToInt32(Enums.StatusCode.Success), data =await this.doctorService.getBookers(userEmail , searchDay ,new PaginationModel {Page = page ,PageSize = pageSize})});


        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer" , Roles = "Doctor")]

        public async Task<IActionResult> confirmCheckUp([FromQuery] string BookingId)
        {
                string userEmail = this.httpContextAccessor.HttpContext.User.Identity.Name;

                await this.doctorService.confirmBook(BookingId , userEmail);
                return Ok(new serviceResponse<string> { statusCode = Convert.ToInt32(Enums.StatusCode.Success), data = "confirmed Booking" });
            
           

        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer" , Roles = "Doctor")]

        public async Task<IActionResult> updateTime([FromBody] updateAppointment updateAppointment , [FromQuery] string timeId)
        {
            string userEmail = this.httpContextAccessor.HttpContext.User.Identity.Name;
          

                await this.doctorService.UpdateTime(userEmail, timeId, updateAppointment);
                return Created( nameof(updateTime), new serviceResponse<string> { statusCode = Convert.ToInt32(Enums.StatusCode.created), data = "Time Updated" });
           
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = "Bearer" , Roles = "Doctor")]

        public async Task<IActionResult> deleteTime([FromQuery] string timeId)
        {
            string userEmail = this.httpContextAccessor.HttpContext.User.Identity.Name;
          


                await this.doctorService.deleteTime(userEmail, timeId);
                return Ok(new serviceResponse<string> { statusCode = Convert.ToInt32(Enums.StatusCode.Success), data = "Time Deleted" });
            

        }





    }
}
