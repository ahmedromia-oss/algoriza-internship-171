using AutoMapper;
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
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService patientService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IAuthService authService;
        private readonly IDiscountService discountService;
        private readonly IMapper mapper;

        public PatientController(IPatientService patientService, IHttpContextAccessor httpContextAccessor, IAuthService authService , IDiscountService discountService , IMapper mapper)
        {
            this.patientService = patientService;
            this.httpContextAccessor = httpContextAccessor;
            this.authService = authService;
            this.discountService = discountService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterPatient([FromForm] addPatientDto addPatientDto, [FromForm] signUpDto signUpDto)
        {
        
                User user = await this.authService.signUp(signUpDto , Convert.ToInt32(Enums.UserTypes.Patient));
                await this.authService.AddToRole(Convert.ToInt32(Enums.UserTypes.Patient) , user);
                await this.patientService.addPatient(addPatientDto, user);
                return CreatedAtAction(nameof(RegisterPatient) , new serviceResponse<string> { statusCode = Convert.ToInt32(Enums.StatusCode.created), data = "Patient Added" });
            
            
        }
        [HttpGet]
        public async Task<IActionResult> patients([FromQuery] int page, [FromQuery] int pageSize , [FromQuery] string searchTerm = "")
        {
            return Ok(new serviceResponse<ICollection<getPatientZipped>> { statusCode = Convert.ToInt32(Enums.StatusCode.Success), data =this.mapper.Map<ICollection<getPatientZipped>>(await this.patientService.getAll(new PaginationModel { Page = page, PageSize = pageSize } , searchTerm)) });
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> patient(string id)
        {
           
                return Ok(new serviceResponse<GetPatientDto> { statusCode = Convert.ToInt32(Enums.StatusCode.Success), data = await this.patientService.getById(id) });
            
            
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
        public async Task<IActionResult> bookAppointment([FromBody] AddBookDto addBookDto)
        {
            string userEmail = this.httpContextAccessor.HttpContext.User.Identity.Name;
            PatientTime patientTime = await this.patientService.book(addBookDto.timeId, userEmail);
            if (addBookDto.DiscountCode != null)
            {
                await this.discountService.useDiscount(addBookDto.DiscountCode , userEmail , patientTime.timeId);

            }




            return CreatedAtAction(nameof(bookAppointment), new serviceResponse<string> {  statusCode = Convert.ToInt32(Enums.StatusCode.created), data = "Appointment Booked" });
            
          



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

        public async Task<IActionResult> cancelBooking([FromQuery] string BookingId)
        {
            string userEmail = this.httpContextAccessor.HttpContext.User.Identity.Name;

            PatientTime patientTime =await this.patientService.cancelBooking(userEmail, BookingId);




                return Ok(new serviceResponse<string> { statusCode = Convert.ToInt32(Enums.StatusCode.Success) , data = "cancelled Booking"});    
          
        }
    }
}

