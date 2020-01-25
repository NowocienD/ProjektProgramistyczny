import axiosDefault from "./axiosDefault";

function addClassSubject(classId, subjectId) {
  return axiosDefault({
    method: "POST",
    url: "/api/classsubject/admin/addclasssubject",
    params: {
      classId,
      subjectId
    }
  });
}

function deleteClassSubject(classId, subjectId) {
  return axiosDefault({
    method: "DELETE",
    url: `/api/classsubject/admin/deleteclasssubject`,
    params: {
      classId,
      subjectId
    }
  });
}

export { addClassSubject, deleteClassSubject };
