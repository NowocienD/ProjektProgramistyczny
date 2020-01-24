import axiosDefault from "./axiosDefault";

function getTeacherClasses() {
  return axiosDefault({
    method: "GET",
    url: "/api/class/teacher/myClasses"
  });
}

function getAllClasses() {
  return axiosDefault({
    method: "GET",
    url: "/api/class/allClasses"
  });
}

export { getTeacherClasses, getAllClasses };
