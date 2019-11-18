using System.Collections.Generic;
using GradeBook.Services.Core;
using GradeBook.Services.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GradeBook.API.Controllers
{
    [Route("api")]
    public class LoginController : ControllerBase
    {
        private readonly ITokenGeneratorService tokenService;

        private readonly IUserData userData;

        public LoginController(
            ITokenGeneratorService tokenService,
            IUserData userData)
        {
            this.tokenService = tokenService;
            this.userData = userData;
        }

        [HttpPost("login")]
        public IActionResult GetToken([FromBody] UserLoginCommandDto dto)
        {
            if (dto == null | string.IsNullOrWhiteSpace(dto.login) | string.IsNullOrEmpty(dto.password))
            {
                return BadRequest();
            }            

            string token = tokenService.GenerateToken(userData.GetId(dto.login));

            // dogadaj z FIlipem sposob przekazywania. Gdzie w headerze ma sie zanjdować token
            return Ok(token); 
        }

        [Authorize]
        [HttpGet("values")]
        public IEnumerable<string> TestGetValues()
        {
            return new string[] { "value1", "value2" };
        }
    }
}