import React, { Component } from 'react';
import AdminAddUserComponent from './AdminAddUserComponent';
import { getUserData, addUser, editUser } from '../../../../Actions/users';
import { getAllRoles } from '../../../../Actions/roles';
import { withSnackbar }  from '../../../navigation/SnackbarContext';

class AdminAddUserContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {
      userId: this.props.match.params.userId,
      user: {},
      addMode: this.props.match.params.userId === 'add',
      roles: [],
      role: '',
    }
  }

  handleRoleChange = (event) => {
    this.setState({
      role: event.target.value,
    });
  }

  componentDidMount = () => {
    getAllRoles()
        .then(res => {
          this.setState({
            roles: res.data.roles,
          });
        });
    if (!this.state.addMode) {
      getUserData(this.state.userId)
        .then(res => {
          this.setState({
            user: res.data,
          });
        });
    }
  }

  onSave = (values) => {
    const v = {...values, Role: {id: values.Role}, IsActive: true, };
    if (this.state.addMode) {
      addUser(v)
      .catch(error => {
        this.props.showMessage(error.response.data);
      })
      .then(res => {
        this.goBack();
        this.props.showMessage(res.data)
      });
    } else {
      editUser(v, this.state.userId)
      .catch(error => {
        this.props.showMessage(error.response.data);
      })
      .then(res => {
        this.goBack();
        this.props.showMessage(res.data)
      });
    } 
  }

  goBack = () => {
    this.props.history.push("/users");
  }

  render() {
    return (
      <div>
        {console.log(this.state.roles)}
        <AdminAddUserComponent
          user={this.state.user}
          addMode={this.state.addMode}
          goBack={this.goBack}
          onSave={this.onSave}
          roles={this.state.roles}
        />
      </div>
    );
  };
}

export default withSnackbar(AdminAddUserContainer);
