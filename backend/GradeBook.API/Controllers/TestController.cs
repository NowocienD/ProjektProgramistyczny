using Microsoft.AspNetCore.Mvc;

namespace GradeBook.Controllers
{
    [ApiController]
    [Route("api")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Route("abc")]
        public string Get()
        {
            return "dziala";
        }
    }
}
