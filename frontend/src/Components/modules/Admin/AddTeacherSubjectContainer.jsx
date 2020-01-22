import React from 'react';
import AddTeacherSubjectComponent from './AdminSubjectsComponent';
import {  getSubjectTeachers } from '../../../Actions/subjects';
import { withSnackbar } from '../../navigation/SnackbarContext';

class AddTeacherSubjectContainer extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      allTeachers: [],
      selectedTeachers: [],
      dialogVisible: false,
      subjectId: props.match.params.subjectId,
      teacherId: '',
    };
  }

  componentDidMount = () => {
    this.update();
  }

  showDialog = (event, data) => {
    this.setState({
      dialogVisible: true,
      teacherId: data.id,
    })
  }

  hideDialog = () => {
    this.setState({
      dialogVisible: false,
      teacherId: '',
    })
  }

  // onDeleteTeacher = () => deleteSubjectTeacher(this.state.subjectId)
  //   .catch(error => {
  //     this.props.showMessage(error.response.data);
  //     this.hideDialog();
  //   })
  //   .then(res => {
  //     this.update();
  //     this.props.showMessage(res.data)
  //     this.hideDialog();
  //   })


  // onAddTeacher = data => addSubjectTeacher(data)
  //   .catch(error => {
  //     this.props.showMessage(error.response.data);
  //   })
  //   .then(res => {
  //     this.update();
  //     this.props.showMessage(res.data)
  //   })

  update = () => {
    getSubjectTeachers(this.state.subjectId)
      .then(res => {
        console.log(res.data);
        this.setState({
          selectedTeachers: res.data.teacherSubjects,
        });
      });
  }

  render() {
    return (
      <AddTeacherSubjectComponent
        teachers={this.state.selectedTeachers}
        actions={[
          {
            icon: 'add',
            toolTip: 'Dodaj ocenę',
            isFreeAction: true,
            //onClick: (event, rowData) => this.showAddDialog(event, rowData),
          },
          {
            icon: 'delete',
            toolTip: 'Usuń przedmiot',
            onClick: (event, rowData) => this.showDialog(event, rowData),
          }
        ]}
        columns={[
          {
            title: 'Nauczyciel',
            field: 'teacherFirstnameSurname',
          },
        ]}
        hideDialog={this.hideDialog}
        onDelete={this.onDeleteSubject}
        dialogVisible={this.state.dialogVisible}
      />
    );
  }
}

export default withSnackbar(AddTeacherSubjectContainer);