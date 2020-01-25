using GradebookBackend.Model;
using GradebookBackend.Repositories;
using GradebookBackend.ServicesCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Services
{
    public class ClassSubjectService : IClassSubjectService
    {
        public IRepositoryRelation<ClassSubjectDAO> classSubjectRepository;
        public ClassSubjectService(IRepositoryRelation<ClassSubjectDAO> classSubjectRepository)
        {
            this.classSubjectRepository = classSubjectRepository;
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
    }
}
