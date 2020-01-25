/* eslint-disable no-template-curly-in-string */
import React from 'react';
import { withFormik } from 'formik';
import { TextField, Button, Typography, Card } from '@material-ui/core';
import { Grid } from '@material-ui/core';
import { withSnackbar } from './../../../navigation/SnackbarContext';

const formikEnhancer = withFormik({
  enableReinitialize: true,

  mapPropsToValues: props => ({
    email: props.user && props.user.email ? props.user.email : '',
    login: props.user && props.user.login ? props.user.login : '',
    password: '',
    firstname: props.user && props.user.firstname ? props.user.firstname : '',
    surname: props.user && props.user.surname ? props.user.surname : '',

  }),

  handleSubmit: (values, { props }) => {
    props.onSubmit(values)
      .then(res => {
        props.showMessage(res.data)
      })
      .catch(error => {
        props.showMessage(error.response.data);
      });
  }
});

const SettingsComponent = (props) => {
  return (
    <form onSubmit={props.handleSubmit}>
      <Card className="component-container">
        {props.addMode ? (
          <Typography variant="h5" className="underline-title" >
            Dodawanie użytkownika
         </Typography>) : (
            <Typography variant="h5" className="underline-title" >
              Edycja użytkownika
         </Typography>)}

        <Grid container>
          <Grid item xs={12}>
            <Grid item xs={10} sm={3}>
              <TextField
                id="email"
                label="Email"
                variant="outlined"
                type="email"
                value={props.values.email}
                className="textfield"
                onChange={props.handleChange}
              />
            </Grid>
          </Grid>
          <Grid item xs={12}>
            <Grid item xs={10} sm={3}>
              <TextField
                id="login"
                label="login"
                variant="outlined"
                value={props.values.login}
                className="textfield"
                onChange={props.handleChange}
              />
            </Grid>
          </Grid>
          <Grid item xs={12}>
            <Grid item xs={10} sm={3}>
              <TextField
                id="password"
                label="password"
                variant="outlined"
                value={props.values.password}
                type="password"
                className="textfield"
                onChange={props.handleChange}
              />
            </Grid>
          </Grid>

          <Grid item xs={12}>
            <Grid item xs={10} sm={3}>
              <TextField
                id="firstname"
                label="Imię"
                variant="outlined"
                value={props.values.firstname}
                className="textfield"
                onChange={props.handleChange}
              />
            </Grid>
          </Grid>

          <Grid item xs={12}>
            <Grid item xs={10} sm={3}>
              <TextField
                id="surname"
                label="Nazwisko"
                variant="outlined"
                value={props.values.surname}
                className="textfield"
                onChange={props.handleChange}
              />
            </Grid>
          </Grid>

          <Grid item xs={12}>
            <Grid item xs={10} sm={3}>
              <Button
                variant="contained"
                className="button"
                onClick={props.handleSubmit}
                type="submit"
              >
                Zapisz
              </Button>
            </Grid>
          </Grid>
          <Grid item xs={12}>
            <Grid item xs={10} sm={3}>
              <Button
                variant="contained"
                className="button"
                onClick={props.goBack}
              >
                Zapisz
              </Button>
            </Grid>
          </Grid>


        </Grid>
      </Card>
    </form>
  );
}

export default withSnackbar(formikEnhancer(SettingsComponent)); 
