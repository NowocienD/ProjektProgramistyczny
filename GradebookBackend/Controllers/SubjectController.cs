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
    [Route("api/subject")]
    public class SubjectController : ControllerBase
    {
        private readonly IUserProviderService userProviderService;
        private readonly IUserService userService;
        private readonly IStudentService studentService;
        private readonly ISubjectService subjectService;

        public SubjectController(IUserProviderService userProviderService, IUserService userService,
            ISubjectService subjectService, IStudentService studentService)
        {
            this.userProviderService = userProviderService;
            this.userService = userService;
            this.subjectService = subjectService;
            this.studentService = studentService;
        }

        [Authorize]
        [HttpGet("student/mySubjects")]
        public IActionResult GetStudentSubjects()
        {
            int userId = int.Parse(userProviderService.GetUserId());
            SubjectListDTO subjectListDTO = subjectService.GetSubjectListByClassId(
                studentService.GetStudentClassIdByStudentId(userService.GetStudentIdByUserId(userId)));
            return Ok(subjectListDTO);
        }

        [Authorize]
        [HttpGet("subjectsFromClass/{classId}")]
        public IActionResult GetClassSubjects(int classId)
        {
            int userId = int.Parse(userProviderService.GetUserId());
            if (userService.IsTeacher(userId) || userService.IsAdmin(userId))
            {
                SubjectListDTO subjectListDTO = subjectService.GetSubjectListByClassId(classId);
                return Ok(subjectListDTO);
            }
            else
            {
                return BadRequest("Logged user is not teacher or admin");
            }
        }


        [Authorize]
        [HttpGet("allSubjects")]
        public IActionResult GetAllSubjects()
        {
            int userId = int.Parse(userProviderService.GetUserId());
            if (userService.IsAdmin(userId))
            {
                SubjectListDTO subjectListDTO = subjectService.GetAllSubjects();
                return Ok(subjectListDTO);
            }
            else
            {
                return BadRequest("Logged user is not admin");
            }

        }

        [Authorize]
        [HttpPost("admin/addNewSubject")]
        public IActionResult AddNewSubject([FromBody]SubjectDTO newSubjectDTO)
        {
            try
            {
                int userId = int.Parse(userProviderService.GetUserId());
                if (userService.IsAdmin(userId))
                {
                    subjectService.AddNewSubject(newSubjectDTO);
                    return Ok("Pomyslnie dodano przedmiot");
                }
                else
                {
                    return BadRequest("Uzytkownik nie jest administratorem");
                }

            }
            catch (GradebookServerException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("admin/deleteSubject/{subjectId}")]
        public IActionResult DeleteSubject(int subjectId)
        {
            try
            {
                int userId = int.Parse(userProviderService.GetUserId());
                if (userService.IsAdmin(userId))
                {
                    subjectService.DeleteSubject(subjectId);
                    return Ok("Pomyslnie usunieto przedmiot");
                }
                else
                {
                    return BadRequest("Zalogowany uzytkownik nie jest administratorem");
                }

            }
            catch (GradebookServerException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("admin/updateSubject/{subjectId}")]
        public IActionResult UpdateSubject([FromBody] SubjectDTO updatedSubjectDTO, int subjectId)
        {
            try
            {
                int userId = int.Parse(userProviderService.GetUserId());
                if (userService.IsAdmin(userId))
                {
                    subjectService.UpdateSubject(updatedSubjectDTO, subjectId);
                    return Ok("Pomyslnie zaktualizowano przedmiot");
                }
                else
                {
                    return BadRequest("Brak uprawnien administratora do wykonania tej operacji");
                }
            }
            catch (GradebookServerException ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
