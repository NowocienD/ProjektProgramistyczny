﻿using GradebookBackend.DTO;
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
    [Route("api/lesson")]
    public class LessonController : ControllerBase
    {
        private readonly IUserProviderService userProviderService;
        private readonly IUserService userService;
        private readonly ILessonService lessonService;
        private readonly IStudentService studentService;

        public LessonController(IUserProviderService userProviderService, IUserService userService,
            ILessonService lessonService, IStudentService studentService)
        {
            this.userService = userService;
            this.userProviderService = userProviderService;
            this.lessonService = lessonService;
            this.studentService = studentService;
        }

        [Authorize]
        [HttpGet("student/myLessonPlan")]
        public IActionResult GetStudentLessonPlan()
        {
            try
            {
                int userId = int.Parse(userProviderService.GetUserId());
                LessonPlanDTO lessonPlanDTO = lessonService.GetStudentLessonPlanByClassId(
                studentService.GetStudentClassIdByStudentId(userService.GetStudentIdByUserId(userId)));
                return Ok(lessonPlanDTO);
            }
            catch (GradebookServerException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [Authorize]
        [HttpGet("teacher/myLessonPlan")]
        public IActionResult GetTeacherLessonPlan()
        {
            try
            {
                int userId = int.Parse(userProviderService.GetUserId());
                int teacherId = userService.GetTeacherIdByUserId(userId);
                LessonPlanDTO lessonPlanDTO = lessonService.GetTeacherLessonPlanByTeacherId(teacherId);
                return Ok(lessonPlanDTO);
            }
            catch (GradebookServerException exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [Authorize]
        [HttpGet("admin/LessonPlan")]
        public IActionResult GetLessonPlan([FromQuery] int dayOfTheWeek, [FromQuery] int classId)
        {
            int userId = int.Parse(userProviderService.GetUserId());
            if (userService.IsAdmin(userId))
            {
                SingleDayLessonPlanDTO singleDaylessonPlanDTO = lessonService.
                    GetSingleDayLessonPlanByDayOfTheWeekAndClassId(dayOfTheWeek, classId);
                return Ok(singleDaylessonPlanDTO);
            }
            else
            {
                return BadRequest("Brak uprawnien administratora");
            }
        }

        [Authorize]
        [HttpPost("admin/addlesson")]
        public IActionResult AddLesson([FromBody] NewLessonDTO newLessonDTO)
        {
            int userId = int.Parse(userProviderService.GetUserId());
            if (userService.IsAdmin(userId))
            {
                if (lessonService.CheckIfLessonExists(newLessonDTO.LessonNumber, newLessonDTO.DayOfTheWeek, newLessonDTO.ClassId))
                {
                    int lessonId = lessonService.GetLessonId(newLessonDTO.LessonNumber, newLessonDTO.DayOfTheWeek, newLessonDTO.ClassId);
                    lessonService.UpdateLesson(newLessonDTO, lessonId);
                    return Ok("Pomyslnie zaktualizowano istniejaca lekcje");
                }
                else
                {
                    lessonService.AddLesson(newLessonDTO);
                    return Ok("Pomyslnie dodano nowa lekcje");
                }
            }
            else
            {
                return BadRequest("Brak uprawnien administratora");
            }
        }

        [Authorize]
        [HttpDelete("admin/deletelesson")]
        public IActionResult DaleteLesson([FromBody] NewLessonDTO newLessonDTO)
        {
            int userId = int.Parse(userProviderService.GetUserId());
            if (userService.IsAdmin(userId))
            {
                if (lessonService.CheckIfLessonExists(newLessonDTO.LessonNumber, newLessonDTO.DayOfTheWeek, newLessonDTO.ClassId))
                {
                    int lessonId = lessonService.GetLessonId(newLessonDTO.LessonNumber, newLessonDTO.DayOfTheWeek, newLessonDTO.ClassId);
                    lessonService.DeleteLesson(lessonId);
                    return Ok("Pomyslnie usunieto lekcje");
                }
                else
                {
                    return BadRequest("Lekcja ktora ma byc usunieta nie istnieje");
                }
            }
            else
            {
                return BadRequest("Brak uprawnien administratora");
            }
        }
    }
}
