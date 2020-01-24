import React, { Component } from 'react';
import StudentGradesComponent from './StudentGradesComponent';
import { getMySubjects } from './../../../../Actions/subjects';
import { getMyGrades } from './../../../../Actions/grades';

class StudentGradesContainer extends Component {
  constructor() {
    super();
    this.state = {
      subjects: [{ name: "Matematyka" }, { name: "Polski" }, { name: "Biologia" }, { name: "Chemia" }],
      subject: {},
      grades: [],
    };
  }

  componentDidMount = () => {
    getMySubjects()
      .then(res => {
        this.setState({
          subjects: res.data.subjectList,
          subject: res.data.subjectList[0],
        }, () => {
          this.updateGrades();
        });
      })
  }


  updateGrades = () => {
    getMyGrades(this.state.subject.id)
      .then(res => {
        this.setState({
          grades: res.data.gradeDTOs,
        });
      });
  }

  handleSelectChange = (event) => {
    this.setState({
      subject: event.target.value,
    }, () => {
      this.updateGrades();
    });
  }

  render() {
    return (
      <div>
        <StudentGradesComponent
          subjects={this.state.subjects}
          handleSelectChange={this.handleSelectChange}
          subject={this.state.subject}
          grades={this.state.grades}
        />
      </div>
    );
  };
}
export default StudentGradesContainer;
