import axiosDefault from "./axiosDefault";

function getMyPresence(date) {
  return axiosDefault({
    method: "GET",
    url: "/api/student/myAttendances",
    params: {
      day: date.slice(0,2),
      month: date.slice(3,5),
      year: date.slice(6,10),
    }
  });
}

export { getMyPresence };
