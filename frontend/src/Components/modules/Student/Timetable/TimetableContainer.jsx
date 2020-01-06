import React, { Component } from 'react';
import { Typography, Paper } from '@material-ui/core';
import TimetableComponent from './TimetableComponent';
import { getMyLessonPlan } from '../../../../Actions/lessonPlan';

class TimetableContainer extends Component {
  constructor() {
    super();
    this.state = {
      lessonPlan: [],
      choppedData: [],
    };
  }

  makeRows = (data) => {
    const days = ["Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek"];
    let rows = [];
    this.setState({
      data: rows,
    });
  }

  componentDidMount = () => {
    getMyLessonPlan()
      .then((res) => {
        console.log(res);
        this.setState({
          lessonPlan: res.data.lessonPlan,
        });
      });
  }

  render() {
    return (
      <div>
        <TimetableComponent
          timetable={this.state.lessonPlan}
        />
      </div>
    );
  };
}
export default TimetableContainer;
