import React, { Component } from 'react';
import NotesComponent from './NotesComponent';
import { getMyNotes } from './../../../../Actions/notes';

class NotesContainer extends Component {
  constructor() {
    super();
    this.state = {
      notes: [],
    };
  }

  componentDidMount = () => {
    getMyNotes()
      .then(res => {
        this.setState({
          notes: res.data.noteDTOs,
        });
      })
  }

  render() {
    return (
      <div>
        <NotesComponent
          notes={this.state.notes}
        />
      </div>
    );
  };
}
export default NotesContainer;
