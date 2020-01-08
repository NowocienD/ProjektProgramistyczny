import axiosDefault from "./axiosDefault";

function getStudentsFromClass(classId) {
  return axiosDefault({
    method: "GET",
    url: `/api/student/studentsFromClass/${classId}`,
  });
}

export { getStudentsFromClass };
