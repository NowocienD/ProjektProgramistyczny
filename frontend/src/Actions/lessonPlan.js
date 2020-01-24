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

function deleteLesson(data) {
  return axiosDefault({
    method: "DELETE",
    url: "/api/lesson/admin/deletelesson",
    data: data
  });
}

export {
  getStudentLessonPlan,
  getTeacherLessonPlan,
  getClassLessons,
  deleteLesson
};
