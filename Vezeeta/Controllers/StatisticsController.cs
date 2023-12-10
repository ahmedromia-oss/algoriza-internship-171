
using Core.DTOs.Doctor;
using Core.DTOs.Requests;
using Core.DTOs.Specialization;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.ResponseShape;
namespace Vezeeta.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]

    public class StatisticsController : ControllerBase
    {
        private readonly IDoctorService doctorService;
        private readonly IPatientService patientService;
        private readonly IRequestService requestService;

        public StatisticsController(IDoctorService doctorService , IPatientService patientService , IRequestService requestService)
        {
            this.doctorService = doctorService;
            this.patientService = patientService;
            this.requestService = requestService;
        }
        [HttpGet]
        public async Task<IActionResult> getNumberOfRequests()
        {
            var result = await this.requestService.getNumberOfRequests();
            if (result == null)
            {
                return Ok(new serviceResponse<string> { statusCode = Convert.ToInt32(Enums.StatusCode.Success), data = "No Data To Calculate On" });

            }
            return Ok(new serviceResponse<getNumberOfRequests> { statusCode = Convert.ToInt32(Enums.StatusCode.Success), data = result });
        }
        [HttpGet]
        public async Task<IActionResult> getNumberPatients()
        {
            return Ok(new serviceResponse<int> { statusCode = Convert.ToInt32(Enums.StatusCode.Success), data = await this.patientService.count() });

        }
        [HttpGet]
        public async Task<IActionResult> getNumberOfDoctors()
        {
            return Ok(new serviceResponse<int> { statusCode = Convert.ToInt32(Enums.StatusCode.Success), data = await this.doctorService.count() });

        }
        [HttpGet]
        public async Task<IActionResult> TopDoctors()
        {
            var result = await this.requestService.Top10Doctors();
            if (result == null)
            {
            return Ok(new serviceResponse<string> { statusCode = Convert.ToInt32(Enums.StatusCode.Success), data = "No Data To Calculate On" });
                
            }
            return Ok(new serviceResponse<Object> { statusCode = Convert.ToInt32(Enums.StatusCode.Success), data = result });

        }

        [HttpGet]
        public async Task<IActionResult> TopSpecialization()
        {
            var result = await this.requestService.Top5Specialization();
            if (result == null)
            {
            return Ok(new serviceResponse<string> { statusCode = Convert.ToInt32(Enums.StatusCode.Success), data = "No Data To Calculate On" });
                
            }
            return Ok(new serviceResponse<Object> { statusCode = Convert.ToInt32(Enums.StatusCode.Success), data = result });

        }





    }
}
