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
        private readonly IUserProvider userProvider;
        private readonly IStudentService studentService;

        public StudentController(IUserProvider userProvider, IStudentService studentService)
        {
            this.userProvider = userProvider;
            this.studentService = studentService;
        }

        [Authorize]
        [HttpGet("student/myNotes")]
        public IActionResult GetStudentNotes()
        {
            int userId = int.Parse(userProvider.GetUserId());
            NoteListDTO noteListDTO = studentService.GetStudentNotesByUserId(userId);
            return Ok(noteListDTO);
        }
    }
}
