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
    url: `api/user/admin/user/${userId}`
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
  console.log(data);
  return axiosDefault({
    method: "PATCH",
    url: `api/user/admin/updateUser/${userId}`,
    data: data
  });
}

function deleteUser(userId) {
  return axiosDefault({
    method: "DELETE",
    url: `api/user/admin/deactivateuser/${userId}`,
  });
}

export {
  getLoggedUserData,
  changeMyPassword,
  getAllUsers,
  getUserData,
  addUser,
  editUser,
  deleteUser,
};
