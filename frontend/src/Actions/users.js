import axiosDefault from "./axiosDefault";

function getLoggedUserData() {
  return axiosDefault({
    method: "GET",
    url: "/api/user/myProfile"
  });
}

function changeMyPassword(data) {
  return axiosDefault({
    method: 'POST',
    url: "/api/user/updateMyPassword",
    data: data,
  })
}

export { getLoggedUserData, changeMyPassword };