import axiosDefault from "./axiosDefault";

function getMyPresence(dates) {
  return axiosDefault({
    method: "GET",
    url: "/api/student/myAttendances",
    data: dates,
  });
}

export { getMyPresence };
