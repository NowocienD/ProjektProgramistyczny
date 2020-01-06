import axiosDefault from "./axiosDefault";

function getMyGrades(subjectId) {
  return axiosDefault({
    method: "GET",
    url: `/api/student/myGrades/${subjectId}`,
  });
}
export { getMyGrades };