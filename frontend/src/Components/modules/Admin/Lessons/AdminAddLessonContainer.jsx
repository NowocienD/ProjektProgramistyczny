import React from 'react';
import AdminAddLessonComponent from './AdminAddLessonComponent';
import { getClassSubjects, getSubjectTeachers } from '../../../../Actions/subjects';
import { addLesson, editLesson, getLesson } from '../../../../Actions/lessonPlan';
import { withSnackbar } from '../../../navigation/SnackbarContext';

class AdminAddLessonContainer extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      addMode: props.match.params.lessonId === 'add',
      lessonId: props.match.params.lessonId,
      classId: props.match.params.classId,
      subjects: [],
      subject: '',
      teachers: [],
      teacher: '',
      lessonNumber: '',
      day: props.match.params.day
    };
  }

  componentDidMount = () => {
    this.fetchData();
  }

  goBack = () => {
    this.props.history.push('/lessons')
  }

  setDefaultValues = () => {
    if (!this.state.addMode) {
      getLesson(this.state.lessonId)
        .then(res => {
          const sub = this.state.subjects.filter(el => el.name === res.data.name)
          const te = this.state.teachers.filter(el => el.firstnameSurname === res.data.teacherName);
          this.setState({
            subject: sub[0],
            lessonNumber: res.data.lessonNumber,
            teacher: te[0],
          });
        })
    }
  }

  fetchData = () => {
    getClassSubjects(this.state.classId)
      .then(res => {
        this.setState({
          subjects: res.data.subjectList ? res.data.subjectList : '',
          subject: res.data.subjectList ? res.data.subjectList[0] : '',
        }, () => {
          this.getTeachers()
        })
      })
  }

  getTeachers = () => {
    if (this.state.subject) {
      getSubjectTeachers(this.state.subject.id)
        .then(res => {
          this.setState({
            teachers: res.data.teacherSubjects,
            teacher: res.data.teacherSubjects ? res.data.teacherSubjects[0] : '',
          }, () => {
            this.setDefaultValues();
          });
        })
    }
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
    if (this.state.subject && this.state.teacher) {
      const dto = {
        LessonNumber: this.state.lessonNumber-1,
        DayOfTheWeek: parseInt(this.state.day),
        SubjectId: this.state.subject.id,
        ClassId: parseInt(this.state.classId),
        TeacherId: this.state.teacher.id,
      };
      if (this.state.addMode) {
        addLesson(dto)
          .then(res => {
            this.goBack();
            this.props.showMessage(res.data);
          })
          .catch(err => {
            this.goBack();
            this.props.showMessage(err.response.data);
          });

      } else {
        editLesson(dto, this.state.lessonId)
          .then(res => {
            this.goBack();
            this.props.showMessage(res.data);
          })
          .catch(res => {
            this.goBack();
            this.props.showMessage(res.response.data);
          });
      }
    } else {
      this.props.showMessage("Nie wybrano przedmiotu lub nauczyciela");
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
        handleTeacherChange={this.handleTeacherChange}
        handleLessonNumberChange={this.handleLessonNumberChange}
        lessonNumber={this.state.lessonNumber}
        addMode={this.state.addMode}
        goBack={this.goBack}
        onSave={this.onSave}
      />
    );
  }
}

export default withSnackbar(AdminAddLessonContainer);