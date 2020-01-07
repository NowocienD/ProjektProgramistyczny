import axios from "axios";


const axiosDefault = axios.create({
  baseURL: "http://localhost:8080"
});

export default axiosDefault;


