import React from 'react';
import AddTeacherSubjectComponent from './AddTeacherSubjectComponent';
import {  getSubjectTeachers, deleteSubjectTeacher, addSubjectTeacher } from '../../../../Actions/subjects';
import {  getAllTeachers } from '../../../../Actions/teacher';
import { withSnackbar } from '../../../navigation/SnackbarContext';

class AddTeacherSubjectContainer extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      leftTeachers: [],
      selectedTeachers: [],
      dialogVisible: false,
      subjectId: props.match.params.subjectId,
      teacher: '',
      addDialogVisible: false,
    };
  }

  componentDidMount = () => {
    this.update();
  }

  showDialog = (event, data) => {
    this.setState({
      dialogVisible: true,
      teacher: data,
    })
  }

  hideDialog = () => {
    this.setState({
      dialogVisible: false,
      teacher: '',
    })
  }

  showAddDialog = (event, data) => {
    this.setState({
      addDialogVisible: true,
    })
  }

  hideAddDialog = () => {
    this.setState({
      addDialogVisible: false,
    })
  }

  onDeleteTeacher = () => deleteSubjectTeacher(this.state.subjectId, this.state.teacher.id)
    .catch(error => {
      this.props.showMessage(error.response.data);
      this.hideDialog();
    })
    .then(res => {
      this.update();
      this.props.showMessage(res.data)
      this.hideDialog();
    })


  onAddTeacher = () => addSubjectTeacher(this.state.subjectId, this.state.teacher.id)
    .catch(error => {
      this.props.showMessage(error.response.data);
      this.hideAddDialog();
    })
    .then(res => {
      this.update();
      this.hideAddDialog();
      this.props.showMessage(res.data)
    })

  update = () => {
    getSubjectTeachers(this.state.subjectId)
      .then(res => {
        this.setState({
          selectedTeachers: res.data.teacherSubjects,
        }, () => {
          getAllTeachers()
            .then(res => {
              const temp = res.data.teachers.filter(n => {
                for (let i=0; i < this.state.selectedTeachers.length; i++) {
                  if (this.state.selectedTeachers[i].id === n.id) {
                    return false;
                  }
                }
                return true;
              });
              this.setState({
                leftTeachers: temp,
              });
            })
        });
      });
  }

  handleTeacherChange = (event) => {
    this.setState({
      teacher: event.target.value,
    });
  }

  render() {
    return (
      <AddTeacherSubjectComponent
        teachers={this.state.selectedTeachers}
        actions={[
          {
            icon: 'add',
            tooltip: 'Dodaj nauczyciela',
            isFreeAction: true,
            onClick: this.showAddDialog,
          },
          {
            icon: 'delete',
            tooltip: 'Usuń nauczyciela',
            onClick: (event, rowData) => this.showDialog(event, rowData),
          }
        ]}
        columns={[
          {
            title: 'Nauczyciel',
            field: 'firstnameSurname',
          },
        ]}
        hideDialog={this.hideDialog}
        onDelete={this.onDeleteTeacher}
        dialogVisible={this.state.dialogVisible}

        leftTeachers={this.state.leftTeachers}
        teacher={this.state.teacher}
        handleTeacherChange={this.handleTeacherChange}
        hideAddDialog={this.hideAddDialog}
        showAddDialog={this.showAddDialog}
        addDialogVisible={this.state.addDialogVisible}
        onAddTeacher={this.onAddTeacher}
      />
    );
  }
}

export default withSnackbar(AddTeacherSubjectContainer);