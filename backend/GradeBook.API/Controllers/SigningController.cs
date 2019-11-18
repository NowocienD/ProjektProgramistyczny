using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GradeBook.API.Controllers
{
    [Route("api")]
    public class SigningController : ControllerBase
    {
        public static readonly SymmetricSecurityKey SigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

        private const string SecretKey = "TQvgjeABMPOwCycOqah5EQujyhvgjuhb5yyVjpmVG";

        private readonly ITokenGeneratorService tokenService;

        public SigningController(ITokenGeneratorService tokenService)
        {
            this.tokenService = tokenService; 
        }

        [HttpGet("token")]
        public IActionResult GetToken()
        {
            int userId = 12;
            return new ObjectResult(tokenService.GenerateToken(userId, SigningKey));
        }

        [Authorize]
        [HttpGet("values")]
        public IEnumerable<string> TestGetValues()
        {
            return new string[] { "value1", "value2" };
        }
    }
}