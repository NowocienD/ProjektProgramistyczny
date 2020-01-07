import axiosDefault from "./axiosDefault";

function getMySubjects() {
    return axiosDefault({
      method: "GET",
      url: "/api/subject/student/mySubjects",
    });
  }

export { getMySubjects };