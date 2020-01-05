using GradebookBackend.DTO;
using GradebookBackend.ServicesCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Controllers
{
    [ApiController]
    [Route("api")]
    public class ClassController : Controller
    {
        private readonly IUserProviderService userProviderService;
        private readonly IUserService userService;
        private readonly IClassService classService;

        public ClassController(IUserProviderService userProviderService, IUserService userService, IClassService classService)
        {
            this.userProviderService = userProviderService;
            this.userService = userService;
            this.classService = classService;
        }

        [Authorize]
        [HttpGet("classes")]
        public IActionResult GetAllClasses()
        {
            int userId = int.Parse(userProviderService.GetUserId());
            if (userService.IsAdmin(userId))
            {
                ClassListDTO classListDTO = classService.GetAllClasses();
                return Ok(classListDTO);
            }
            else
            {
                return Forbid("Brak Uprawnien administratora");
            }
        }
    }
}
