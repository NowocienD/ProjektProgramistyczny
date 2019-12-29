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
    public class GradeService : IGradeService
    {
        private readonly IRepository<GradeDAO> gradesRepository;

        public GradeService(IRepository<GradeDAO> gradesRepository)
        {
            this.gradesRepository = gradesRepository;
        }
        public void AddGrade(NewGradeDTO newGradeDTO, int teacherId, int studentId )
        {
            GradeDAO newGradeDAO = new GradeDAO
            {
                Date = newGradeDTO.Date,
                Importance = newGradeDTO.Importance,
                Topic = newGradeDTO.Topic,
                Value = newGradeDTO.Value,
                SubjectId = newGradeDTO.SubjectId,
                StudentId = studentId,
                TeacherId = teacherId
            };
            gradesRepository.Add(newGradeDAO);
        }
        public void DeleteGrade(int gradeId)
        {
            gradesRepository.Delete(gradeId);
        }

        public void UpdateGrade(NewGradeDTO updatedGradeDTO, int teacherId, int studentId)
        {
            GradeDAO updatedGradeDAO = new GradeDAO
            {
                Id = updatedGradeDTO.Id,
                Date = updatedGradeDTO.Date,
                Importance = updatedGradeDTO.Importance,
                Topic = updatedGradeDTO.Topic,
                Value = updatedGradeDTO.Value,
                SubjectId = updatedGradeDTO.SubjectId,
                StudentId = studentId,
                TeacherId = teacherId
            };
            gradesRepository.Update(updatedGradeDAO);
        }
    }
}
