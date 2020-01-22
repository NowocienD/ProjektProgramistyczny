import React from 'react';
import AdminSubjectsComponent from './AdminSubjectsComponent';
import { getAllSubjects, addSubject, deleteSubject } from '../../../Actions/subjects';
import { withSnackbar } from '../../navigation/SnackbarContext';

class AdminSubjectsContainer extends React.Component {
  constructor() {
    super();
    this.state = {
      subjects: [],
      dialogVisible: false,
      subjectId: '',
    };
  }

  showDialog = (event, data) => {
    this.setState({
      dialogVisible: true,
      subjectId: data.id,
    })
  }

  hideDialog = () => {
    this.setState({
      dialogVisible: false,
      subjectId: '',
    })
  }

  componentDidMount = () => {
    this.update();
  }

  update = () => {
    getAllSubjects()
      .then(res => {
        this.setState({
          subjects: res.data.subjectList,
        });
      });
  }

  onDeleteSubject = () => deleteSubject(this.state.subjectId)
    .catch(error => {
      this.props.showMessage(error.response.data);
      this.hideDialog();
    })
    .then(res => {
      this.update();
      this.props.showMessage(res.data)
      this.hideDialog();
    })


  onAddSubject = data => addSubject(data)
    .catch(error => {
      this.props.showMessage(error.response.data);
    })
    .then(res => {
      this.update();
      this.props.showMessage(res.data)
    })

  render() {
    return (
      <AdminSubjectsComponent
        subjects={this.state.subjects}
        editable={{
          onRowAdd: newData => this.onAddSubject(newData),
        }}
        actions={[
          {
            icon: 'edit',
            toolTip: 'Dodaj nauczyciela',
            onClick: (event, rowData) =>{this.props.history.push(`/subjects/${rowData.id}`)}
          },
          {
            icon: 'delete',
            toolTip: 'Usuń przedmiot',
            onClick: (event, rowData) => this.showDialog(event, rowData),
          }
        ]}
        columns={[
          {
            title: 'Nazwa',
            field: 'name',
          },
        ]}
        hideDialog={this.hideDialog}
        onDelete={this.onDeleteSubject}
        dialogVisible={this.state.dialogVisible}

        hideAddDialog={this.hideAddDialog}
        onSave={this.onSave}
        addDialogVisible={this.state.addDialogVisible}
      />
    );
  }
}

export default (withSnackbar(AdminSubjectsContainer))