import React from 'react';
import TeacherNotesComponent from './TeacherNotesComponent';
import { getTeacherClasses } from '../../../../Actions/class';
import { getStudentsFromClass } from '../../../../Actions/student';
import { getStudentNotes, addNote } from '../../../../Actions/notes';

class TeacherNotesContainer extends React.Component {
  constructor() {
    super();
    this.state= {
      classes: [],
      class: {},
      students: [],
      student: {},
      notes: [],
      dialogVisible: false,
    };
  }

  componentDidMount = () => {
    getTeacherClasses()
      .then(res => {
        this.setState({
          classes: res.data.classList,
          class: res.data.classList ? res.data.classList[0] : {},
        }, () => {
          this.getStudents();
        });
      })
  }

  getStudents = () => {
    getStudentsFromClass(this.state.class.id)
      .then(res => {
        this.setState({
          students: res.data.studentList,
          student: res.data.studentList ? res.data.studentList[0] : {},
        }, () => {
          this.getNotes();
        });
      })
  }

  handleClassChange = (event) => {
    this.setState({
      class: event.target.value,
    }, () => {
      this.getStudents();
    })
  }

  handleStudentChange = (event) => {
    this.setState({
      student: event.target.value,
    }, () => {
     this.getNotes();
    })
  }

  getNotes = () => {
    getStudentNotes(this.state.student.id)
    .then(res => {
      console.log(res);
      this.setState({
        notes: res.data.noteDTOs,
      });
    });
  }

  hideDialog = () => {
    this.setState({
      dialogVisible: false,
    })
  }

  showDialog = () => {
    this.setState({
      dialogVisible: true,
    })
  }

  onAddNote = (data) => addNote(data, this.state.student.id);

  render() {
    return (
      <TeacherNotesComponent
        classes={this.state.classes}
        class={this.state.class}
        student={this.state.student}
        students={this.state.students}
        handleClassChange={this.state.handleClassChange}
        handleStudentChange={this.state.handleStudentChange}
        notes={this.state.notes}
        columns={[
          {
            title: 'Treść',
            field: 'statement'
          },
          {
            title: 'Data',
            field: 'date'
          },
          {
            title: 'Wpisana przez',
            field: 'firstNameAndSurname'
          },
        ]}
        actions={[
          {
            icon: 'add',
            toolTip: 'Dodaj uwagę',
            isFreeAction: true,
            onClick: this.showDialog,
          }
        ]}
        dialogVisible={this.state.dialogVisible}
        showDialog={this.showDialog}
        hideDialog={this.hideDialog}
        getNotes={this.getNotes}
        onAddNote={this.onAddNote}
      />
    );
  }
}

export default TeacherNotesContainer;