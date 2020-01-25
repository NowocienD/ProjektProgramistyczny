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

function addClass(data) {
  return axiosDefault({
    method: "POST",
    url: "/api/class/admin/addClass",
    data: data
  });
}

function editClass(data, classId) {
  return axiosDefault({
    method: "PUT",
    url: `/api/class/admin/updateClass/${classId}`,
    data: data
  });
}

function deleteClass(classId) {
  return axiosDefault({
    method: "DELETE",
    url: `/api/class/admin/deleteClass/${classId}`
  });
}

export { getTeacherClasses, getAllClasses, addClass, editClass, deleteClass };
