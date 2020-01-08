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
    [Route("api/grade")]
    public class GradeController : ControllerBase
    {
        private readonly IUserProviderService userProviderService;
        private readonly IUserService userService;
        private readonly IGradeService gradeService;

        public GradeController(IUserProviderService userProviderService, IUserService userService,
            IGradeService gradeService)
        {
            this.gradeService = gradeService;
            this.userService = userService;
            this.userProviderService = userProviderService;
        }

        [Authorize]
        [HttpGet("student/myGrades/{subjectId}")]
        public IActionResult GetStudentGrades(int subjectId)
        {
            int userId = int.Parse(userProviderService.GetUserId());
            GradeListDTO gradeListDTO = gradeService.GetStudentGradesByStudentId(userService.GetStudentIdByUserId(userId), subjectId);
            return Ok(gradeListDTO);
        }

        [Authorize]
        [HttpGet("teacher/grades/{subjectId}/{studentId}")]
        public IActionResult GetGrades(int subjectId, int studentId)
        {
            int userId = int.Parse(userProviderService.GetUserId());
            try
            {
                if (userService.IsTeacher(userId))
                {
                    GradeListDTO gradeListDTO = gradeService.GetStudentGradesByStudentId(studentId, subjectId);
                    return Ok(gradeListDTO);
                }
                else
                {
                    return BadRequest("Logged user is not teacher");
                }
            }
            catch (GradebookServerException exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [Authorize]
        [HttpPost("teacher/addGrade/{studentId}")]
        public IActionResult AddNewGrade([FromBody] NewGradeDTO newGradeDTO, int studentId)
        {
            try
            {
                int userId = int.Parse(userProviderService.GetUserId());
                int teacherId = userService.GetTeacherIdByUserId(userId);
                gradeService.AddGrade(newGradeDTO, teacherId, studentId);
            }
            catch(GradebookServerException exception)
            {
                return BadRequest(exception.Message);
            }
            return Ok("Grade has been added");
        }

        [Authorize]
        [HttpPost("teacher/deleteGrade/{gradeId}")]
        public IActionResult DeleteGrade(int gradeId)
        {
            try
            {
                int userId = int.Parse(userProviderService.GetUserId());
                if (userService.IsTeacher(userId))
                {
                    gradeService.DeleteGrade(gradeId);
                }
                else return BadRequest("Logged user is not a teacher");
            }
            catch (GradebookServerException exception)
            {
                return BadRequest(exception.Message);
            }
            return Ok("Grade has been deleted");
        }
        [Authorize]
        [HttpPost("teacher/updateGrade/{studentId}")]
        public IActionResult UpdateGrade([FromBody] NewGradeDTO updatedGradeDTO, int studentId)
        {
            try
            {
                int userId = int.Parse(userProviderService.GetUserId());
                int teacherId = userService.GetTeacherIdByUserId(userId);
                gradeService.UpdateGrade(updatedGradeDTO, teacherId, studentId);
            }
            catch (GradebookServerException exception)
            {
                return BadRequest(exception.Message);
            }
            return Ok("Grade has been updated");
        }
    }
}
