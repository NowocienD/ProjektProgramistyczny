import axiosDefault from "./axiosDefault";

function getAllTeachers() {
  return axiosDefault({
    method: "GET",
    url: "/api/teacher/admin/allteachers",
  });
}

export { getAllTeachers }