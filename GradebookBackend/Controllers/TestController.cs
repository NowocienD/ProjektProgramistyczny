using Microsoft.AspNetCore.Mvc;

namespace GradebookBackend
{
    [ApiController]
    [Route("api")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Route("test")]
        public IActionResult Get()
        {
            return BadRequest("Wlascnie zwróciłem badRequest");
        }
    }
}
