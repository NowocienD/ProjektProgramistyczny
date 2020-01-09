import React, { Component } from 'react';
import StudentTimetableComponent from './StudentTimetableComponent';
import { getStudentLessonPlan } from '../../../../Actions/lessonPlan';

class StudentTimetableContainer extends Component {
  constructor() {
    super();
    this.state = {
      lessonPlan: [],
    };
  }

  componentDidMount = () => {
    getStudentLessonPlan()
      .then((res) => {
        this.setState({
          lessonPlan: res.data.lessonPlan,
        });
      });
  }

  render() {
    return (
      <div>
        <StudentTimetableComponent
          timetable={this.state.lessonPlan}
        />
      </div>
    );
  };
}
export default StudentTimetableContainer;
