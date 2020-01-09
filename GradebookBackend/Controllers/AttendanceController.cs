using GradebookBackend.DTO;
using GradebookBackend.Services;
using GradebookBackend.ServicesCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Controllers
{
    [ApiController]
    [Route("api/attendance")]
    public class AttendanceController : ControllerBase
    {
        private readonly IUserProviderService userProviderService;
        private readonly IUserService userService;
        private readonly IAttendanceService attendanceService;
        private readonly ILessonService lessonService;
        private readonly IStudentService studentService;

        public AttendanceController(IUserProviderService userProviderService, IUserService userService,
            IAttendanceService attendanceService, IStudentService studentService, ILessonService lessonService)
        {
            this.userProviderService = userProviderService;
            this.userService = userService;
            this.attendanceService = attendanceService;
            this.studentService = studentService;
            this.lessonService = lessonService;
        }

        [Authorize]
        [HttpGet("student/myAttendances")]
        public IActionResult GetStudentAttendances([FromQuery] int day, [FromQuery] int month, [FromQuery] int year)
        {
            if (day == 0 || month == 0 || year == 0)
            {
                return BadRequest("day, month i year nie moga byc rowne 0");
            }
            try
            {
                int userId = int.Parse(userProviderService.GetUserId());
                int studentId = userService.GetStudentIdByUserId(userId);
                DateTime date = new DateTime(year, month, day);
                SingleDayAttendancesListDTO singleDayAttendancesListDTO = attendanceService.GetAttendancesByStudentId(studentId, date);
                return Ok(singleDayAttendancesListDTO);
            }
            catch (GradebookServerException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [Authorize]
        [HttpGet("teacher/classAttendances")]
        public IActionResult GetClassAttendances([FromQuery] int classId, [FromQuery] int lessonNumber, [FromQuery] int day, [FromQuery] int month, [FromQuery] int year)
        {
            if (day == 0 || month == 0 || year == 0 || classId == 0 || lessonNumber == 0)
            {
                return BadRequest("day, month, year, classId i lessonNumber nie moga byc rowne 0");
            }
            int userId = int.Parse(userProviderService.GetUserId());
            if (userService.IsTeacher(userId))
            {
                try
                {
                    DateTime date = new DateTime(year, month, day);
                    int lessonId = lessonService.GetLessonId(lessonNumber, (int)date.DayOfWeek - 1, classId);
                    SingleLessonAttendancesListDTO singleLessonAttendancesListDTO = attendanceService.GetClassAttendances(classId, lessonId, date);
                    return Ok(singleLessonAttendancesListDTO);
                }
                catch (GradebookServerException exception)
                {
                    return BadRequest(exception.Message);
                }
            }
            else
            {
                return BadRequest("Brak uprawnien administratora");
            }
        }

        [Authorize]
        [HttpGet("teacher/addAttendance/{studentId}")]
        public IActionResult AddAttendance([FromBody] NewAttendanceDTO newAttendanceDTO, int studentId)
        {
            try
            {
                DateTime date = DateTime.ParseExact(newAttendanceDTO.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                int userId = int.Parse(userProviderService.GetUserId());
                int teacherId = userService.GetTeacherIdByUserId(userId);
                int classId = studentService.GetStudentClassIdByStudentId(studentId);
                int lessonId = lessonService.GetLessonId(newAttendanceDTO.LessonNumber, (int)date.DayOfWeek - 1, classId);
                attendanceService.AddAttendance(date, newAttendanceDTO.AttendanceStatusId, lessonId, studentId);
                return Ok("Obecnosc zostala dodana");
            }
            catch (GradebookServerException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
