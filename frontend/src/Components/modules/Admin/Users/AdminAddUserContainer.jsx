import React, { Component } from 'react';
import AdminAddUserComponent from './AdminAddUserComponent';
import { getUserData } from '../../../../Actions/users';
class AdminAddUserContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {
      userId: this.props.match.params.userId,
      user: {},
      addMode: this.props.match.params.userId === 'add',
      roles: [{id:0, name:"student"}]
    }
  }

  componentDidMount = () => {
    if (!this.state.addMode) {
      getUserData(this.state.userId)
        .then(res => {
          this.setState({
            user: res.data,
          });
        })
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
        />
      </div>
    );
  };
}

export default AdminAddUserContainer;
