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
    public class SubjectService : ISubjectService
    {
        public IRepository<SubjectDAO> subjectsRepository;
        public IRepository<ClassSubjectDAO> classSubjectRepository;

        public SubjectService(IRepository<SubjectDAO> subjectsRepository, IRepository<ClassSubjectDAO> classSubjectRepository)
        {
            this.subjectsRepository = subjectsRepository;
            this.classSubjectRepository = classSubjectRepository;
        }
        public SubjectListDTO GetSubjectListByClassId(int classId)
        {
            IEnumerable<ClassSubjectDAO> classesSubjects = classSubjectRepository.GetAll();
            SubjectListDTO subjectListDTO = new SubjectListDTO();
            foreach (ClassSubjectDAO classSubject in classesSubjects)
            {
                if (classSubject.ClassId == classId)
                {
                    subjectListDTO.SubjectList.Add(new SubjectDTO
                    {
                        Id = classSubject.SubjectId,
                        Name = subjectsRepository.Get(classSubject.SubjectId).Name
                    }
                    );
                }            
            }
            return subjectListDTO;
        }
    }
}
