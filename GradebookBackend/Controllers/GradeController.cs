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
    public class GradeController : Controller
    {
        private readonly IUserProviderService userProvider;
        private readonly IUserService userDataService;
        private readonly IGradeService gradeService;

        public GradeController(IUserProviderService userProvider, IUserService userDataService,
            IGradeService gradeService)
        {
            this.gradeService = gradeService;
            this.userDataService = userDataService;
            this.userProvider = userProvider;
        }

        [Authorize]
        [HttpPost("teacher/addGrade/{studentId}")]
        public IActionResult AddNewGrade([FromBody] NewGradeDTO newGradeDTO, int studentId)
        {
            if (newGradeDTO == null || string.IsNullOrWhiteSpace(newGradeDTO.Date) || string.IsNullOrWhiteSpace(newGradeDTO.Topic)
                || newGradeDTO.SubjectId <= 0 || newGradeDTO.Value <= 0 || newGradeDTO.Importance < 0 || studentId <= 0)
            {
                return Forbid("Invalid dto");
            }
            try
            {
                int userId = int.Parse(userProvider.GetUserId());
                int teacherId = userDataService.GetTeacherIdByUserId(userId);
                gradeService.AddGrade(newGradeDTO, teacherId, studentId);
            }
            catch(GradebookServerException exception)
            {
                return Forbid(exception.Message);
            }
            return Ok("Grade has been added");
        }

        [Authorize]
        [HttpPost("teacher/deleteGrade/{gradeId}")]
        public IActionResult DeleteGrade(int gradeId)
        {
            try
            {
                int userId = int.Parse(userProvider.GetUserId());
                if (userDataService.IsTeacher(userId))
                {
                    gradeService.DeleteGrade(gradeId);
                }
                else return Forbid("Logged user is not a teacher");
            }
            catch (GradebookServerException exception)
            {
                return Forbid(exception.Message);
            }
            return Ok("Grade has been deleted");
        }
        [Authorize]
        [HttpPost("teacher/updateGrade/{studentId}")]
        public IActionResult UpdateGrade([FromBody] NewGradeDTO updatedGradeDTO, int studentId)
        {
            try
            {
                int userId = int.Parse(userProvider.GetUserId());
                int teacherId = userDataService.GetTeacherIdByUserId(userId);
                gradeService.UpdateGrade(updatedGradeDTO, teacherId, studentId);
            }
            catch (GradebookServerException exception)
            {
                return Forbid(exception.Message);
            }
            return Ok("Grade has been updated");
        }
    }
}
