import axiosDefault from "./axiosDefault";

function getMyNotes() {
  return axiosDefault({
    method: "GET",
    url: "/api/note/student/myNotes",
  });
}

function getStudentNotes(studentId) {
  return axiosDefault({
    method: "GET",
    url: `/api/note/teacher/studentNotes/${studentId}`,
  });
}

function addNote(data, studentId) {
  return axiosDefault({
    method: "POST",
    url: `/api/note/teacher/addNewNote/${studentId}`,
    data: data,
  });
}

export { getMyNotes, getStudentNotes, addNote };
