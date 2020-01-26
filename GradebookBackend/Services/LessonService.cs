using GradebookBackend.DTO;
using GradebookBackend.Model;
using GradebookBackend.Repositories;
using GradebookBackend.ServicesCore;
using System.Collections.Generic;

namespace GradebookBackend.Services
{
    public class LessonService : ILessonService
    {
        private readonly IRepository<LessonDAO> lessonsRepository;
        private readonly IRepository<SubjectDAO> subjectRepository;
        private readonly IRepository<UserDAO> usersRepository;
        private readonly IRepository<TeacherDAO> teachersRepository;

        public LessonService(IRepository<LessonDAO> lessonsRepository, IRepository<SubjectDAO> subjectRepository,
            IRepository<UserDAO> usersRepository, IRepository<TeacherDAO> teachersRepository)
        {
            this.lessonsRepository = lessonsRepository;
            this.subjectRepository = subjectRepository;
            this.usersRepository = usersRepository;
            this.teachersRepository = teachersRepository;
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
        public SingleDayLessonPlanExtendedDTO GetSingleDayLessonPlanByDayOfTheWeekAndClassId(int dayOfTheWeek, int classId)
        {
            SingleDayLessonPlanExtendedDTO singleDayLessonPlanDTO = new SingleDayLessonPlanExtendedDTO();
            IEnumerable<LessonDAO> lessons = lessonsRepository.GetAll();
            foreach (LessonDAO lesson in lessons)
            {
                if (lesson.ClassId == classId && lesson.DayOfTheWeek == dayOfTheWeek)
                {
                    int userId = teachersRepository.Get(lesson.TeacherId).UserId;
                    singleDayLessonPlanDTO.Lessons[lesson.LessonNumber].Id = lesson.Id;
                    singleDayLessonPlanDTO.Lessons[lesson.LessonNumber].LessonNumber = lesson.LessonNumber;
                    singleDayLessonPlanDTO.Lessons[lesson.LessonNumber].Name = subjectRepository.Get(lesson.SubjectId).Name;
                    singleDayLessonPlanDTO.Lessons[lesson.LessonNumber].TeacherName = usersRepository.Get(userId).Firstname + " " + usersRepository.Get(userId).Surname;
                }
            }
            return singleDayLessonPlanDTO;
        }
        public int GetLessonId(int lessonNumber, int dayOfTheWeek, int classId)
        {
            IEnumerable<LessonDAO> lessons = lessonsRepository.GetAll();
            foreach (LessonDAO lesson in lessons)
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
        public void AddLesson(NewLessonDTO newLessonDTO)
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
        public void DeleteLesson(int lessonId)
        {
            lessonsRepository.Delete(lessonId);
        }
        public LessonDTO GetLessonByLessonId(int lessonId)
        {
            LessonDAO lesson = lessonsRepository.Get(lessonId);
            int userId = teachersRepository.Get(lesson.TeacherId).UserId;
            LessonDTO lessonDTO = new LessonDTO
            {
                Id = lessonId,
                LessonNumber = lesson.LessonNumber,
                Name = subjectRepository.Get(lesson.SubjectId).Name,
                TeacherName = usersRepository.Get(userId).Firstname + " " + usersRepository.Get(userId).Surname
            };
            return lessonDTO;
        }
    }
}
