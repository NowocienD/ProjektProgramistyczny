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
        private readonly IUserProviderService userProvider;
        private readonly IUserDataService userDataService;
        private readonly IClassService classService;

        public ClassController(IUserProviderService userProvider, IUserDataService userDataService, IClassService classService)
        {
            this.userProvider = userProvider;
            this.userDataService = userDataService;
            this.classService = classService;
        }

        [Authorize]
        [HttpGet("classes")]
        public IActionResult GetAllClasses()
        {
            int userId = int.Parse(userProvider.GetUserId());
            if (userDataService.IsAdmin(userId))
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
