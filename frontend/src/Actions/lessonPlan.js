import axiosDefault from "./axiosDefault";

function getStudentLessonPlan() {
  return axiosDefault({
    method: "GET",
    url: "/api/lesson/student/myLessonPlan"
  });
}

function getTeacherLessonPlan() {
  return axiosDefault({
    method: "GET",
    url: "/api/lesson/teacher/myLessonPlan"
  });
}

function getClassLessons(classId, dayOfTheWeek) {
  return axiosDefault({
    method: "GET",
    url: `/api/lesson/admin/LessonPlan/${classId}/${dayOfTheWeek}`
  });
}

function deleteLesson(lessonId) {
  return axiosDefault({
    method: "DELETE",
    url: `/api/lesson/admin/deletelesson/${lessonId}`,
  });
}

function addLesson(data) {
  return axiosDefault({
    method: "POST",
    url: "/api/lesson/admin/addlesson",
    data: data
  });
}

function editLesson(data, lessonId) {
  return axiosDefault({
    method: "Patch",
    url: `/api/lesson/admin/updatelesson/${lessonId}`,
    data: data
  });
}

function getLesson(lessonId) {
  return axiosDefault({
    method: "GET",
    url: `/api/lesson/admin/getlesson/${lessonId}`,
  });
}

export {
  getStudentLessonPlan,
  getTeacherLessonPlan,
  getClassLessons,
  deleteLesson,
  addLesson,
  editLesson,
  getLesson,
};
