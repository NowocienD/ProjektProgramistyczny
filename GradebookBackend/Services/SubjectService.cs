﻿using GradebookBackend.DTO;
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
        public IRepository<SubjectDAO> subjectRepository;
        public IRepository<ClassSubjectDAO> classSubjectRepository;

        public SubjectService(IRepository<SubjectDAO> subjectRepository, IRepository<ClassSubjectDAO> classSubjectRepository)
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
            foreach(SubjectDAO subject in subjects)
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
    }
}