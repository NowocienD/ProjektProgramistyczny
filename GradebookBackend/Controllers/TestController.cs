using Microsoft.AspNetCore.Mvc;

namespace GradebookBackend.Controllers
{
    [ApiController]
    [Route("api")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Route("test")]
        public IActionResult Get()
        {
            return BadRequest("Wlasnie zwróciłem badRequest");
        }
    }
}
