import React from 'react';
import TeacherGradesComponent from './TeacherGradesComponent';
import { getTeacherClasses } from '../../../../Actions/class';
import { getStudentsFromClass } from '../../../../Actions/student';
import { getClassSubjects } from '../../../../Actions/subjects';
import { getStudentGrades, addGrade, updateGrade } from '../../../../Actions/grades';

class TeacherGradesContainer extends React.Component {
  constructor() {
    super();
    this.state = {
      classes: [],
      class: '',
      students: [],
      student: '',
      subjects: [],
      subject: '',
      grades: [],
      grade: undefined,
      dialogVisible: false,
      value: 5,
      importance: 5,
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
      grade: undefined,
    })
  }

  showDialog = (event, rowData) => {
    this.setState({
      dialogVisible: true,
      grade: rowData,
    })
  }

  onAddGrade = (data) => {
    const dt = {
      value: this.state.value,
      importance: this.state.importance,
      subjectId: this.state.subject.id,
      ...data,
    };
    return addGrade(dt, this.state.student.id);
  }

  handleValue = (event) => {
    this.setState({
      value: event.target.value,
    })
  }

  handleImportance = (event) => {
    this.setState({
      importance: event.target.value,
    })
  }
  onUpdateGrade = (data) => {
    let newGrade = this.state.value;
    let newImportance = this.state.importance
    if (data.correction) {
      newGrade = Math.round((this.state.grade.value + this.state.value) / 2);
      newImportance = this.state.grade.importance;
    } 
    const dt = {
      id: this.state.grade.id,
      subjectId: this.state.subject.id,
      value: newGrade,
      importance: newImportance,
      ...data,
    };
    console.log(dt);
    return updateGrade(dt, this.state.student.id);
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
        grade={this.state.grade}
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
          },
          {
            icon: 'edit',
            toolTip: 'Edytuj ocenę',
            onClick: (event, rowData) => this.showDialog(event, rowData),
          }
        ]}
        dialogVisible={this.state.dialogVisible}
        showDialog={this.showDialog}
        hideDialog={this.hideDialog}
        getGrades={this.getGrades}
        onAddGrade={this.onAddGrade}
        onUpdateGrade={this.onUpdateGrade}
        handleValue={this.handleValue}
        handleImportance={this.handleImportance}
        value={this.state.value}
        importance={this.state.importance}
      />
    );
  }
}

export default TeacherGradesContainer;