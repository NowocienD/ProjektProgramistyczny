import React from 'react';
import TextField from '@material-ui/core/TextField';
import { Button } from '@material-ui/core';
import { withFormik } from 'formik';
import { Grid } from '@material-ui/core';
import { Typography } from '@material-ui/core';
import { Card } from '@material-ui/core';
import { withSnackbar } from './../navigation/SnackbarContext';


const formikEnhancer = withFormik({
  enableReinitialize: true,

  mapPropsToValues: props => ({
    Login: '',
    Password: '',
  }),

  handleSubmit: (values, { props }) => {
    props.login(values)
      .then(() => {
        props.redirectToMainPage();
      })
      .catch(error => {
        props.showMessage(error.response.data);
      });
  }
});

const LoginComponent = (props) => {
  const {
    values,
  } = props;

  return (
    <div className="login-background">
      <form>
        <Card className="login-panel">
          <Typography variant="h5" className="underline">
            Logowanie
      </Typography>
          <Grid container>
            <Grid item xs={12}>
              <TextField
                id="Login"
                label="Login"
                variant="outlined"
                value={values.Login}
                onChange={props.handleChange}
                className="textfield"
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                id="Password"
                label="Hasło"
                variant="outlined"
                type="password"
                value={values.Password}
                className="textfield"
                onChange={props.handleChange}
              />
            </Grid>
            <Grid item xs={12}>
              <Button
                variant="contained"
                className="button"
                onClick={props.handleSubmit}
                type="submit"
              >
                Zaloguj się
              </Button>
            </Grid>
          </Grid>
        </Card>
      </form>
    </div>
  )
}

export default withSnackbar(formikEnhancer(LoginComponent));