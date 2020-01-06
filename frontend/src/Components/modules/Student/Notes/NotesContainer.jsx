import React, { Component } from 'react';
import NotesComponent from './NotesComponent';

class NotesContainer extends Component {
  constructor() {
    super();
    this.state = {
      notes: [
         { content: "Zle zachowanie1", via: "Nowak", date: '01-01-2019' },
         { content: "Zle zachowanie2", via: "Kowalski", date: '02-01-2019' },
         { content: "Zle zachowanie3", via: "Nowocień", date: '03-01-2019' },
         { content: "Zle zachowanie4", via: "Nalepa", date: '04-01-2019' },
        ],
    };

  }

  // componentDidMount = () => {
  //   getSubjects()
  //     .then(res => {
  //       this.setState({
  //         subjects: res.data,
  //         subject: res.data[0],
  //       }, () => {
  //         this.updateNotes();
  //       });
  //     })
  // }


  // updateNotes = () => {
  //   getNotes(this.state.subject.id)
  //     .then(res => {
  //       this.setState({
  //         Notes: res.data,
  //       });
  //     });
  // }
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
