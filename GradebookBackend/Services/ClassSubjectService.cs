using GradebookBackend.DTO;
using GradebookBackend.Model;
using GradebookBackend.Repositories;
using GradebookBackend.ServicesCore;
using System.Collections.Generic;

namespace GradebookBackend.Services
{
    public class ClassSubjectService : IClassSubjectService
    {
        public IRepositoryRelation<ClassSubjectDAO> classSubjectRepository;
        public IRepository<SubjectDAO> subjectRepository;

        public ClassSubjectService(IRepositoryRelation<ClassSubjectDAO> classSubjectRepository,
            IRepository<SubjectDAO> subjectRepository)
        {
            this.classSubjectRepository = classSubjectRepository;
            this.subjectRepository = subjectRepository;
        }

        public void AddClassSubject(int classId, int subjectId)
        {
            ClassSubjectDAO newClassSubjectDAO = new ClassSubjectDAO
            {
                ClassId = classId,
                SubjectId = subjectId
            };
            classSubjectRepository.Add(newClassSubjectDAO);
        }

        public void DeleteClassSubject(int classId, int subjectId)
        {
            classSubjectRepository.Delete(classId, subjectId);
        }

        public SubjectListDTO GetSubjectsAssignedToClass(int classId)
        {
            SubjectListDTO subjectListDTO = new SubjectListDTO();
            IEnumerable<ClassSubjectDAO> classSubjects = classSubjectRepository.GetAll();
            foreach (ClassSubjectDAO classSubject in classSubjects)
            {
                if (classSubject.ClassId == classId)
                {
                    subjectListDTO.SubjectList.Add(new SubjectDTO
                    {
                        Id = classSubject.SubjectId,
                        Name = subjectRepository.Get(classSubject.SubjectId).Name
                    });
                }
            }
            return subjectListDTO;
        }
    }
}
