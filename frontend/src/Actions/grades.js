import axios from 'axios';

function getGrades(subject) {
  return axios.get(`http://localhost:3000/subjects/${subject}/grades`);
}

export { getGrades };