import React, { Component } from 'react';
import SettingsComponent from './SettingsComponent';
import { changeMyPassword } from './../../../Actions/users';


class SettingsContainer extends Component {
  constructor() {
    super();
    this.state = {
      message: '',
      type: '',
    }
  }

  onSubmit = data => changeMyPassword(data);

  render() {
    return (
      <div>
        <SettingsComponent
          onSubmit={this.onSubmit}
          type={this.state.type}
          message={this.state.message}
        />
      </div>
    );
  };
}

export default SettingsContainer;
