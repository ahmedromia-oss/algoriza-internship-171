using Core.Domain;
using Core.DTOs.Appointments;
using Core.DTOs.Auth;
using Core.DTOs.bookers;
using Core.DTOs.Doctor;
using Core.DTOs.patient;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.ResponseShape;

namespace Vezeeta.Controllers
{
    [Route("api/[controller]/[action]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService patientService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IAuthService authService;

        public PatientController(IPatientService patientService, IHttpContextAccessor httpContextAccessor, IAuthService authService)
        {
            this.patientService = patientService;
            this.httpContextAccessor = httpContextAccessor;
            this.authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPatient([FromForm] addPatientDto addPatientDto, [FromForm] signUpDto signUpDto)
        {
            try
            {
                signUpDto.UserType = Convert.ToInt32(Enums.UserTypes.Patient);
                User user = await this.authService.signUp(signUpDto);
                await this.authService.AddToRole(Convert.ToInt32(Enums.UserTypes.Patient) , user);
                await this.patientService.addPatient(addPatientDto, user);
                return CreatedAtAction(nameof(AddPatient) , new serviceResponse<string> { statusCode = Convert.ToInt32(Enums.StatusCode.created), data = "Patient Added" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse { statusCode = Convert.ToInt32(Enums.StatusCode.BadRequest), Errors = new { message = ex.Message } });
            }
        }
        [HttpGet]
        public async Task<IActionResult> patients([FromQuery] int page, [FromQuery] int pageSize)
        {
            return Ok(new serviceResponse<ICollection<GetPatientDto>> { statusCode = Convert.ToInt32(Enums.StatusCode.Success), data = await this.patientService.getAll(new PaginationModel { Page = page, PageSize = pageSize }) });
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> patient(string id)
        {
            try
            {
                return Ok(new serviceResponse<GetPatientDto> { statusCode = Convert.ToInt32(Enums.StatusCode.Success), data = await this.patientService.getById(id) });
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorResponse { statusCode = Convert.ToInt32(Enums.StatusCode.NotFound), Errors = new { message = ex.Message } });
            }
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer " , Roles = "Patient")]
        public async Task<IActionResult> Profile()
        {
            string userEmail = this.httpContextAccessor.HttpContext.User.Identity.Name;

            return Ok(new serviceResponse<GetPatientDto> { statusCode = Convert.ToInt32(Enums.StatusCode.Success), data = await this.patientService.loggedInUser(userEmail) });

        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer " , Roles = "Patient")]
        public async Task<IActionResult> bookAppointment([FromQuery] string timeId)
        {
            string userEmail = this.httpContextAccessor.HttpContext.User.Identity.Name;
            try
            {
                return CreatedAtAction(nameof(bookAppointment), new serviceResponse<bool> {  statusCode = Convert.ToInt32(Enums.StatusCode.created), data = await this.patientService.book(timeId, userEmail) });
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorResponse { statusCode = Convert.ToInt32(Enums.StatusCode.NotFound), Errors = new { message = ex.Message } });
            }



        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer " , Roles = "Patient")]


        public async Task<IActionResult> bookings()
        {
            string userEmail = this.httpContextAccessor.HttpContext.User.Identity.Name;


            return Ok(new serviceResponse<ICollection<getBookings>> { statusCode = Convert.ToInt32(Enums.StatusCode.Success), data = await this.patientService.bookings(userEmail) });

        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer " , Roles = "Patient")]

        public async Task<IActionResult> cancelBooking([FromQuery] string timeId)
        {
            string userEmail = this.httpContextAccessor.HttpContext.User.Identity.Name;
            try
            {


                await this.patientService.cancelBooking(userEmail, timeId);
                return Ok(new serviceResponse<string> { statusCode = Convert.ToInt32(Enums.StatusCode.Success) , data = "cancelled Booking"});    
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
    }
}

