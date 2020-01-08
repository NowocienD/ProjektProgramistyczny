import React from 'react';
import TeacherGradesComponent from './TeacherGradesComponent';
import { getTeacherClasses } from '../../../../Actions/class';
import { getStudentsFromClass } from '../../../../Actions/student';
import { getClassSubjects } from '../../../../Actions/subjects';
import { getStudentGrades, addGrade } from '../../../../Actions/grades';

class TeacherGradesContainer extends React.Component {
  constructor() {
    super();
    this.state = {
      classes: [],
      class: {},
      students: [],
      student: {},
      subjects: [],
      subject: {},
      grades: [],
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
          this.updateAll();
        });
      })
  }

  updateAll = () => {
    getStudentsFromClass(this.state.class.id)
      .then(res => {
        this.setState({
          students: res.data.studentList,
          student: res.data.studentList ? res.data.studentList[0] : {},
        }, () => {
          getClassSubjects(this.state.class.id)
            .then(res => {
              this.setState({
                subjects: res.data.subjectList,
                subject: res.data.subjectList ? res.data.subjectList[0] : {},
              }, () => {
                this.getGrades();
              });
            });
        });
      })
  }

  handleClassChange = (event) => {
    this.setState({
      class: event.target.value,
    }, () => {
      this.getStudents();
      this.getSubjects();
    })
  }

  handleStudentChange = (event) => {
    this.setState({
      student: event.target.value,
    }, () => {
      this.getGrades();
    })
  }

  getSubjects = () => {
    getClassSubjects(this.state.class.id)

  }

  getGrades = () => {
    getStudentGrades(this.state.student.id, this.state.subject.id)
      .then(res => {
        this.setState({
          grades: res.data.gradeDTOs,
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

  onAddGrade = (data) => {
    const dt = {
      subjectId: this.state.subject.id,
      ...data,
    };
    return addGrade(dt, this.state.student.id);
  }

  render() {
    return (
      <TeacherGradesComponent
        classes={this.state.classes}
        class={this.state.class}
        student={this.state.student}
        students={this.state.students}
        subject={this.state.subject}
        subjects={this.state.subjects}
        handleClassChange={this.state.handleClassChange}
        handleStudentChange={this.state.handleStudentChange}
        grades={this.state.grades}
        columns={[
          {
            title: 'Ocena',
            field: 'value',
          },
          {
            title: 'Waga',
            field: 'importance',
          },
          {
            title: 'Temat',
            field: 'topic',
          },
          {
            title: 'Wpisana przez',
            field: 'teacherFullname',
          },
        ]}
        actions={[
          {
            icon: 'add',
            toolTip: 'Dodaj ocenę',
            isFreeAction: true,
            onClick: this.showDialog,
          }
        ]}
        dialogVisible={this.state.dialogVisible}
        showDialog={this.showDialog}
        hideDialog={this.hideDialog}
        getGrades={this.getGrades}
        onAddGrade={this.onAddGrade}
      />
    );
  }
}

export default TeacherGradesContainer;