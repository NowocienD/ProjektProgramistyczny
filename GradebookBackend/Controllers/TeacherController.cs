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
    public class TeacherController : Controller
    {
        private readonly IUserProviderService userProvider;
        private readonly IUserDataService userDataService;
        private readonly IGradeService gradeService;

        public TeacherController(IUserProviderService userProvider, IUserDataService userDataService,
            IGradeService gradeService)
        {
            this.gradeService = gradeService;
            this.userDataService = userDataService;
            this.userProvider = userProvider;

        }

        [Authorize]
        [HttpPost("teacher/addGrade/{studentId}")]
        public IActionResult AddNewGradeToStudent([FromBody] NewGradeDTO newGradeDTO, int studentId)
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
                gradeService.AddGradeToStudent(newGradeDTO, teacherId, studentId);
            }
            catch(GradebookException exception)
            {
                return Forbid(exception.Message);
            }
            return Ok("Grade has been added");
        }
    }
}
