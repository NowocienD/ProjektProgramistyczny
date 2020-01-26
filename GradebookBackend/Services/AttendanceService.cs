using GradebookBackend.DTO;
using GradebookBackend.Model;
using GradebookBackend.Repositories;
using GradebookBackend.ServicesCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GradebookBackend.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IRepository<AttendanceDAO> attendanceRepository;
        private readonly IRepository<LessonDAO> lessonRepository;
        private readonly IRepository<StudentDAO> studentRepository;
        private readonly IRepository<UserDAO> userRepository;
        private readonly IRepository<AttendanceStatusDAO> attendanceStatusRepository;

        public AttendanceService(IRepository<AttendanceDAO> attendanceRepository, IRepository<LessonDAO> lessonRepository,
            IRepository<StudentDAO> studentRepository, IRepository<UserDAO> userRepository,
            IRepository<AttendanceStatusDAO> attendanceStatusRepository)
        {
            this.attendanceRepository = attendanceRepository;
            this.lessonRepository = lessonRepository;
            this.studentRepository = studentRepository;
            this.userRepository = userRepository;
            this.attendanceStatusRepository = attendanceStatusRepository;
        }
        public SingleDayAttendancesListDTO GetAttendancesByStudentId(int studentId, DateTime firstDate)
        {
            IEnumerable<AttendanceDAO> attendances = attendanceRepository.GetAll();
            SingleDayAttendancesListDTO attendancesPlanDTO = new SingleDayAttendancesListDTO();
            DateTime secondDate = firstDate.AddDays(5);
            foreach (AttendanceDAO attendance in attendances)
            {
                if (attendance.StudentId == studentId && attendance.Date >= firstDate && attendance.Date <= secondDate)
                {
                    int dayOfTheWeek = lessonRepository.Get(attendance.LessonId).DayOfTheWeek;
                    int lessonNumber = lessonRepository.Get(attendance.LessonId).LessonNumber;
                    attendancesPlanDTO.AttendancesPlan[dayOfTheWeek].Attendances[lessonNumber] = attendanceStatusRepository.Get(attendance.AttendanceStatusId).Name;
                }
            }
            return attendancesPlanDTO;
        }
        public SingleLessonAttendancesListDTO GetClassAttendances(int classId, int lessonId, DateTime date)
        {
            SingleLessonAttendancesListDTO singleLessonAttendancesListDTO = new SingleLessonAttendancesListDTO();
            IEnumerable<StudentDAO> students = studentRepository.GetAll();
            IEnumerable<AttendanceDAO> attendances = attendanceRepository.GetAll();
            bool attendanceEntered = false;
            foreach (StudentDAO student in students)
            {
                if (student.ClassId == classId)
                {
                    foreach (AttendanceDAO attendance in attendances)
                    {
                        if (attendance.StudentId == student.Id && attendance.LessonId == lessonId && attendance.Date == date)
                        {
                            singleLessonAttendancesListDTO.SingleLessonAttendances.Add(new SingleLessonAttendanceDTO
                            {
                                StudentId = student.Id,
                                Name = userRepository.Get(student.UserId).Firstname + " " + userRepository.Get(student.UserId).Surname,
                                AttendanceStatusId = attendance.AttendanceStatusId,
                                AttendanceStatus = attendanceStatusRepository.Get(attendance.AttendanceStatusId).Name
                            });
                            attendanceEntered = true;
                            break;
                        }
                    }
                    if (!attendanceEntered)
                    {
                        singleLessonAttendancesListDTO.SingleLessonAttendances.Add(new SingleLessonAttendanceDTO
                        {
                            StudentId = student.Id,
                            Name = userRepository.Get(student.UserId).Firstname + " " + userRepository.Get(student.UserId).Surname,
                            AttendanceStatus = "nie wpisano"
                        });
                    }
                    attendanceEntered = false;
                }
            }
            return singleLessonAttendancesListDTO;
        }
        public void AddUpdateDeleteAttendance(DateTime date, int attendanceStatusId, int lessonId, int studentId)
        {
            IEnumerable<AttendanceDAO> attendances = attendanceRepository.GetAll();
            foreach (AttendanceDAO attendance in attendances.ToList())
            {
                if (attendance.Date == date && attendance.LessonId == lessonId && attendance.StudentId == studentId)
                {
                    if (attendanceStatusId == 0)
                    {
                        attendanceRepository.Delete(attendance.Id);
                    }
                    else
                    {
                        AttendanceDAO updatedAttendanceDAO = new AttendanceDAO
                        {
                            Id = attendance.Id,
                            AttendanceStatusId = attendanceStatusId,
                            StudentId = studentId,
                            LessonId = lessonId,
                            Date = date
                        };
                        attendanceRepository.Update(updatedAttendanceDAO);
                    }
                    return;
                }
            }
            AttendanceDAO newAttendanceDAO = new AttendanceDAO
            {
                AttendanceStatusId = attendanceStatusId,
                StudentId = studentId,
                LessonId = lessonId,
                Date = date
            };
            attendanceRepository.Add(newAttendanceDAO);
        }
    }
}
