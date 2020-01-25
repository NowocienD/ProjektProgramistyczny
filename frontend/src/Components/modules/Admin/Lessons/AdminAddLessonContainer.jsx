import React from 'react';
import AdminAddLessonComponent from './AdminAddLessonComponent';
import { getClassSubjects, getSubjectTeachers } from '../../../../Actions/subjects';
import { addLesson, editLesson } from '../../../../Actions/lessonPlan';
import { withSnackbar } from '../../../navigation/SnackbarContext';

class AdminAddLessonContainer extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      addMode: props.match.params.lessonNumber === 'add',
      lessonNumber: props.match.params.lessonNumber,
      classId: props.match.params.classId,
      subjects: [],
      subject: '',
      teachers: [],
      teacher: '',
    };
  }

  componentDidMount = () => {
    this.setDefaultValues();
    this.fetchData();
  }

  setDefaultValues = () => {
    if (!this.state.addMode)
  }

  fetchData = () => {
    getClassSubjects(this.state.classId)
      .then(res => {
        this.setState({
          subjects: res.data.subjectList,
          subject: res.data.subjectList ? res.data.subjectList[0] : '',
        }, () => {
          this.getTeachers();
        })
      })
  }

  getTeachers = () => {
    getSubjectTeachers(this.state.subject.id)
      .then(res => {
        this.setState({
          teachers: res.data.teacherSubjects,
          teacher: res.data.teacherSubjects ? res.data.teacherSubjects[0] : '',
        });
      })
  }

  handleSubjectChange = (event) => {
    this.setState({
      subject: event.target.value,
    }, () => {
      this.getTeachers();
    });
  }

  handleTeacherChange = (event) => {
    this.setState({
      teacher: event.target.value,
    });
  }

  handleLessonNumberChange = (event) => {
    this.setState({
      lessonNumber: event.target.value,
    });
  }

  onSave = () => {
    if (this.state.addMode) {

    } else {

    }
  }

  render() {
    return (
      <AdminAddLessonComponent
        subjects={this.state.subjects}
        subject={this.state.subject}
        teachers={this.state.teachers}
        teacher={this.state.teacher}
        handleSubjectChange={this.handleSubjectChange}
        handleTeacherChange={this.handleSubjectChange}
        handleLessonNumberChange={this.handleLessonNumberChange}
        lessonNumber={this.state.lessonNumber}
        addMode={this.state.addMode}
      />
    );
  }
}

export default withSnackbar(AdminAddLessonContainer);