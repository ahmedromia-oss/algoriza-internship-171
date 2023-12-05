using Core.DTOs.Specialization;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.ResponseShape;


namespace Vezeeta.Controllers
{
    [Route("api/[controller]/[action]")]
    public class SpecializationController : ControllerBase
    {
        private readonly ISpecializationService specializationService;

        public SpecializationController(ISpecializationService specializationService)
        {
            this.specializationService = specializationService;
        }
        [HttpGet]
        [Route("/")]

        public async Task<IActionResult> All([FromQuery] int page , [FromQuery] int pageSize)
        {
            return Ok(new serviceResponse<ICollection<GetSpecializationDto>>{ statusCode = Convert.ToInt32(Enums.StatusCode.Success) , data = await this.specializationService.getSpecialization(new PaginationModel { Page = page, PageSize = pageSize })});
        }
    }
}
