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
    [Route("api/attendanceStatus")]
    public class AttendanceStatusController : ControllerBase
    {
        private readonly IAttendanceStatusService attendanceStatusService;
        private readonly IUserProviderService userProviderService;
        private readonly IUserService userService;

        public AttendanceStatusController(IAttendanceStatusService attendanceStatusService,
            IUserProviderService userProviderService, IUserService userService)
        {
            this.attendanceStatusService = attendanceStatusService;
            this.userProviderService = userProviderService;
            this.userService = userService;
        }

        [Authorize]
        [HttpGet("allAttendanceStatus")]
        public IActionResult GetAttendanceStatusList()
        {
            AttendanceStatusListDTO attendanceStatusListDTO = attendanceStatusService.GetAttendanceStatusList();
            return Ok(attendanceStatusListDTO);
        }

        [Authorize]
        [HttpGet("addAttendanceStatus/{newAttendanceStatusName}")]
        public IActionResult AddAttendanceStatusList(string newAttendanceStatusName)
        {
            int userId = int.Parse(userProviderService.GetUserId());
            if (userService.IsAdmin(userId))
            {
                attendanceStatusService.AddAttendanceStatus(newAttendanceStatusName);
                return Ok("Nowy stan obecnosci zostal dodany");
            }
            else
            {
                return BadRequest("Brak uprawnien administratora");
            }
        }
    }
}
