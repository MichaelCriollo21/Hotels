using Hotels.BusinessLogic.Interface;
using Hotels.Entity.DTO;
using Hotels.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IAuthentication IAuthenticationBL;

        public AuthController(IAuthentication authentication)
        {
            IAuthenticationBL = authentication;
        }

        [HttpPost]
        [Route("Login")]
        //La contraseña es "Password123!"
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var result = await IAuthenticationBL.LoginUserAsync(loginDTO);
            return Ok(result);
        }
    }
}
