import axiosDefault from "./axiosDefault";

function getMyGrades(subjectId) {
  return axiosDefault({
    method: "GET",
    url: `/api/grade/student/myGrades/${subjectId}`,
  });
}

function getStudentGrades(studentId, subjectId) {
  return axiosDefault({
    method: "GET",
    url: `/api/grade/teacher/grades/${subjectId}/${studentId}`,
  });
}

function addGrade(data, studentId) {
  return axiosDefault({
    method: "POST",
    url: `/api/grade/teacher/addGrade/${studentId}`,
    data: data,
  })
}
export { getMyGrades, getStudentGrades, addGrade };