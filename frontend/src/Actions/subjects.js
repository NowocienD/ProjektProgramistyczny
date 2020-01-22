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

  function getAllSubjects() {
    return axiosDefault({
      method: "GET",
      url: "/api/subject/allSubjects/",
    })
  }

  function addSubject(data) {
    return axiosDefault({
      method: "POST",
      url: "/api/subject/admin/addNewSubject",
      data: data,
    })
  }

  function deleteSubject(id) {
    console.log(id);
    return axiosDefault({
      method: "DELETE",
      url: `/api/subject/admin/deleteSubject/${id}`,
    });
  }

export { getMySubjects, getClassSubjects, getAllSubjects, addSubject, deleteSubject };