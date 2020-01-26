import React from 'react';
import TeacherGradesComponent from './TeacherGradesComponent';
import { getTeacherClasses } from '../../../../Actions/class';
import { getStudentsFromClass } from '../../../../Actions/student';
import { getClassSubjects } from '../../../../Actions/subjects';
import { getStudentGrades, addGrade, updateGrade, deleteGrade } from '../../../../Actions/grades';

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
      deleteDialogVisible: false,
      checked: false,
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
      this.updateAll();
    })
  }

  handleStudentChange = (event) => {
    this.setState({
      student: event.target.value,
    }, () => {
      this.getGrades();
    })
  }

  handleSubjectChange = (event) => {
    this.setState({
      subject: event.target.value,
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
      value: rowData.value,
      importance: rowData.importance,
    })
  }

  hideDeleteDialog = () => {
    this.setState({
      deleteDialogVisible: false,
      grade: undefined,
    })
  }

  showDeleteDialog = (event, rowData) => {
    this.setState({
      deleteDialogVisible: true,
      grade: rowData,
    })
  }

  onDelete = () => {
    return deleteGrade(this.state.grade.id)
      .then(this.hideDeleteDialog)
      .then(this.getGrades);
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

  handleChecked = (event) => {
    this.setState({
      checked: event.target.checked,
    });
  }

  onUpdateGrade = (data) => {
    let newGrade = this.state.value;
    let newImportance = this.state.importance
    if (this.state.checked) {
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
        handleClassChange={this.handleClassChange}
        handleStudentChange={this.handleStudentChange}
        handleSubjectChange={this.handleSubjectChange}
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
            tooltip: 'Dodaj ocenę',
            isFreeAction: true,
            onClick: this.showDialog,
          },
          {
            icon: 'edit',
            tooltip: 'Edytuj ocenę',
            onClick: (event, rowData) => this.showDialog(event, rowData),
          },
          {
            icon: 'delete',
            tooltip: 'Usuń ocenę',
            onClick: (event, rowData) => this.showDeleteDialog(event, rowData),
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
        hideDeleteDialog={this.hideDeleteDialog}
        deleteDialogVisible={this.state.deleteDialogVisible}
        onSubmitDeleteDialog={this.onDelete}
        checked={this.state.checked}
        handleChecked={this.handleChecked}
      />
    );
  }
}

export default TeacherGradesContainer;