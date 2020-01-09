import axiosDefault from "./axiosDefault";

function getPresenceStatuses() {
  return axiosDefault({
    method: "GET",
    url: "/api/attendanceStatus/allAttendanceStatus",
  });
}

export { getPresenceStatuses }