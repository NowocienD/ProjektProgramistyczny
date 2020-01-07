using GradebookBackend.DTO;
using GradebookBackend.Services;
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
    public class AttendanceController : ControllerBase
    {
        private readonly IUserProviderService userProviderService;
        private readonly IUserService userService;
        private readonly IAttendanceService attendanceService;
        public AttendanceController(IUserProviderService userProviderService, IUserService userService,
            IAttendanceService attendanceService)
        {
            this.userProviderService = userProviderService;
            this.userService = userService;
            this.attendanceService = attendanceService;
        }

        [Authorize]
        [HttpGet("student/myAttendances")]
        public IActionResult GetStudentAttendances([FromQuery] int day, [FromQuery] int month, [FromQuery] int year)
        {
            try
            {
                int userId = int.Parse(userProviderService.GetUserId());
                int studentId = userService.GetStudentIdByUserId(userId);
                SingleDayAttendancesListDTO singleDayAttendancesListDTO = attendanceService.GetAttendancesByStudentId(studentId, day, month, year);
                return Ok(singleDayAttendancesListDTO);
            }
            catch(GradebookServerException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
