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
    public class AttendanceController : Controller
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
        public IActionResult GetStudentAttendances([FromBody] DatesDTO datesDTO )
        {
            int studentId;
            try
            {
                int userId = int.Parse(userProviderService.GetUserId());
                 studentId = userService.GetStudentIdByUserId(userId);
            }
            catch(GradebookServerException exception)
            {
                return BadRequest(exception.Message);
            }
            SingleDayAttendancesListDTO singleDayAttendancesListDTO = attendanceService.GetAttendancesByStudentId(studentId, datesDTO);
            return Ok(singleDayAttendancesListDTO);
        }
    }
}
