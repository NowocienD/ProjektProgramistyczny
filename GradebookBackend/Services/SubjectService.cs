using GradebookBackend.DTO;
using GradebookBackend.Model;
using GradebookBackend.Repositories;
using GradebookBackend.ServicesCore;
using System.Collections.Generic;

namespace GradebookBackend.Services
{
    public class SubjectService : ISubjectService
    {
        public IRepository<SubjectDAO> subjectRepository;
        public IRepositoryRelation<ClassSubjectDAO> classSubjectRepository;

        public SubjectService(IRepository<SubjectDAO> subjectRepository, IRepositoryRelation<ClassSubjectDAO> classSubjectRepository)
        {
            this.subjectRepository = subjectRepository;
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
                        Name = subjectRepository.Get(classSubject.SubjectId).Name
                    }
                    );
                }
            }
            return subjectListDTO;
        }

        public SubjectListDTO GetAllSubjects()
        {
            IEnumerable<SubjectDAO> subjects = subjectRepository.GetAll();
            SubjectListDTO subjectsDTO = new SubjectListDTO();
            foreach (SubjectDAO subject in subjects)
            {
                SubjectDTO subjectDTO = new SubjectDTO()
                {
                    Id = subject.Id,
                    Name = subject.Name
                };
                subjectsDTO.SubjectList.Add(subjectDTO);
            }
            return subjectsDTO;
        }

        public void AddNewSubject(SubjectDTO newSubjectDTO)
        {
            SubjectDAO newSubject = new SubjectDAO
            {
                Name = newSubjectDTO.Name
            };
            foreach (SubjectDAO checkedSubject in subjectRepository.GetAll())
            {
                if (checkedSubject.Name.Equals(newSubject.Name))
                {
                    throw new GradebookServerException("Proba dodania przedmiotu, którzy juz istnieje");
                }
            }
            subjectRepository.Add(newSubject);
        }

        public void UpdateSubject(SubjectDTO updatedSubjectDTO, int subjectId)
        {
            if (IsSubjectRepositoryContaining(subjectId))
            {
                SubjectDAO updatedSubjectDAO = new SubjectDAO
                {
                    Id = subjectId,
                    Name = updatedSubjectDTO.Name
                };
                subjectRepository.Update(updatedSubjectDAO);
            }
            else
            {
                throw new GradebookServerException("Nie ma przedmiotu o tym numerze Id");
            }

        }

        public void DeleteSubject(int subjectId)
        {
            if (IsSubjectRepositoryContaining(subjectId))
            {
                subjectRepository.Delete(subjectId);
            }
            else
            {
                throw new GradebookServerException("Nie ma przedmiotu o podanym Id");
            }
        }

        public bool IsSubjectRepositoryContaining(int subjectId)
        {
            foreach (SubjectDAO checkedSubject in subjectRepository.GetAll())
            {
                if (checkedSubject.Id == subjectId)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
