import React, { Component } from 'react';
import StudentPresenceComponent from './StudentPresenceComponent';
import { getWeeks } from './../../../../Actions/weekDictionary';
import { getMyPresence } from './../../../../Actions/presence';


class StudentPresenceContainer extends Component {
  constructor() {
    super();
    this.state = {
      weeks: getWeeks(),
      week: {},
      presence: [],
    };
  }

  updatePresence = () => {
    getMyPresence(this.state.week.begin)
      .then((res) => {
        this.setState({
          presence: res.data.attendancesPlan,
        });
      });
  }

  handleSelectChange = (event) => {
    this.setState({
      week: event.target.value,
    }, () => {
      this.updatePresence();
    }
    );
  }

  render() {
    return (
      <div>
        <StudentPresenceComponent
          weeks={this.state.weeks}
          handleSelectChange={this.handleSelectChange}
          week={this.state.week}
          presence={this.state.presence}
        />
      </div>
    );
  };
}
export default StudentPresenceContainer;
