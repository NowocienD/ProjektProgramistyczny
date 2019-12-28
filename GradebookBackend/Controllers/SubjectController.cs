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
        private readonly IUserProviderService userProvider;
        private readonly IUserDataService userDataService;
        private readonly ISubjectService subjectService;

        public SubjectController(IUserProviderService userProvider, IUserDataService userDataService, ISubjectService subjectService)
        {
            this.userProvider = userProvider;
            this.userDataService = userDataService;
            this.subjectService = subjectService;
        }

        [Authorize]
        [HttpGet("subjects")]
        public IActionResult GetAllSubjects()
        {
            int userId = int.Parse(userProvider.GetUserId());
            if(userDataService.IsAdmin(userId))
            {
                SubjectListDTO subjectListDTO = subjectService.GetAllSubjects();
                return Ok(subjectListDTO);
            }
            else
            {
                return Forbid();
            }

        }

    }
}
