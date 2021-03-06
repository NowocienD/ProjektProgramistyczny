﻿using GradebookBackend.DTO;
using GradebookBackend.ServicesCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            int userId = userProviderService.GetUserId();
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
