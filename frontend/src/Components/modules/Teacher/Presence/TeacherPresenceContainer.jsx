import React from 'react';
import TeacherPresenceComponent from './TeacherPresenceComponent';
import { getTeacherClasses } from '../../../../Actions/class';

class TeacherPresenceContainer extends React.Component {
  constructor() {
    super();
    this.state = {
      classes: [],
      lessons: [1, 2, 3, 4, 5, 6, 7, 8],
      date: new Date().toISOString().slice(0,10),
      class: '',
      lesson: 1,
      data: [],
    }
  }

  componentDidMount() {
    getTeacherClasses()
      .then(res => {
        this.setState({
          classes: res.data.classList,
          class: res.data.classList ? res.data.classList[0] : '',
        }, () => {
          this.getData();
        });
      })
  }

  getData = () => {
    
  }

  handleDateChange = (event) => {
    this.setState({
      date: event.target.value,
    });
  }

  handleClassChange = (event) => {
    this.setState({
      class: event.target.value,
    });
  }

  handleLessonChange = (event) => {
    this.setState({
      lesson: event.target.value,
    });
  }

  render() {
    return (
      <TeacherPresenceComponent
        classes={this.state.classes}
        lessons={this.state.lessons}
        date={this.state.date}
        class={this.state.class}
        lesson={this.state.lesson}
        handleDateChange={this.handleDateChange}
        handleClassChange={this.handleClassChange}
        handleLessonChange={this.handleLessonChange}
      />
    )
  }
}

export default TeacherPresenceContainer;

