import axiosDefault from "./axiosDefault";

function getMyNotes() {
  return axiosDefault({
    method: "GET",
    url: "/api/student/myNotes"
  });
}

export { getMyNotes };
