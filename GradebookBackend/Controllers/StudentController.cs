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
        private readonly IUserProviderService userProvider;
        private readonly IStudentService studentService;
        private readonly IUserDataService userDataService;
        private readonly ILessonService lessonService;
        private readonly ISubjectService subjectService;

        public StudentController(IUserProviderService userProvider, IStudentService studentService,
            IUserDataService userDataService, ILessonService lessonService, ISubjectService subjectService)
        {
            this.userProvider = userProvider;
            this.studentService = studentService;
            this.userDataService = userDataService;
            this.lessonService = lessonService;
            this.subjectService = subjectService;
        }

        [Authorize]
        [HttpGet("student/myNotes")]
        public IActionResult GetStudentNotes()
        {
            int userId = int.Parse(userProvider.GetUserId());
            NoteListDTO noteListDTO = studentService.GetStudentNotesByStuedntId(userDataService.GetStudentIdByUserId(userId));
            return Ok(noteListDTO);
        }

        [Authorize]
        [HttpGet("student/myGrades/{subjectId}")]
        public IActionResult GetStudentGrades(int subjectId)
        {
            int userId = int.Parse(userProvider.GetUserId());
            GradeListDTO gradeListDTO = studentService.GetStudentGradesByStudentId(userDataService.GetStudentIdByUserId(userId), subjectId);
            return Ok(gradeListDTO);
        }
        [Authorize]
        [HttpGet("student/mySubjects")]
        public IActionResult GetStudentSubjects()
        {
            int userId = int.Parse(userProvider.GetUserId());
            SubjectListDTO subjectListDTO = subjectService.GetSubjectListByClassId(
                studentService.GetStudentClassIdByStudentId(userDataService.GetStudentIdByUserId(userId)));
            return Ok(subjectListDTO);
        }

        [Authorize]
        [HttpGet("student/myLessonPlan")]
        public IActionResult GetStudentLessonPlan()
        {
            int userId = int.Parse(userProvider.GetUserId());
            LessonPlanDTO lessonPlanDTO = lessonService.GetLessonPlanByClassId(
                studentService.GetStudentClassIdByStudentId(userDataService.GetStudentIdByUserId(userId)));
            return Ok(lessonPlanDTO);
        }

        [Authorize]
        [HttpGet("students/class/{classId}")]
        public IActionResult GetAllStudentsByClassId(int classId)
        {
            int userId = int.Parse(userProvider.GetUserId());
            if(userDataService.IsAdmin(userId) || userDataService.IsTeacher(userId))
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
