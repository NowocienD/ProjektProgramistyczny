import React, { Component } from 'react';
import AdminAddUserComponent from './AdminAddUserComponent';
import { getUserData } from '../../../../Actions/users';
import { getAllRoles } from '../../../../Actions/roles';

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
    if (!this.state.addMode) {
      getAllRoles()
        .then(res => {
          this.setState({
            roles: res.data.roles,
          });
        });
      getUserData(this.state.userId)
        .then(res => {
          this.setState({
            user: res.data,
          });
        });
    }
  }

  onSave = (values) => {

  }

  goBack = () => {
    this.props.history.push("/users");
  }

  render() {
    return (
      <div>
        <AdminAddUserComponent
          user={this.state.user}
          addMode={this.state.addMode}
          goBack={this.goBack}
          roles={this.state.roles}
          role={this.state.role}
          handleRoleChange={this.handleRoleChange}
        />
      </div>
    );
  };
}

export default AdminAddUserContainer;
