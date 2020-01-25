import React from 'react';
import AdminUsersComponent from './AdminUsersComponent';
import { getAllUsers } from '../../../../Actions/users';
import { withSnackbar } from '../../../navigation/SnackbarContext';

class AdminUsersContainer extends React.Component {
  constructor() {
    super();
    this.state = {
      users: [],
      dialogVisible: false,
      userId: '',

    };
  }

  showDialog = (event, data) => {
    this.setState({
      dialogVisible: true,
      userId: data.id,
    })
  }

  hideDialog = () => {
    this.setState({
      dialogVisible: false,
      userId: '',
    })
  }

  componentDidMount = () => {
    this.update();
  }

  update = () => {
    getAllUsers()
      .then(res => {
        this.setState({
          users: res.data.users,
        });
      });
  }

  // onDeleteUser = () => deleteUser(this.state.userId)
  //   .catch(error => {
  //     this.props.showMessage(error.response.data);
  //     this.hideDialog();
  //   })
  //   .then(res => {
  //     this.update();
  //     this.props.showMessage(res.data)
  //     this.hideDialog();
  //   })


  // onAddSubject = data => addUser(data)
  //   .catch(error => {
  //     this.props.showMessage(error.response.data);
  //   })
  //   .then(res => {
  //     this.update();
  //     this.props.showMessage(res.data)
  //   })

  render() {
    return (
      <AdminUsersComponent
        users={this.state.users}
        actions={[
          {
            icon: 'add',
            toolTip: 'Dodaj nauczyciela',
            onClick: (event, rowData) => {this.props.history.push("/users/add")},
            isFreeAction: true,
          },
          {
            icon: 'delete',
            toolTip: 'Usuń nauczyciela',
            onClick: (event, rowData) => this.showDialog(event, rowData),
          },
          {
            icon: 'edit',
            toolTip: 'Edytuj nauczyciela',
            onClick: (event, rowData) => {this.props.history.push(`/users/${rowData.id}`)}
          }
        ]}
        columns={[
          {
            title: 'Login',
            field: 'login',
          },
          {
            title: 'E-mail',
            field: 'email',
          },
          {
            title: 'Imię',
            field: 'firstname',
          },
          {
            title: 'Nazwisko',
            field: 'surname',
          },
          {
            title: 'Rola',
            field: 'role.name',
          },
        ]}
        hideDialog={this.hideDialog}
        onDelete={this.onDeleteUser}
        dialogVisible={this.state.dialogVisible}
      />
    );
  }
}

export default (withSnackbar(AdminUsersContainer))