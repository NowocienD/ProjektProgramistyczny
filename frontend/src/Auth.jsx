import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import App from './App';
import { getLoggedUserData } from './Actions/users';

// Klasa ogarniająca logowanie
class Auth extends Component {
  constructor() {
    super();
    this.state = {
      user: {},
    }
  }

  checkToken = () => {
    return localStorage.getItem('Token');
  }

  redirectToLoginPage = () => {
    this.props.history.push("/login");
  }

  // Metoda uruchamiająca się na starcie aplikacji.
  // Jeśli w pamięci nie ma tokena - przekieruj na stronę logowania
  // W przeciwnym wypadku pobierz dane zalogowanego użytkownika
  componentDidMount() {
    if (!this.checkToken()) {
      this.redirectToLoginPage();
    } else {
      getLoggedUserData()
        .then(res => {
          this.setState({
            user: res.data,
          })
        })
    }
  }

  render() {
    return (
      <App
        user={this.state.user}
      />
    )
  }
}

export default withRouter(Auth);