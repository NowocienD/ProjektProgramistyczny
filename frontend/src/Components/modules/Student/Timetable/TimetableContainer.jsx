import React, { Component } from 'react';
import { Typography, Paper } from '@material-ui/core';
import TimetableComponent from './TimetableComponent';

const timetable = [
  {
    day: "Poniedziałek",
    name: "Matematyka",
    start: "9:00",
    end: "9:45",
  },
  {
    day: "Poniedziałek",
    name: "Angielski",
    start: "10:00",
    end: "10:45",
  },
  {
    day: "Poniedziałek",
    name: "Angielski",
    start: "11:00",
    end: "11:45",
  },
  {
    day: "Poniedziałek",
    name: "Angielski",
    start: "12:00",
    end: "12:45",
  },
  {
    day: "Wtorek",
    name: "Polski",
    start: "10:00",
    end: "10:45",
  },
  {
    day: "Wtorek",
    name: "Historia",
    start: "11:00",
    end: "11:45",
  }
]

class TimetableContainer extends Component {
  constructor() {
    super();
    this.state = {
      data: [],
    };
  }

  makeRows = (data) => {
    const days = ["Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota"];
    let rows = new Array();

    days.forEach(day => {
      const row = data.filter(element => element.day === day);
      rows.push(row);
    });
    this.setState({
      data: rows,
    });
  }

  componentDidMount = () => {
    this.makeRows(timetable);
  }
  render() {
    return (
      <div>
        <TimetableComponent
          timetable={this.state.data}
        />
      </div>
    );
  };
}
export default TimetableContainer;
