import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import App from './App';

// Klasa ogarniająca logowanie
class Auth extends Component {

  checkToken = () => {
    return localStorage.getItem('Token');
  }

  redirectToLoginPage = () => {
    this.props.history.push("/login");
  }
  
  // Metoda uruchamiająca się na starcie aplikacji.
  // Jeśli w pamięci nie ma tokena - przekieruj na stronę logowania
  // W przeciwnym wypadku nie rób nic (wyrenderuj stronę App)
  componentDidMount() {
    if (!this.checkToken()) {
      this.redirectToLoginPage();
    }
  }

  render() {
    return (
      <App />
    )
  }
}

export default withRouter(Auth);