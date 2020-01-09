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

function getClassPresence(date) {
  return axiosDefault({
    method: "GET",
    url: "/api/attendance/teacher/classAttendances",
    params: {
      day: date.slice(0,2),
      month: date.slice(3,5),
      year: date.slice(6,10),
    }
  });
}

export { getMyPresence };
