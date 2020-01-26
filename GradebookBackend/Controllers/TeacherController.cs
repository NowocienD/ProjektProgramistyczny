using GradebookBackend.DTO;
using GradebookBackend.ServicesCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GradebookBackend.Controllers
{
    [ApiController]
    [Route("api/teacher")]
    public class TeacherController : ControllerBase
    {
        private readonly IUserProviderService userProviderService;
        private readonly IUserService userService;
        private readonly ITeacherService teacherService;

        public TeacherController(IUserProviderService userProviderService, IUserService userService,
            ITeacherService teacherService)
        {
            this.userProviderService = userProviderService;
            this.userService = userService;
            this.teacherService = teacherService;
        }

        [Authorize]
        [HttpGet("admin/allteachers")]
        public IActionResult GetTeachers()
        {
            int userId = userProviderService.GetUserId();
            if (userService.IsAdmin(userId))
            {
                TeacherListDTO teachersListDTO = teacherService.GetAllTeachers();
                return Ok(teachersListDTO);
            }
            else
            {
                return BadRequest("Brak uprawnien administratora");
            }
        }
    }
}
