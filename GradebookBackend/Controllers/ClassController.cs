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
    [Route("api/class")]
    public class ClassController : ControllerBase
    {
        private readonly IUserProviderService userProviderService;
        private readonly IUserService userService;
        private readonly IClassService classService;

        public ClassController(IUserProviderService userProviderService, IUserService userService, IClassService classService)
        {
            this.userProviderService = userProviderService;
            this.userService = userService;
            this.classService = classService;
        }

        [Authorize]
        [HttpGet("allClasses")]
        public IActionResult GetAllClasses()
        {
            int userId = int.Parse(userProviderService.GetUserId());
            if (userService.IsAdmin(userId))
            {
                ClassListDTO classListDTO = classService.GetAllClasses();
                return Ok(classListDTO);
            }
            else
            {
                return BadRequest("Brak Uprawnien administratora");
            }
        }

        [Authorize]
        [HttpGet("teacher/myClasses")]
        public IActionResult GetAllClassesByTeacherId()
        {
            int userId = int.Parse(userProviderService.GetUserId());
            try
            {
                int teacherId = userService.GetTeacherIdByUserId(userId);
                ClassListDTO classListDTO = classService.GetAllClassesOfTeacher(teacherId);
                return Ok(classListDTO);

            }
            catch (GradebookServerException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [Authorize]
        [HttpPost("admin/addClass")]
        public IActionResult AddClass([FromBody] ClassDTO newClassDTO)
        {
            try
            {
                int userId = int.Parse(userProviderService.GetUserId());
                if (userService.IsAdmin(userId))
                {
                    classService.AddClass(newClassDTO);
                    return Ok("Udalo sie dodac nowa klase");
                }
                else
                {
                    return BadRequest("Brak uprawnien do dodawania klasy");
                }
            }
            catch (GradebookServerException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [Authorize]
        [HttpDelete("admin/deleteClass/{classId}")]
        public IActionResult DeleteClassByClassId(int classId)
        {
            int userId = int.Parse(userProviderService.GetUserId());
            try
            {
                if (userService.IsAdmin(userId))
                {
                    classService.DeleteClassWithId(classId);
                    return Ok("Udalo sie usunac podana klase");
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

        [Authorize]
        [HttpPatch("admin/updateClass/{classId}")]
        public IActionResult UpdateClassById([FromBody] ClassDTO updatedClassDTO, int classId)
        {
            try
            {
                int userId = int.Parse(userProviderService.GetUserId());
                if (userService.IsAdmin(userId))
                {
                    classService.UpdateClass(updatedClassDTO, classId);
                    return Ok("Pomyslnie zaktualizowano klase");
                }
                else
                {
                    return BadRequest("Brak uprawnien administratora do wykonania tej operacji");
                }
            }
            catch (GradebookServerException exception)
            {
                return BadRequest(exception.Message);
            }
        }

    }
}
