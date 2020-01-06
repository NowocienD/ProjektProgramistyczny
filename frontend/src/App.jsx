import React, { Component } from 'react';
import './App.css';
import Layout from './Components/navigation/Layout';
import 'typeface-roboto'
import { logout } from './Actions/auth';
import { withRouter } from 'react-router-dom';

class App extends Component {

  constructor() {
    super();
    this.createMenu();
  }

  logout = () => {
    logout();
    this.redirectToLoginPage();
  }

  redirectToLoginPage() {
    this.props.history.push('/login');
  }
    
  createMenu = () => {
    this.menu = [
      {
        key: 'timetable',
        name: "Plan zajęć",
        icon: 'calendar_today',
        to: '/timetable'
      },
      {
        key: 'grades',
        name: "Oceny",
        icon: 'check',
        to: '/grades'
      },
      {
        key: 'presence',
        name: "Obecności",
        icon: 'bookmark',
        to: '/presence',
      },
      {
        key: 'notes',
        name: "Uwagi",
        icon: 'warning',
        to: '/notes',
      },
    ]
  }
  
  render() {
    return (
      <div className="Container">
        <Layout
          menu={this.menu}
          logout={this.logout}
          user={this.props.user}
        />
      </div>
    );
  }

}

export default withRouter(App);
