import axiosDefault from "./axiosDefault";

function getAllRoles() {
  return axiosDefault({
    method: "GET",
    url: "/api/role/admin/allroles",
  });
}

export { getAllRoles };
