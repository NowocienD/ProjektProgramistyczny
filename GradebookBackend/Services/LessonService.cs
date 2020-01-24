﻿using GradebookBackend.DTO;
using GradebookBackend.Model;
using GradebookBackend.Repositories;
using GradebookBackend.ServicesCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Services
{
    public class LessonService : ILessonService
    {
        private readonly IRepository<LessonDAO> lessonsRepository;
        private readonly IRepository<SubjectDAO> subjectRepository;

        public LessonService(IRepository<LessonDAO> lessonsRepository, IRepository<SubjectDAO> subjectRepository)
        {
            this.lessonsRepository = lessonsRepository;
            this.subjectRepository = subjectRepository;
        }

        public LessonPlanDTO GetStudentLessonPlanByClassId(int classId)
        {
            LessonPlanDTO lessonPlanDTO = new LessonPlanDTO();
            IEnumerable<LessonDAO> lessons = lessonsRepository.GetAll();

            foreach (LessonDAO lesson in lessons)
            {
                if (lesson.ClassId == classId)
                {
                    lessonPlanDTO.LessonPlan[lesson.DayOfTheWeek].Lessons[lesson.LessonNumber] = subjectRepository.Get(lesson.SubjectId).Name;
                }
            }
            return lessonPlanDTO;
        }
        public LessonPlanDTO GetTeacherLessonPlanByTeacherId(int teacherId)
        {
            LessonPlanDTO lessonPlanDTO = new LessonPlanDTO();
            IEnumerable<LessonDAO> lessons = lessonsRepository.GetAll();

            foreach (LessonDAO lesson in lessons)
            {
                if (lesson.TeacherId == teacherId)
                {
                    lessonPlanDTO.LessonPlan[lesson.DayOfTheWeek].Lessons[lesson.LessonNumber] = subjectRepository.Get(lesson.SubjectId).Name;
                }
            }
            return lessonPlanDTO;
        }
        public SingleDayLessonPlanDTO GetSingleDayLessonPlanByDayOfTheWeekAndClassId(int dayOfTheWeek, int classId)
        {
            SingleDayLessonPlanDTO singleDayLessonPlanDTO = new SingleDayLessonPlanDTO();
            IEnumerable<LessonDAO> lessons = lessonsRepository.GetAll();
            foreach (LessonDAO lesson in lessons)
            {
                if (lesson.ClassId == classId && lesson.DayOfTheWeek == dayOfTheWeek)
                {
                    singleDayLessonPlanDTO.Lessons[lesson.LessonNumber] = subjectRepository.Get(lesson.SubjectId).Name;
                }
            }
            return singleDayLessonPlanDTO;
        }
        public int GetLessonId(int lessonNumber, int dayOfTheWeek, int classId)
        {
            IEnumerable<LessonDAO> lessons = lessonsRepository.GetAll();
            foreach(LessonDAO lesson in lessons)
            {
                if (lesson.LessonNumber == lessonNumber && lesson.DayOfTheWeek == dayOfTheWeek && lesson.ClassId == classId)
                {
                    return lesson.Id;
                }
            }
            throw new GradebookServerException("Nie znaleziono lekcji o przekazanych danych");
        }
        public bool CheckIfLessonExists(int lessonNumber, int dayOfTheWeek, int classId)
        {
            IEnumerable<LessonDAO> lessons = lessonsRepository.GetAll();
            foreach (LessonDAO lesson in lessons)
            {
                if (lesson.LessonNumber == lessonNumber && lesson.DayOfTheWeek == dayOfTheWeek && lesson.ClassId == classId)
                {
                    return true;
                }
            }
            return false;
        }
        public void AddNewLesson(NewLessonDTO newLessonDTO)
        {
            LessonDAO newLessonDAO = new LessonDAO
            {
                LessonNumber = newLessonDTO.LessonNumber,
                DayOfTheWeek = newLessonDTO.DayOfTheWeek,
                ClassId = newLessonDTO.ClassId,
                SubjectId = newLessonDTO.SubjectId,
                TeacherId = newLessonDTO.TeacherId,
            };
            lessonsRepository.Add(newLessonDAO);
        }
        public void UpdateLesson(NewLessonDTO updatedLessonDTO, int lessonId)
        {
            LessonDAO newLessonDAO = new LessonDAO
            {
                Id = lessonId,
                LessonNumber = updatedLessonDTO.LessonNumber,
                DayOfTheWeek = updatedLessonDTO.DayOfTheWeek,
                ClassId = updatedLessonDTO.ClassId,
                SubjectId = updatedLessonDTO.SubjectId,
                TeacherId = updatedLessonDTO.TeacherId,
            };
            lessonsRepository.Update(newLessonDAO);
        }
    }
}
