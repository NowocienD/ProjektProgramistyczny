import React, { Component } from 'react';
import LoginComponent from './LoginComponent';
import { login } from './../../Actions/auth';
import { withRouter } from 'react-router-dom';

class LoginContainer extends Component {

  onSubmit = (data) => login(data);

  redirectToMainPage = () => {
    this.props.history.push('/');
  }

  render() {
    return (
      <div>
        <LoginComponent
          login={this.onSubmit}
          redirectToMainPage={this.redirectToMainPage}
        />
      </div>
    )
  }
}

  export default withRouter(LoginContainer);