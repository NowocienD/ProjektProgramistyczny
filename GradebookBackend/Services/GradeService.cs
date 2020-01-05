using GradebookBackend.DTO;
using GradebookBackend.Model;
using GradebookBackend.Repositories;
using GradebookBackend.ServicesCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Services
{
    public class GradeService : IGradeService
    {
        private readonly IRepository<GradeDAO> gradesRepository;
        private readonly IRepository<UserDAO> usersRepository;
        private readonly IRepository<TeacherDAO> teachersRepository;

        public GradeService(IRepository<GradeDAO> gradesRepository, IRepository<UserDAO> usersRepository,
            IRepository<TeacherDAO> teachersRepository)
        {
            this.gradesRepository = gradesRepository;
            this.usersRepository = usersRepository;
            this.teachersRepository = teachersRepository;
        }
        public void AddGrade(NewGradeDTO newGradeDTO, int teacherId, int studentId )
        {
            GradeDAO newGradeDAO = new GradeDAO
            {
                Date = DateTime.ParseExact(newGradeDTO.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture),
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
                Date = DateTime.ParseExact(updatedGradeDTO.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                Importance = updatedGradeDTO.Importance,
                Topic = updatedGradeDTO.Topic,
                Value = updatedGradeDTO.Value,
                SubjectId = updatedGradeDTO.SubjectId,
                StudentId = studentId,
                TeacherId = teacherId
            };
            gradesRepository.Update(updatedGradeDAO);
        }

        public GradeListDTO GetStudentGradesByStudentId(int studentId, int subjectId)
        {
            IEnumerable<GradeDAO> grades = gradesRepository.GetAll();
            GradeListDTO gradeListDTO = new GradeListDTO();
            foreach (GradeDAO grade in grades)
            {
                if (grade.StudentId == studentId && grade.SubjectId == subjectId)
                {
                    gradeListDTO.GradeDTOs.Add(new GradeDTO
                    {
                        Value = grade.Value,
                        Importance = grade.Importance,
                        Topic = grade.Topic,
                        Date = grade.Date.ToString(),
                        TeacherFirstname = usersRepository.Get(teachersRepository.Get(grade.TeacherId).UserId).Firstname,
                        TeacherSurname = usersRepository.Get(teachersRepository.Get(grade.TeacherId).UserId).Surname
                    });
                }
            }
            return gradeListDTO;
        }
    }
}
