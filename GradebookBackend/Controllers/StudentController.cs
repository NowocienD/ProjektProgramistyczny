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

        public StudentController(IUserProviderService userProvider, IStudentService studentService,
            IUserDataService userDataService, ILessonService lessonService)
        {
            this.userProvider = userProvider;
            this.studentService = studentService;
            this.userDataService = userDataService;
            this.lessonService = lessonService;
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
        [HttpGet("student/myLessonPlan")]
        public IActionResult GetStudentLessonPlan()
        {
            int userId = int.Parse(userProvider.GetUserId());
            LessonPlanDTO lessonPlanDTO = lessonService.GetLessonPlanByClassId(
                studentService.GetStudentClassIdByStudentId(userDataService.GetStudentIdByUserId(userId)));
            return Ok(lessonPlanDTO);
        }
    }
}
