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
    [Route("api/teachersubject")]
    public class TeacherSubjectController : ControllerBase
    {
        private readonly IUserProviderService userProviderService;
        private readonly IUserService userService;
        private readonly ITeacherSubjectService teacherSubjectService;
        public TeacherSubjectController(IUserProviderService userProviderService, IUserService userService,
            ITeacherSubjectService teacherSubjectService)
        {
            this.userProviderService = userProviderService;
            this.userService = userService;
            this.teacherSubjectService = teacherSubjectService;
        }

        [Authorize]
        [HttpGet("admin/allteachersubject/{subjectId}")]
        public IActionResult GetTeacherSubjects(int subjectId)
        {
            int userId = int.Parse(userProviderService.GetUserId());
            if(userService.IsAdmin(userId))
            {
                TeacherSubjectListDTO teacherSubjectListDTO = teacherSubjectService.GetTeacherSubjectBySubjectId(subjectId);
                return Ok(teacherSubjectListDTO);
            }
            else
            {
                return BadRequest("Brak uprawnien administratora");
            }        
        }

        [Authorize]
        [HttpGet("admin/addteachersubject")]
        public IActionResult AddTeacherSubjects([FromQuery] int teacherId, [FromQuery] int subjectId)
        {
            int userId = int.Parse(userProviderService.GetUserId());
            if (userService.IsAdmin(userId))
            {
                teacherSubjectService.AddTeacherSubject(teacherId, subjectId);
                return Ok("Pomyslnie dodano powiazanie");
            }
            else
            {
                return BadRequest("Brak uprawnien administratora");
            }
        }

        [Authorize]
        [HttpGet("admin/deleteteachersubject")]
        public IActionResult DeleteTeacherSubjects([FromQuery] int teacherId, [FromQuery] int subjectId)
        {
            int userId = int.Parse(userProviderService.GetUserId());
            if (userService.IsAdmin(userId))
            {
                teacherSubjectService.DeleteTeacherSubject(teacherId, subjectId);
                return Ok("Pomyslnie usunieto powiazanie");
            }
            else
            {
                return BadRequest("Brak uprawnien administratora");
            }
        }
    }
}
