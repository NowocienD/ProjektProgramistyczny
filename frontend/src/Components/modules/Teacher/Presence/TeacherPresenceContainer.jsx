import React from 'react';
import TeacherPresenceComponent from './TeacherPresenceComponent';
import { getTeacherClasses } from '../../../../Actions/class';
import { getClassPresence, addPresence } from '../../../../Actions/presence';
import { getPresenceStatuses } from '../../../../Actions/presenceStatus';

class TeacherPresenceContainer extends React.Component {
  constructor() {
    super();
    this.state = {
      classes: [],
      lessons: [1, 2, 3, 4, 5, 6, 7, 8],
      date: new Date().toISOString().slice(0, 10),
      class: '',
      lesson: 1,
      data: [],
      statuses: {},
    }
  }

  componentDidMount() {
    getPresenceStatuses()
      .then(res => {
        let lookup = { 0: 'Nie wpisano' };
        const st = res.data.attendanceStatusDTOs;
        let i;
        for (i = 0; i < st.length; i++) {
          lookup[st[i].id] = st[i].name;
        }
        console.log(lookup)
        this.setState({
          statuses: lookup,
        }, () => {
          getTeacherClasses()
            .then(res => {
              this.setState({
                classes: res.data.classList,
                class: res.data.classList ? res.data.classList[0] : '',
              }, () => {
                this.getData();
              });
            })
        })
      });
  }

  getData = () => {
    getClassPresence(this.state.date, this.state.class.id, this.state.lesson)
      .then(res => {
        this.setState({
          data: res.data.singleLessonAttendances,
        });
      })
      .catch(err => {
        this.setState({
          data: [],
        });
      });
  }

  handleDateChange = (event) => {
    this.setState({
      date: event.target.value,
    }, () => {
      this.getData();
    });
  }

  handleClassChange = (event) => {
    this.setState({
      class: event.target.value,
    }, () => {
      this.getData();
    });
  }

  handleLessonChange = (event) => {
    this.setState({
      lesson: event.target.value,
    }, () => {
      this.getData();
    });
  }

  onAddPresence = (newData, oldData) => {
    const data = {
      date: this.state.date,
      lessonNumber: parseInt(this.state.lesson),
      attendanceStatusId: parseInt(newData.attendanceStatusId),
    };
    return addPresence(data, newData.studentId)
      .then(() => {
        this.getData();
      })
  }

  render() {
    return (
      <TeacherPresenceComponent
        classes={this.state.classes}
        lessons={this.state.lessons}
        date={this.state.date}
        data={this.state.data}
        class={this.state.class}
        lesson={this.state.lesson}
        handleDateChange={this.handleDateChange}
        handleClassChange={this.handleClassChange}
        handleLessonChange={this.handleLessonChange}
        columns={[
          {
            title: "Uczeń",
            field: "name",
            editable: "never",
          },
          {
            title: "Obecność",
            field: "attendanceStatusId",
            lookup: this.state.statuses,
          }
        ]}
        editable={{
          onRowUpdate: (newData, oldData) => this.onAddPresence(newData, oldData)
        }}
      />
    )
  }
}

export default TeacherPresenceContainer;

