import React, { Component } from 'react';
import { Typography, Paper } from '@material-ui/core';
import TimetableComponent from './TimetableComponent';

const timetable = [
  [
    {
      name: "Poniedziałek",
      day: true,
    },
    {
      name: "Matematyka",
    },
    {
      name: "Angielski",
    },
    {
      name: "Biologia",
    },
    {
      name: "Chemia",
    },
    {
      name: "Polski",
    },
    {
      name: "Historia",
    },
    {
      name: "Chemia",
    }
  ],
  [
    {
      name: "Wtorek",
      day: true,
    },
    {
      name: "Matematyka",
    },
    {
      name: "Angielski",
    },
    {
      name: "Biologia",
    },
    {
      name: "Chemia",
    },
    {
      name: "Polski",
    },
    {
      name: "Historia",
    },
    {
      name: "Chemia",
    }
  ],
  [
    {
      name: "Środa",
      day: true,
    },
    {
      name: "Matematyka",
    },
    {
      name: "Angielski",
    },
    {
      name: "Biologia",
    },
    {
      name: "Chemia",
    },
    {
      name: "Polski",
    },
    {
      name: "Historia",
    },
    {
      name: "Chemia",
    }
  ],
  [
    {
      name: "Czwartek",
      day: true,
    },
    {
      name: "Matematyka",
    },
    {
      name: "Angielski",
    },
    {
      name: "Biologia",
    },
    {
      name: "Chemia",
    },
    {
      name: "Polski",
    },
    {
      name: "Historia",
    },
    {
      name: "Chemia",
    }
  ],
  [
    {
      name: "Piątek",
      day: true,
    },
    {
      name: "Matematyka",
    },
    {
      name: "Angielski",
    },
    {
      name: "Biologia",
    },
    {
      name: "Chemia",
    },
    {
      name: "Polski",
    },
    {
      name: "Historia",
    },
    {
      name: "Chemia",
    }
  ],
]

class TimetableContainer extends Component {
  constructor() {
    super();
    this.state = {
      data: [],
    };
  }

  makeRows = (data) => {
    const days = ["Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek"];
    let rows = [];

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
          // timetable={this.state.data}
          timetable={timetable}
        />
      </div>
    );
  };
}
export default TimetableContainer;
