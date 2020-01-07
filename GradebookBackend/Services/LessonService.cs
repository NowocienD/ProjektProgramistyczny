using GradebookBackend.DTO;
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
    }
}
