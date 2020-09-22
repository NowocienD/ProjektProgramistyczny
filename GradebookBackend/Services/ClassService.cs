using GradebookBackend.DTO;
using GradebookBackend.Model;
using GradebookBackend.Repositories;
using GradebookBackend.ServicesCore;
using System;
using System.Collections.Generic;
using System.Linq;

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

        private ClassListDTO ClassDAOList_to_classDTOList(IEnumerable<ClassDAO> classDAOList)
        {
            var returnDTO = new ClassListDTO();
            classDAOList.ToList().ForEach((x => returnDTO.ClassList.Add(ClassDAO_to_classDTO(x))));
            return returnDTO;
        }
        private ClassDTO ClassDAO_to_classDTO(ClassDAO classDAO)
        {
            return new ClassDTO()
                {
                    Id = classDAO.Id,
                    Name = classDAO.Name
                };
        }

        public ClassListDTO GetAllClasses()
        {
            return ClassDAOList_to_classDTOList(classRepository.GetAll());
        }

        public ClassListDTO GetAllClassesOfTeacher(int teacherId)
        {
            ClassListDTO classListDTO = new ClassListDTO();
            IEnumerable<LessonDAO> lessons = lessonRepository.GetAll();
            foreach (LessonDAO lesson in lessons)
            {
                if (lesson.TeacherId == teacherId)
                {
                    ClassDTO newClass = new ClassDTO()
                    {
                        Id = lesson.ClassId,
                        Name = classRepository.Get(lesson.ClassId).Name
                    };
                    if (!IsClassAlreadyInClassList(classListDTO, newClass))
                    {
                        classListDTO.ClassList.Add(newClass);
                    }
                }
            }
            return classListDTO;
        }

        public bool IsClassAlreadyInClassList(ClassListDTO classListDTO, ClassDTO classDTO)
        {
            bool isAlready = false;
            foreach (ClassDTO checkedClass in classListDTO.ClassList)
            {
                if (checkedClass.Id == classDTO.Id)
                {
                    isAlready = true;
                }
            }
            return isAlready;
        }

        public void AddClass(ClassDTO newClassDTO)
        {
            ClassDAO newClassDAO = new ClassDAO
            {
                Name = newClassDTO.Name
            };
            foreach (ClassDAO checkedClass in classRepository.GetAll().ToList())
            {
                if (checkedClass.Name.Equals(newClassDAO.Name))
                {
                    throw new GradebookServerException("Klasa o tej nazwie juz istnieje");
                }
            }
            classRepository.Add(newClassDAO);
        }

        public void DeleteClass(int classId)
        {
            IEnumerable<ClassDAO> classes = classRepository.GetAll();
            foreach (ClassDAO checkedClass in classes.ToList())
            {
                if (checkedClass.Id == classId)
                {
                    classRepository.Delete(classId);
                    return;
                }
            }
            throw new GradebookServerException("Nie ma klasy o takim numerze Id");
        }

        public void UpdateClass(ClassDTO updatedClassDTO, int classId)
        {
            if (IsClassRepositoryContaining(classId))
            {
                ClassDAO updatedClassDAO = new ClassDAO
                {
                    Id = classId,
                    Name = updatedClassDTO.Name
                };
                classRepository.Update(updatedClassDAO);
            }
            else
            {
                throw new GradebookServerException("Nie ma klasy o podanym numerze Id");
            }
        }

        private bool IsClassRepositoryContaining(int classId)
        {
            foreach (ClassDAO checkedClass in classRepository.GetAll().ToList())
            {
                if (checkedClass.Id == classId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

