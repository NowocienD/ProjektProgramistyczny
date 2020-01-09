import axiosDefault from "./axiosDefault";

function getTeacherClasses() {
  return axiosDefault({
    method: "GET",
    url: "/api/class/teacher/myClasses",
  });
}

export { getTeacherClasses }