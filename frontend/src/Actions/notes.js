import axiosDefault from "./axiosDefault";

function getMyNotes() {
  return axiosDefault({
    method: "GET",
    url: "/api/note/student/myNotes"
  });
}

export { getMyNotes };
