import React, { Component } from 'react';
import GradesComponent from './GradesComponent';
import { getSubjects } from './../../../../Actions/subjects';
import { getGrades } from './../../../../Actions/grades';
import CircularProgress from '@material-ui/core/CircularProgress';

class GradesContainer extends Component {
  constructor() {
    super();
    this.state = {
      subjects: [],
      subject: {},
      grades: [],
    };
  }

  componentDidMount = () => {
    getSubjects()
      .then(res => {
        this.setState({
          subjects: res.data,
          subject: res.data[0],
        }, () => {
          this.updateGrades();
        });
      })
  }

  updateGrades = () => {
    getGrades(this.state.subject.id)
      .then(res => {
        this.setState({
          grades: res.data,
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
        <GradesComponent
          subjects={this.state.subjects}
          handleSelectChange={this.handleSelectChange}
          subject={this.state.subject}
          grades={this.state.grades}
        />
      </div>
    );
  };
}
export default GradesContainer;
