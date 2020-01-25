import axiosDefault from "./axiosDefault";

function getLoggedUserData() {
  return axiosDefault({
    method: "GET",
    url: "/api/user/myProfile"
  });
}

function changeMyPassword(data) {
  return axiosDefault({
    method: "POST",
    url: "/api/user/updateMyPassword",
    data: data
  });
}

function getAllUsers() {
  return axiosDefault({
    method: "GET",
    url: "api/user/admin/allusers"
  });
}

function getUserData(userId) {
  return axiosDefault({
    method: "GET",
    url: `api/user/admin/userdata/${userId}`
  });
}

function addUser(data) {
  return axiosDefault({
    method: "POST",
    url: "api/user/admin/addUser",
    data: data
  });
}

function editUser(data, userId) {
  return axiosDefault({
    method: "PATCH",
    url: `api/user/admin/updateUser/${userId}`,
    data: data
  });
}

export {
  getLoggedUserData,
  changeMyPassword,
  getAllUsers,
  getUserData,
  addUser,
  editUser,
};
