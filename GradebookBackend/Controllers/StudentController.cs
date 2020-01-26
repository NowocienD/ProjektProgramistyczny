using GradebookBackend.DTO;
using GradebookBackend.ServicesCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GradebookBackend.Controllers
{
    [ApiController]
    [Route("api/student")]
    public class StudentController : Controller
    {
        private readonly IUserProviderService userProviderService;
        private readonly IStudentService studentService;
        private readonly IUserService userService;

        public StudentController(IUserProviderService userProviderService, IStudentService studentService,
            IUserService userService)
        {
            this.userProviderService = userProviderService;
            this.studentService = studentService;
            this.userService = userService;
        }

        [Authorize]
        [HttpGet("studentsFromClass/{classId}")]
        public IActionResult GetAllStudentsByClassId(int classId)
        {
            int userId = userProviderService.GetUserId();
            if (userService.IsAdmin(userId) || userService.IsTeacher(userId))
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
