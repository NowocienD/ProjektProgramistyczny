import axiosDefault from "./axiosDefault";

function getMyGrades(subjectId) {
  return axiosDefault({
    method: "GET",
    url: `/api/grade/student/myGrades/${subjectId}`
  });
}

function getStudentGrades(studentId, subjectId) {
  return axiosDefault({
    method: "GET",
    url: `/api/grade/teacher/grades/${subjectId}/${studentId}`
  });
}

function addGrade(data, studentId) {
  return axiosDefault({
    method: "POST",
    url: `/api/grade/teacher/addGrade/${studentId}`,
    data: data
  });
}

function updateGrade(data, studentId) {
  return axiosDefault({
    method: "PUT",
    url: `/api/grade/teacher/updateGrade/${studentId}`,
    data: data
  });
}

function deleteGrade(gradeId) {
  return axiosDefault({
    method: "DELETE",
    url: `/api/grade/teacher/deleteGrade/${gradeId}`
  });
}
export { getMyGrades, getStudentGrades, addGrade, updateGrade, deleteGrade };
