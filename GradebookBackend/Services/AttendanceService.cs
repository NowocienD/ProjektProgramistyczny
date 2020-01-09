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
    public class AttendanceService : IAttendanceService
    {
        private readonly IRepository<AttendanceDAO> attendanceRepository;
        private readonly IRepository<LessonDAO> lessonRepository;
        private readonly IRepository<StudentDAO> studentRepository;
        private readonly IRepository<UserDAO> userRepository;


        public AttendanceService(IRepository<AttendanceDAO> attendanceRepository, IRepository<LessonDAO> lessonRepository,
            IRepository<StudentDAO> studentRepository, IRepository<UserDAO> userRepository)
        {
            this.attendanceRepository = attendanceRepository;
            this.lessonRepository = lessonRepository;
            this.studentRepository = studentRepository;
            this.userRepository = userRepository;
        }
        public SingleDayAttendancesListDTO GetAttendancesByStudentId(int studentId, int day, int month, int year)
        {
            if(day == 0 || month == 0 || year == 0)
            {
                throw new GradebookServerException("day, month and year can't be equal 0");
            }
            IEnumerable<AttendanceDAO> attendances = attendanceRepository.GetAll();
            SingleDayAttendancesListDTO attendancesPlanDTO = new SingleDayAttendancesListDTO();
            DateTime firstDate = new DateTime(year, month, day);
            DateTime secondDate = firstDate.AddDays(5);
                foreach(AttendanceDAO attendance in attendances)
                {
                    if(attendance.StudentId == studentId && attendance.Date >= firstDate && attendance.Date <= secondDate)
                    {
                        int dayOfTheWeek = lessonRepository.Get(attendance.LessonId).DayOfTheWeek;
                        int lessonNumber = lessonRepository.Get(attendance.LessonId).LessonNumber;
                        attendancesPlanDTO.AttendancesPlan[dayOfTheWeek].Attendances[lessonNumber] = attendance.State;
                    }
                }
            return attendancesPlanDTO;
        }
        public SingleLessonAttendancesListDTO  GetClassAttendances(int teacherId, int classId, int lessonId, int day, int month, int year)
        {
            if (day == 0 || month == 0 || year == 0 || classId == 0 || lessonId == 0)
            {
                throw new GradebookServerException("day, month, year, classId and lessonId can't be equal 0");
            }
            SingleLessonAttendancesListDTO singleLessonAttendancesListDTO = new SingleLessonAttendancesListDTO();
            DateTime date = new DateTime(year, month, day);
            IEnumerable<StudentDAO> students = studentRepository.GetAll();
            IEnumerable<AttendanceDAO> attendances = attendanceRepository.GetAll();
            bool attendanceEntered = false;
            foreach (StudentDAO student in students)
            {
                if(student.ClassId == classId)
                {
                    foreach(AttendanceDAO attendance in attendances)
                    {
                        if(attendance.StudentId == student.Id && attendance.LessonId == lessonId && attendance.Date == date)
                        {
                            singleLessonAttendancesListDTO.SingleLessonAttendances.Add(new SingleLessonAttendanceDTO
                            {
                                Id = student.Id,
                                Name = userRepository.Get(student.UserId).Firstname + userRepository.Get(student.UserId).Surname,
                                Attendance = attendance.State
                            });
                            attendanceEntered = true;
                            break;
                        }
                    }
                    if (!attendanceEntered)
                    {
                        singleLessonAttendancesListDTO.SingleLessonAttendances.Add(new SingleLessonAttendanceDTO
                        {
                            Id = student.Id,
                            Name = userRepository.Get(student.UserId).Firstname + userRepository.Get(student.UserId).Surname,
                            Attendance = "not entered"
                        });
                    }
                    attendanceEntered = false;
                }
            }
            return singleLessonAttendancesListDTO;
        }
    }
}
