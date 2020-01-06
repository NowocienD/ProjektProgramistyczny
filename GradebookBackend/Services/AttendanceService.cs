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


        public AttendanceService(IRepository<AttendanceDAO> attendanceRepository, IRepository<LessonDAO> lessonRepository)
        {
            this.attendanceRepository = attendanceRepository;
            this.lessonRepository = lessonRepository;
        }
        public SingleDayAttendancesListDTO GetAttendancesByStudentId(int studentId, string date)
        {
            IEnumerable<AttendanceDAO> attendances = attendanceRepository.GetAll();
            SingleDayAttendancesListDTO attendancesPlanDTO = new SingleDayAttendancesListDTO();
            DateTime firstDate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
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
    }
}
