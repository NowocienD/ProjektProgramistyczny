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
    public class ClassService : IClassService
    {
        private readonly IRepository<ClassDAO> classRepository;
        private readonly IRepository<LessonDAO> lessonRepository;

        public ClassService(IRepository<ClassDAO> classRepository, IRepository<LessonDAO> lessonRepository)
        {
            this.classRepository = classRepository;
            this.lessonRepository = lessonRepository;
        }

        public ClassListDTO GetAllClasses()
        {
            IEnumerable<ClassDAO> classes = classRepository.GetAll();
            ClassListDTO classesDTO = new ClassListDTO();
            foreach(ClassDAO classDAO in classes)
            {
                ClassDTO classDTO = new ClassDTO()
                {
                    Id = classDAO.Id,
                    Name = classDAO.Name
                };
                classesDTO.ClassList.Add(classDTO);
            }
            return classesDTO;
        }

        public ClassListDTO GetAllClassesOfTeacher(int teacherId)
        {
            ClassListDTO classListDTO = new ClassListDTO();
            IEnumerable<LessonDAO> lessons = lessonRepository.GetAll();
            foreach(LessonDAO lesson in lessons)
            {
                if(lesson.TeacherId == teacherId)
                {
                    ClassDTO newClass = new ClassDTO()
                    {
                        Id = lesson.ClassId,
                        Name = classRepository.Get(lesson.ClassId).Name
                    };
                }
            }
            return classListDTO;
        }
    }
}
