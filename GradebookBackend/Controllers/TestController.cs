using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GradebookBackend.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Route("test")]
        public IActionResult Get()
        {
            return BadRequest("Wlasnie zwróciłem badRequest");
        }

        [Authorize]
        [HttpGet("token")]
        public IActionResult TestToken()
        {
            return Ok("Autoryzacja potwierdzona");
        }
    }
}
