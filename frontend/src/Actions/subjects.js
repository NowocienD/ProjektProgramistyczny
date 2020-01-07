import axiosDefault from "./axiosDefault";

function getMySubjects() {
    return axiosDefault({
      method: "GET",
      url: "/api/student/mySubjects",
    });
  }

export { getMySubjects };