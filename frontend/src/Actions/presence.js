import axiosDefault from "./axiosDefault";

function getMyPresence(date) {
  return axiosDefault({
    method: "GET",
    url: `/api/student/myAttendances/${date}`,
  });
}

export { getMyPresence };
