import axiosDefault from "./axiosDefault";

function getMyLessonPlan() {
  return axiosDefault({
    method: "GET",
    url: "/api/student/myLessonPlan",
  });
}

export { getMyLessonPlan };
