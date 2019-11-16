import axios from 'axios';

function getSubjects() {
    return axios.get('http://localhost:3000/subjects');
}

export { getSubjects };