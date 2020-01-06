import axiosDefault from "./axiosDefault";

function getNotes() {
  return axiosDefault({
    method: "GET",
    url: "/api/student/myNotes"
  });
}

export { getNotes };
