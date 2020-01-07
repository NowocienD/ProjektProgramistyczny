import React, { Component } from 'react';
import './App.css';
import Layout from './Components/navigation/Layout';
import 'typeface-roboto'
import { logout } from './Actions/auth';
import { withRouter } from 'react-router-dom';
import StudentGradesContainer from './Components/modules/Student/Grades/StudentGradesContainer';
import StudentPresenceContainer from './Components/modules/Student/Presence/StudentPresenceContainer';
import StudentTimetableContainer from './Components/modules/Student/Timetable/StudentTimetableContainer';
import StudentNotesContainer from './Components/modules/Student/Notes/StudentNotesContainer';

import TeacherTimetableContainer from './Components/modules/Teacher/Timetable/TeacherTimetableContainer';

class App extends Component {

  constructor(props) {
    super();
    this.createMenu(props.user.role);
    this.createComponents(props.user.role);
  }

  logout = () => {
    logout();
    this.redirectToLoginPage();
  }

  redirectToLoginPage() {
    this.props.history.push('/login');
  }

  createMenu = (role) => {
    if (role) {
      if (role === 'Student' || role === 'Teacher') {
        this.menu = [
          {
            key: 'timetable',
            name: "Plan zajęć",
            icon: 'calendar_today',
            to: '/timetable'
          },
          {
            key: 'grades',
            name: "Oceny",
            icon: 'check',
            to: '/grades'
          },
          {
            key: 'presence',
            name: "Obecności",
            icon: 'bookmark',
            to: '/presence',
          },
          {
            key: 'notes',
            name: "Uwagi",
            icon: 'warning',
            to: '/notes',
          },
        ]
      }
    }
  }

  createComponents = (role) => {
    this.components = {};
    if (role === 'Student') {
      this.components = {
        GradesContainer: StudentGradesContainer,
        NotesContainer: StudentNotesContainer,
        PresenceContainer: StudentPresenceContainer,
        TimetableContainer: StudentTimetableContainer,
      } 
    } else if (role === 'Teacher') {
      this.components = {
        //GradesContainer: TeacherGradesContainer,
        //NotesContainer: TeacherNotesContainer,
        //PresenceContainer: TeacherPresenceContainer,
        TimetableContainer: TeacherTimetableContainer,
      }  
    }
  }

  render() {
    return (
      <div className="Container">
        <Layout
          menu={this.menu}
          components={this.components}
          logout={this.logout}
          user={this.props.user}
        />
      </div>
    );
  }

}

export default withRouter(App);
