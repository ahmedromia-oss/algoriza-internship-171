using Core.Domain;
using Core.DTOs.Auth;
using Core.DTOs.User;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.ResponseShape;

namespace Vezeeta.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthController(IAuthService authService , IHttpContextAccessor httpContextAccessor)
        {
            this.authService = authService;
            this.httpContextAccessor = httpContextAccessor;
        }
        [HttpPost]
        public async Task<IActionResult> signIn([FromBody] signInDto signInDto)
        {
            
          
            
                return Ok(new TokenResponse { statusCode = Convert.ToInt32(Enums.StatusCode.Success), Token = await this.authService.signIn(signInDto) });

            
           
        }
      


       
    }
}
