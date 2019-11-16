import React, { Component } from 'react';
import './App.css';
import Layout from './Components/navigation/Layout';
import 'typeface-roboto'
import LoginContainer from './Components/login/LoginContainer';
import axios from 'axios';

class App extends Component {

  constructor() {
    super();
    this.createMenu();
  }

  componentDidMount() {
    axios.get("https://localhost:44306/controller/abc")
      .then(res => {
        console.log(res);
      })
    

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
    ]
  }
  render() {
    return (
      <div className="Container">
        <Layout
          menu={this.menu}
        />
        {/* <LoginContainer /> */}

      </div>
    );
  }

}

export default App;
