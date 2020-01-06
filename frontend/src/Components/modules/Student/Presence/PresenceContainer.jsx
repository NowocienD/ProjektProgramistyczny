import React, { Component } from 'react';
import PresenceComponent from './PresenceComponent';
import { getWeeks } from './../../../../Actions/weekDictionary';
import { getMyPresence } from './../../../../Actions/presence';


class PresenceContainer extends Component {
  constructor() {
    super();
    this.state = {
      weeks: getWeeks(),
      week: {},
      presence: [],
    };
  }

  updatePresence = () => {
    const values = {
      FirstDate: this.state.week.begin,
    };
    getMyPresence(values)
      .then((res) => {
        this.setState({
          presence: res.data.AttendancesPlan,
        });
      });
  }

  handleSelectChange = (event) => {
    console.log(event.target.value);
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
        <PresenceComponent
          weeks={this.state.weeks}
          handleSelectChange={this.handleSelectChange}
          week={this.state.week}
          presence={this.state.presence}
        />
      </div>
    );
  };
}
export default PresenceContainer;
