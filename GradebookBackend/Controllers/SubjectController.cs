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
    public class SubjectController : Controller
    {
        private readonly IUserProviderService userProviderService;
        private readonly IUserService userService;
        private readonly ISubjectService subjectService;

        public SubjectController(IUserProviderService userProviderService, IUserService userService, ISubjectService subjectService)
        {
            this.userProviderService = userProviderService;
            this.userService = userService;
            this.subjectService = subjectService;
        }

        [Authorize]
        [HttpGet("subjects")]
        public IActionResult GetAllSubjects()
        {
            int userId = int.Parse(userProviderService.GetUserId());
            if(userService.IsAdmin(userId))
            {
                SubjectListDTO subjectListDTO = subjectService.GetAllSubjects();
                return Ok(subjectListDTO);
            }
            else
            {
                return BadRequest("Brak uprawnien administratora");
            }

        }

    }
}
