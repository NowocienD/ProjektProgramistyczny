using GradebookBackend.DTO;
using GradebookBackend.ServicesCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GradebookBackend.Controllers
{
    [ApiController]
    [Route("api/lesson")]
    public class LessonController : ControllerBase
    {
        private readonly IUserProviderService userProviderService;
        private readonly IUserService userService;
        private readonly ILessonService lessonService;
        private readonly IStudentService studentService;

        public LessonController(IUserProviderService userProviderService, IUserService userService,
            ILessonService lessonService, IStudentService studentService)
        {
            this.userService = userService;
            this.userProviderService = userProviderService;
            this.lessonService = lessonService;
            this.studentService = studentService;
        }

        [Authorize]
        [HttpGet("student/myLessonPlan")]
        public IActionResult GetStudentLessonPlan()
        {
            try
            {
                int userId = userProviderService.GetUserId();
                LessonPlanDTO lessonPlanDTO = lessonService.GetStudentLessonPlanByClassId(
                studentService.GetStudentClassIdByStudentId(userService.GetStudentIdByUserId(userId)));
                return Ok(lessonPlanDTO);
            }
            catch (GradebookServerException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [Authorize]
        [HttpGet("teacher/myLessonPlan")]
        public IActionResult GetTeacherLessonPlan()
        {
            try
            {
                int userId = userProviderService.GetUserId();
                int teacherId = userService.GetTeacherIdByUserId(userId);
                LessonPlanDTO lessonPlanDTO = lessonService.GetTeacherLessonPlanByTeacherId(teacherId);
                return Ok(lessonPlanDTO);
            }
            catch (GradebookServerException exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [Authorize]
        [HttpGet("admin/LessonPlan/{classId}/{dayOfTheWeek}")]
        public IActionResult GetLessonPlan(int dayOfTheWeek, int classId)
        {
            int userId = userProviderService.GetUserId();
            if (userService.IsAdmin(userId))
            {
                SingleDayLessonPlanExtendedDTO singleDaylessonPlanDTO = lessonService.
                    GetSingleDayLessonPlanByDayOfTheWeekAndClassId(dayOfTheWeek, classId);
                return Ok(singleDaylessonPlanDTO);
            }
            else
            {
                return BadRequest("Brak uprawnien administratora");
            }
        }

        [Authorize]
        [HttpPost("admin/addlesson")]
        public IActionResult AddLesson([FromBody] NewLessonDTO newLessonDTO)
        {
            int userId = userProviderService.GetUserId();
            if (userService.IsAdmin(userId))
            {
                if (lessonService.CheckIfLessonExists(newLessonDTO.LessonNumber, newLessonDTO.DayOfTheWeek, newLessonDTO.ClassId))
                {
                    return BadRequest("Lekcja w tym czasie dla podanej klasy juz istnieje");
                }
                else
                {
                    lessonService.AddLesson(newLessonDTO);
                    return Ok("Pomyslnie dodano nowa lekcje");
                }
            }
            else
            {
                return BadRequest("Brak uprawnien administratora");
            }
        }

        [Authorize]
        [HttpPatch("admin/updatelesson/{lessonId}")]
        public IActionResult UpdateLesson([FromBody] NewLessonDTO updatedLessonDTO, int lessonId)
        {
            int userId = userProviderService.GetUserId();
            if (userService.IsAdmin(userId))
            {
                lessonService.UpdateLesson(updatedLessonDTO, lessonId);
                return Ok("Pomyslnie zaktualizowano lekcje");
            }
            else
            {
                return BadRequest("Brak uprawnien administratora");
            }
        }

        [Authorize]
        [HttpDelete("admin/deletelesson/{lessonId}")]
        public IActionResult DeleteLesson(int lessonId)
        {
            if (lessonId == 0)
            {
                return BadRequest("Nie ma takiej lekcji");
            }
            int userId = userProviderService.GetUserId();
            if (userService.IsAdmin(userId))
            {
                lessonService.DeleteLesson(lessonId);
                return Ok("Pomyslnie usunieto lekcje");                           
            }
            else
            {
                return BadRequest("Brak uprawnien administratora");
            }
        }
        [Authorize]
        [HttpGet("admin/getlesson/{lessonId}")]
        public IActionResult GetLesson(int lessonId)
        {
            int userId = userProviderService.GetUserId();
            if (userService.IsAdmin(userId))
            {
                LessonDTO lessonDTO = lessonService.GetLessonByLessonId(lessonId);
                return Ok(lessonDTO);
            }
            else
            {
                return BadRequest("Brak uprawnien administratora");
            }
        }
    }
}
