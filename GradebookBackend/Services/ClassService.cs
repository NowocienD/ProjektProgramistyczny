using GradebookBackend.DTO;
using GradebookBackend.Model;
using GradebookBackend.Repositories;
using GradebookBackend.ServicesCore;
using System.Collections.Generic;
using System.Linq;
using System;
using GradebookBackend.Logger.Service;

namespace GradebookBackend.Services
{
    public class ClassService : IClassService
    {
        private readonly IRepository<ClassDAO> classRepository;
        private readonly IRepository<LessonDAO> lessonRepository;
        private readonly ILogerService logerService;

        public ClassService(
            IRepository<ClassDAO> classRepository, 
            IRepository<LessonDAO> lessonRepository,
            ILogerService logerService)
        {
            this.classRepository = classRepository;
            this.lessonRepository = lessonRepository;
            this.logerService = logerService;
        }

        private ClassListDTO ClassDAOList_to_classDTOList(IEnumerable<ClassDAO> classDAOList)
        {
            var returnDTO = new ClassListDTO();
            classDAOList.ToList().ForEach(x => returnDTO.ClassList.Add(ClassDAO_to_classDTO(x))) ;
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

            lessonRepository.GetAll().Where(x => x.TeacherId == teacherId).ToList().ForEach(x =>
            {
                if (!classListDTO.ClassList.Any(o => o.Id == x.ClassId))
                {
                    classListDTO.ClassList.Add(new ClassDTO()
                    {
                        Id = x.ClassId,
                        Name = classRepository.Get(x.ClassId).Name
                    });
                }
            });

            return classListDTO;
        }

        public void AddClass(ClassDTO newClassDTO)
        {
            if (classRepository.GetAll().Any(x => x.Name.Equals(newClassDTO.Name)))
            {
                logerService.Debug("Klasa o tej nazwie juz istnieje", this);
                throw new GradebookServerException("Klasa o tej nazwie juz istnieje");
            }
            else
            {
                classRepository.Add(new ClassDAO() { Name = newClassDTO.Name });
            }
        }

        public void DeleteClass(int classId)
        {
            if (classRepository.GetAll().Any(x => x.Id == classId))
            {
                classRepository.Delete(classId);
            }
            else
            {
                logerService.Debug("Nie ma klasy o takim numerze Id", this);
                throw new GradebookServerException("Nie ma klasy o takim numerze Id");
            }
        }

        public void UpdateClass(ClassDTO updatedClassDTO, int classId)
        {
            if (classRepository.GetAll().Any(x => x.Id == classId))
            {
                classRepository.Update(new ClassDAO
                {
                    Id = classId,
                    Name = updatedClassDTO.Name
                });
            }
            else
            {
                logerService.Debug("Nie ma klasy o podanym numerze Id", this);
                throw new GradebookServerException("Nie ma klasy o podanym numerze Id");
            }
        }
    }
}

