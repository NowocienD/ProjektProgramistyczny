import React from 'react';
import { getClassSubjects, getAllSubjects } from '../../../../Actions/subjects';
import { addClassSubject, deleteClassSubject } from '../../../../Actions/classSubject';
import { withSnackbar } from '../../../navigation/SnackbarContext';
import AdminAddSubjectComponent from './AdminAddSubjectComponent';

class AdminAddSubjectContainer extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      dialogVisible: false,
      classId: props.match.params.classId,
      subjects: [],
      subject: '',
      addDialogVisible: false,
      leftSubjects: [],
    };
  }

  componentDidMount = () => {
    this.update();
  }

  showDialog = (event, data) => {
    this.setState({
      dialogVisible: true,
      subject: data,
    })
  }

  hideDialog = () => {
    this.setState({
      dialogVisible: false,
      subject: '',
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

  onDeleteSubject = () => deleteClassSubject(this.state.classId, this.state.subject.id)
    .catch(error => {
      this.props.showMessage(error.response.data);
      this.hideDialog();
    })
    .then(res => {
      this.update();
      this.props.showMessage(res.data)
      this.hideDialog();
    })


  onAddSubject = () => addClassSubject(this.state.classId, this.state.subject.id)
    .catch(error => {
      this.props.showMessage(error.response.data);
      this.hideAddDialog();
    })
    .then(res => {
      this.update();
      this.hideAddDialog();
      this.props.showMessage(res.data)
    })

  goBack = () => {
    this.props.history.push("/classes");
  }
  update = () => {
    getClassSubjects(this.state.classId)
      .then(res => {
        this.setState({
          subjects: res.data.subjectList,
        }, () => {
          getAllSubjects()
            .then(res => {
              const temp = res.data.subjectList.filter(n => {
                for (let i=0; i < this.state.subjects.length; i++) {
                  if (this.state.subjects[i].id === n.id) {
                    return false;
                  }
                }
                return true;
              });
              this.setState({
                leftSubjects: temp,
              });
            });
        });
      });
  }

  handleSubjectChange = (event) => {
    this.setState({
      subject: event.target.value,
    });
  }

  render() {
    return (
      <AdminAddSubjectComponent
        subjects={this.state.subjects}
        actions={[
          {
            icon: 'add',
            tooltip: 'Dodaj przedmiot',
            isFreeAction: true,
            onClick: this.showAddDialog,
          },
          {
            icon: 'delete',
            tooltip: 'Usuń przedmiot z klasy',
            onClick: (event, rowData) => this.showDialog(event, rowData),
          }
        ]}
        columns={[
          {
            title: 'Przedmiot',
            field: 'name',
          },
        ]}
        hideDialog={this.hideDialog}
        onDelete={this.onDeleteSubject}
        dialogVisible={this.state.dialogVisible}
        hideAddDialog={this.hideAddDialog}
        showAddDialog={this.showAddDialog}
        addDialogVisible={this.state.addDialogVisible}
        onAddSubject={this.onAddSubject}
        leftSubjects={this.state.leftSubjects}
        handleSubjectChange={this.handleSubjectChange}
        goBack={this.goBack}
      />
    );
  }
}

export default withSnackbar(AdminAddSubjectContainer);