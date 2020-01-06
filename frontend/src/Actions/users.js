import axiosDefault from "./axiosDefault";

function getLoggedUserData() {
  return axiosDefault({
    method: "GET",
    url: "/api/user/myProfile"
  });
}

export { getLoggedUserData };
