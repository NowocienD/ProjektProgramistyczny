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
    public class StudentController : Controller
    {
        private readonly IUserProviderService userProviderService;
        private readonly IStudentService studentService;
        private readonly IUserService userService;
        private readonly ISubjectService subjectService;
        private readonly IGradeService gradeService;

        public StudentController(IUserProviderService userProviderService, IStudentService studentService,
            IUserService userService, ISubjectService subjectService,
            IGradeService gradeService)
        {
            this.userProviderService = userProviderService;
            this.studentService = studentService;
            this.userService = userService;
            this.subjectService = subjectService;
            this.gradeService = gradeService;
        }

        [Authorize]
        [HttpGet("student/myGrades/{subjectId}")]
        public IActionResult GetStudentGrades(int subjectId)
        {
            int userId = int.Parse(userProviderService.GetUserId());
            GradeListDTO gradeListDTO = gradeService.GetStudentGradesByStudentId(userService.GetStudentIdByUserId(userId), subjectId);
            return Ok(gradeListDTO);
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
        [HttpGet("students/class/{classId}")]
        public IActionResult GetAllStudentsByClassId(int classId)
        {
            int userId = int.Parse(userProviderService.GetUserId());
            if(userService.IsAdmin(userId) || userService.IsTeacher(userId))
            {
                StudentListDTO studentListDTO = studentService.GetStudentsByClassId(classId);
                return Ok(studentListDTO);
            }
            else
            {
                return BadRequest("Brak uprawnien do listy studentow");
            }
        }
    }
}
