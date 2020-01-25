import React from 'react';
import AdminUsersComponent from './AdminUsersComponent';
import { getAllUsers, deleteUser } from '../../../../Actions/users';
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

  onDeleteUser = () => deleteUser(this.state.userId)
    .catch(error => {
      this.props.showMessage(error.response.data);
      this.hideDialog();
    })
    .then(res => {
      this.update();
      this.props.showMessage(res.data)
      this.hideDialog();
    })


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
            tooltip: 'Dodaj użytkownika',
            onClick: (event, rowData) => {this.props.history.push("/users/add")},
            isFreeAction: true,
          },         
          {
            icon: 'edit',
            tooltip: 'Edytuj użytkownika',
            onClick: (event, rowData) => {this.props.history.push(`/users/${rowData.id}`)}
          },
          rowData => ({
            icon: 'delete',
            tooltip: "Usuń użytkownika",
            hidden: !rowData.isActive,
            onClick: (event, rowData) => {
              if (rowData.isActive) {
                this.showDialog(event, rowData);
              } else {
                this.props.showMessage('Ten użytkownik już jest dezaktywowany');
              }
            },
          })
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
          {
            title: 'Aktywny',
            field: 'isActive',
            type: 'boolean'
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