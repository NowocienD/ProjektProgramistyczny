import React, { Component } from 'react';
import StudentNotesComponent from './StudentNotesComponent';
import { getMyNotes } from './../../../../Actions/notes';

class StudentNotesContainer extends Component {
  constructor() {
    super();
    this.state = {
      notes: [],
    };
  }

  componentDidMount = () => {
    getMyNotes()
      .then(res => {
        console.log(res);
        this.setState({
          notes: res.data.noteDTOs,
        });
      })
  }

  render() {
    return (
      <div>
        <StudentNotesComponent
          notes={this.state.notes}
        />
      </div>
    );
  };
}
export default StudentNotesContainer;
