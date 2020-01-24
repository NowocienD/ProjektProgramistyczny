import React, { Component } from 'react';
import TeacherTimetableComponent from './TeacherTimetableComponent';
import { getTeacherLessonPlan } from '../../../../Actions/lessonPlan';

class TeacherTimetableContainer extends Component {
  constructor() {
    super();
    this.state = {
      lessonPlan: [],
    };
  }

  componentDidMount = () => {
    getTeacherLessonPlan()
      .then((res) => {
        this.setState({
          lessonPlan: res.data.lessonPlan,
        });
      });
  }

  render() {
    return (
      <div>
        <TeacherTimetableComponent
          timetable={this.state.lessonPlan}
        />
      </div>
    );
  };
}
export default TeacherTimetableContainer;
