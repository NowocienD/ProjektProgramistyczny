using GradebookBackend.DTO;
using GradebookBackend.Model;
using GradebookBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Services
{
    public class ClassService
    {
        private readonly IRepository<ClassDAO> classRepository;

        public ClassService(IRepository<ClassDAO> classRepository)
        {
            this.classRepository = classRepository;
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
    }
}
