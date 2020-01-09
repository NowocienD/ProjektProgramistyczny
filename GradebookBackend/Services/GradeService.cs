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
        private readonly IRepository<TeacherSubjectDAO> teacherSubjectsRepository;

        public GradeService(IRepository<GradeDAO> gradesRepository, IRepository<UserDAO> usersRepository,
            IRepository<TeacherDAO> teachersRepository, IRepository<TeacherSubjectDAO> teacherSubjectsRepository)
        {
            this.gradesRepository = gradesRepository;
            this.usersRepository = usersRepository;
            this.teachersRepository = teachersRepository;
            this.teacherSubjectsRepository = teacherSubjectsRepository;
        }
        public bool CheckIfTeacherTeachSubject(int teacherId, int subjectId)
        {
            IEnumerable<TeacherSubjectDAO> teacherSubjectDAOs = teacherSubjectsRepository.GetAll();
            foreach(TeacherSubjectDAO teacherSubject in teacherSubjectDAOs)
            {
                if(teacherSubject.SubjectId == subjectId && teacherSubject.TeacherId == teacherId)
                {
                    return true;
                }
            }
            return false;
        }
        public void AddGrade(NewGradeDTO newGradeDTO, int teacherId, int studentId )
        {
            if(!CheckIfTeacherTeachSubject(teacherId, newGradeDTO.SubjectId))
            {
                throw new GradebookServerException("Nie mozna dodac oceny poniewaz nauczyciel nie uczy tego przedmiotu");
            }
            GradeDAO newGradeDAO = new GradeDAO
            {
                Date = DateTime.ParseExact(newGradeDTO.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture),
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
            if (!CheckIfTeacherTeachSubject(teacherId, updatedGradeDTO.SubjectId))
            {
                throw new GradebookServerException("Nie mozna zaktualizowac oceny poniewaz nauczyciel nie uczy tego przedmiotu");
            }
            GradeDAO updatedGradeDAO = new GradeDAO
            {
                Id = updatedGradeDTO.Id,
                Date = DateTime.ParseExact(updatedGradeDTO.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture),
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
                        Id = grade.Id,
                        Value = grade.Value,
                        Importance = grade.Importance,
                        Topic = grade.Topic,
                        Date = grade.Date.ToString("yyyy-MM-dd"),
                        TeacherFirstname = usersRepository.Get(teachersRepository.Get(grade.TeacherId).UserId).Firstname,
                        TeacherSurname = usersRepository.Get(teachersRepository.Get(grade.TeacherId).UserId).Surname,
                        TeacherFullname = usersRepository.Get(teachersRepository.Get(grade.TeacherId).UserId).Firstname
                        + " " + usersRepository.Get(teachersRepository.Get(grade.TeacherId).UserId).Surname
                    });
                }
            }
            return gradeListDTO;
        }
    }
}
