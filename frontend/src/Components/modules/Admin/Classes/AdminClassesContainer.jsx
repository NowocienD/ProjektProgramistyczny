import React from 'react';
import AdminClassesComponent from './AdminClassesComponent';
import { getAllClasses, addClass, deleteClass, editClass } from '../../../../Actions/subjects';
import { withSnackbar } from '../../../navigation/SnackbarContext';

class AdminClassesContainer extends React.Component {
  constructor() {
    super();
    this.state = {
      classes: [],
      dialogVisible: false,
      classId: '',
    };
  }

  showDialog = (event, data) => {
    this.setState({
      dialogVisible: true,
      classId: data.id,
    })
  }

  hideDialog = () => {
    this.setState({
      dialogVisible: false,
      classId: '',
    })
  }

  componentDidMount = () => {
    this.update();
  }

  update = () => {
    getAllClasses()
      .then(res => {
        this.setState({
          classes: res.data.classList,
        });
      });
  }

  onDeleteClass = () => deleteClass(this.state.classId)
    .catch(error => {
      this.props.showMessage(error.response.data);
      this.hideDialog();
    })
    .then(res => {
      this.update();
      this.props.showMessage(res.data)
      this.hideDialog();
    })


  onAddClass = data => addClass(data)
    .catch(error => {
      this.props.showMessage(error.response.data);
    })
    .then(res => {
      this.update();
      this.props.showMessage(res.data)
    })

  render() {
    return (
      <AdminClassesComponent
        classes={this.state.classes}
        editable={{
          onRowAdd: newData => this.onAddClass(newData),
          //onRowEdit: newData => this.onEditClass(newData),
        }}
        actions={[
          {
            icon: 'edit',
            toolTip: 'Dodaj nauczyciela',
            onClick: (event, rowData) => { this.props.history.push(`/classes/${rowData.id}`) }
          },
          {
            icon: 'delete',
            toolTip: 'Usuń klasę',
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
        onDelete={this.onDeleteClass}
        dialogVisible={this.state.dialogVisible}

        onSave={this.onSave}
      />
    );
  }
}

export default (withSnackbar(AdminClassesContainer))