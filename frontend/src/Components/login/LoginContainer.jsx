import React, { Component } from 'react';
import LoginComponent from './LoginComponent';
import { login, random } from './../../Actions/login';

class LoginContainer extends Component {
  constructor() {
    super();
    this.state = {
      isLogged: false,
      role: '',
    }
  }

  onSubmit = (data) => login (data);
    
  render() {
    return (
      (this.state.isLogged === false ? (
        <div>
          <LoginComponent
            login={this.onSubmit}
          />
        </div>
      ) : (
          this.props.children
        ))

    );
  }
}

export default LoginContainer;