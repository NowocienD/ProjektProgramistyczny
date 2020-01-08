import axiosDefault from "./axiosDefault";

function getMySubjects() {
    return axiosDefault({
      method: "GET",
      url: "/api/subject/student/mySubjects",
    });
  }

  function getClassSubjects(classId) {
    return axiosDefault({
      method: "GET",
      url: `/api/subject/subjectsFromClass/${classId}`,
    })
  }

export { getMySubjects, getClassSubjects };