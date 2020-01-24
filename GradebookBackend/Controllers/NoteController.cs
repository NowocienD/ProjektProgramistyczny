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
    [Route("api/note")]
    public class NoteController : ControllerBase
    {
        private readonly IUserProviderService userProviderService;
        private readonly IUserService userService;
        private readonly INoteService noteService;

        public NoteController(INoteService noteService, IUserProviderService userProviderService,
             IUserService userService)
        {
            this.noteService = noteService;
            this.userProviderService = userProviderService;
            this.userService = userService;
        }

        [Authorize]
        [HttpGet("student/myNotes")]
        public IActionResult GetStudentNotes()
        {
            try
            {
                int userId = int.Parse(userProviderService.GetUserId());
                NoteListDTO noteListDTO = noteService.GetStudentNotesByStudentId(userService.GetStudentIdByUserId(userId));
                return Ok(noteListDTO);
            }
            catch (GradebookServerException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [Authorize]
        [HttpPost("teacher/addNewNote/{studentId}")]
        public IActionResult AddNewNote([FromBody]NoteDTO newNoteDTO, int studentId)
        {
            try
            {
                int userId = int.Parse(userProviderService.GetUserId());
                int teacherId = userService.GetTeacherIdByUserId(userId);
                noteService.AddNewNote(newNoteDTO, teacherId, studentId);
                return Ok("Note has been added");
            }
            catch(GradebookServerException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [Authorize]
        [HttpGet("teacher/studentNotes/{studentId}")]
        public IActionResult GetStudentNotesByStudentId(int studentId)
        {
            try
            {
                NoteListDTO noteListDTO = noteService.GetStudentNotesByStudentId(studentId);
                return Ok(noteListDTO);
            }
            catch(GradebookServerException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
