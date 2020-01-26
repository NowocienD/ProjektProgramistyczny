using GradebookBackend.DTO;
using GradebookBackend.ServicesCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GradebookBackend.Controllers
{
    [ApiController]
    [Route("api/classsubject/")]
    public class ClassSubjectController : ControllerBase
    {
        private readonly IUserProviderService userProviderService;
        private readonly IUserService userService;
        private readonly IClassSubjectService classSubjectService;

        public ClassSubjectController(IUserProviderService userProviderService, IUserService userService,
            IClassSubjectService classSubjectService)
        {
            this.userProviderService = userProviderService;
            this.userService = userService;
            this.classSubjectService = classSubjectService;
        }

        [Authorize]
        [HttpGet("admin/addclasssubject")]
        public IActionResult AddClassSubject([FromQuery] int classId, [FromQuery] int subjectId)
        {
            int userId = userProviderService.GetUserId();
            if (userService.IsAdmin(userId))
            {
                classSubjectService.AddClassSubject(classId, subjectId);
                return Ok("Pomyslnie dodano powiazanie klasy z przedmiotem");
            }
            else
            {
                return BadRequest("Brak uprawnien administratora");
            }
        }

        [Authorize]
        [HttpDelete("admin/deleteclasssubject")]
        public IActionResult DeleteClassSubject([FromQuery] int classId, [FromQuery] int subjectId)
        {
            int userId = userProviderService.GetUserId();
            if (userService.IsAdmin(userId))
            {
                classSubjectService.DeleteClassSubject(classId, subjectId);
                return Ok("Pomyslnie usunieto powiazanie klasy z przedmiotem");
            }
            else
            {
                return BadRequest("Brak uprawnien administratora");
            }
        }

        [Authorize]
        [HttpGet("admin/subjectsassignedtoclass/{classId}")]
        public IActionResult GetSubjectsAssignedToClass(int classId)
        {
            int userId = userProviderService.GetUserId();
            if (userService.IsAdmin(userId))
            {
                SubjectListDTO subjectListDTO = classSubjectService.GetSubjectsAssignedToClass(classId);
                return Ok(subjectListDTO);
            }
            else
            {
                return BadRequest("Brak uprawnien administratora");
            }
        }
    }
}
