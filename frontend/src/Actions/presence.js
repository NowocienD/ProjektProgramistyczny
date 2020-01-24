import axiosDefault from "./axiosDefault";

function getMyPresence(date) {
  return axiosDefault({
    method: "GET",
    url: "/api/attendance/student/myAttendances",
    params: {
      day: date.slice(0,2),
      month: date.slice(3,5),
      year: date.slice(6,10),
    }
  });
}

function getClassPresence(date, classId, lessonNumber) {
  console.log(date);
  return axiosDefault({
    method: "GET",
    url: "/api/attendance/teacher/classAttendances",
    params: {
      day: date.slice(8,11),
      month: date.slice(5,7),
      year: date.slice(0,4),
      classId: classId,
      lessonNumber: lessonNumber,
    }
  });
}

function addPresence(data, studentId) {
  return axiosDefault({
    method: "POST",
    url: `/api/attendance/teacher/addAttendance/${studentId}`,
    data: data,
  });
}

export { getMyPresence, getClassPresence, addPresence };
