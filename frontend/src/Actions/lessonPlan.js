import axiosDefault from "./axiosDefault";

function getStudentLessonPlan() {
  return axiosDefault({
    method: "GET",
    url: "/api/lesson/student/myLessonPlan",
  });
}

function getTeacherLessonPlan() {
  return axiosDefault({
    method: "GET",
    url: "/api/lesson/teacher/myLessonPlan",
  });
}

export { getStudentLessonPlan, getTeacherLessonPlan  };
