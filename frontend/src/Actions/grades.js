import axiosDefault from "./axiosDefault";

function getMyGrades(subjectId) {
  return axiosDefault({
    method: "GET",
    url: `/api/grade/student/myGrades/${subjectId}`,
  });
}
export { getMyGrades };