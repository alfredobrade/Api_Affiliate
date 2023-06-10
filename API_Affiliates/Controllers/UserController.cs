using API_Affiliates.Models.Auth;
using API_Affiliates.ServiceInterfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Affiliates.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigins")]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;

        public UserController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthRequest request)
        {
            var auth_result = await _authService.GetToken(request);
            if (auth_result == null) return Unauthorized();
            return Ok(auth_result);
        }



    }
}
