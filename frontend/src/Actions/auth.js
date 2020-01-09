import axiosDefault from "./axiosDefault";

function login(data) {
    return axiosDefault({
      method: "POST",
      url: "/api/user/login",
      data: data,
    }).then(res => {
      const token = res.data;
      localStorage.setItem('Token', token);
      setAuthorizationToken(token);
    })
}

function setAuthorizationToken(token) {
  if (token) {
    axiosDefault.defaults.headers.common["Authorization"] = `Bearer ${token}`;
  } else {
    delete axiosDefault.defaults.headers.common["Authorization"];
  }
}

function logout() {
  localStorage.removeItem('Token');
}
export { login, setAuthorizationToken, logout };
