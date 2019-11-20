import axios from "axios";

function login(data) {
  
  //   axios.post('http://localhost:8080/api/login', data)
  //   .then(response => {
  //     localStorage.setItem('token', response.data);
  //   });
  // random();
  var options = {
    method: 'POST',
    url: 'http://localhost:8080/api/login',
    data: data,
  }

  axios(options);

}

function random() {
  const token = localStorage.getItem('token');
  var options = {
    method: 'GET',
    url: 'http://localhost:8080/api/values',
    headers: {
      'Authorization': `Bearer ${token}`
    }
  }
  axios(options)
    .then(res => {
      console.log(res);
    })
}

export { login }
