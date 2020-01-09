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
    [Route("api/subject")]
    public class SubjectController : ControllerBase
    {
        private readonly IUserProviderService userProviderService;
        private readonly IUserService userService;
        private readonly IStudentService studentService;
        private readonly ISubjectService subjectService;

        public SubjectController(IUserProviderService userProviderService, IUserService userService,
            ISubjectService subjectService, IStudentService studentService)
        {
            this.userProviderService = userProviderService;
            this.userService = userService;
            this.subjectService = subjectService;
            this.studentService = studentService;
        }

        [Authorize]
        [HttpGet("student/mySubjects")]
        public IActionResult GetStudentSubjects()
        {
            int userId = int.Parse(userProviderService.GetUserId());
            SubjectListDTO subjectListDTO = subjectService.GetSubjectListByClassId(
                studentService.GetStudentClassIdByStudentId(userService.GetStudentIdByUserId(userId)));
            return Ok(subjectListDTO);
        }

        [Authorize]
        [HttpGet("allSubjects")]
        public IActionResult GetAllSubjects()
        {
            int userId = int.Parse(userProviderService.GetUserId());
            if (userService.IsAdmin(userId))
            {
                SubjectListDTO subjectListDTO = subjectService.GetAllSubjects();
                return Ok(subjectListDTO);
            }
            else
            {
                return BadRequest("Logged user is not a admin");
            }

        }

        [Authorize]
        [HttpPost("admin/addNewSubject")]
        public IActionResult AddNewSubject([FromBody]SubjectDTO newSubjectDTO)
        {
            try
            {
                subjectService.AddNewSubject(newSubjectDTO);
                return Ok("Pomyslnie dodano przedmiot");
            } catch(GradebookServerException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
