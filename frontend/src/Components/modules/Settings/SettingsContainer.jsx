import React, { Component } from 'react';
import SettingsComponent from './SettingsComponent';
import { changeMyPassword } from './../../../Actions/users';

class SettingsContainer extends Component {
  constructor() {
    super();
    this.state = {
      openSnackbar: false,
      message: '',
      type: '',
    }
  }

  showSnackbar = (message, type) => {
    this.setState({
      openSnackbar: true,
      message: message,
      type: type,
    });
  }

  hideSnackbar = () => {
    this.setState({
      openSnackbar: false,
      message: '',
    });
  }

  onSubmit = data => changeMyPassword(data);

  render() {
    return (
      <div>
        <SettingsComponent
          onSubmit={this.onSubmit}
          open={this.state.openSnackbar}
          type={this.state.type}
          message={this.state.message}
          showSnackbar={this.showSnackbar}
          hideSnackbar={this.hideSnackbar}
        />
      </div>
    );
  };
}

export default SettingsContainer;
