/* eslint-disable no-template-curly-in-string */
import React from 'react';
import { withFormik } from 'formik';
import { TextField, Button, Typography, Card } from '@material-ui/core';
import { Grid } from '@material-ui/core';
import { withSnackbar } from './../../navigation/SnackbarContext';

const formikEnhancer = withFormik({
  enableReinitialize: true,

  mapPropsToValues: props => ({
    //email: props.user ? props.user.email,
    password: '',
    newpassword: '',
    newpasswordrepeat: '',
  }),

  handleSubmit: (values, { props }) => {
    if (values.newpassword !== values.newpasswordrepeat) {
      props.showMessage("Hasła nie zgadzają się");
    } else {
      const data = {
        OldPassword: values.password,
        NewPassword: values.newpassword,
      };
      props.onSubmit(data)
        .then(res => {
          props.showMessage(res.data)
        })
        .catch(error => {
          props.showMessage(error.response.data);
        });
    }
  }
});

const SettingsComponent = (props) => {
  return (
    <form onSubmit={props.handleSubmit}>
      <Card className="component-container">
        <Typography variant="h5" className="underline-title" >
          Opcje konta
      </Typography>
        <Grid container>
          <Grid item xs={12}>
            {/* <Grid item xs={10} sm={3}>
            <TextField
              id="email"
              label="E-mail"
              variant="outlined"
              value={props.values.email}
              disabled
              className="textfield"
            />
          </Grid> */}
          </Grid>
          <Grid item xs={12}>
            <Grid item xs={10} sm={3}>
              <TextField
                id="password"
                label="Hasło"
                variant="outlined"
                type="password"
                value={props.values.password}
                className="textfield"
                onChange={props.handleChange}
              />
            </Grid>
          </Grid>

          <Grid item xs={12}>
            <Grid item xs={10} sm={3}>
              <TextField
                id="newpassword"
                label="Nowe Hasło"
                variant="outlined"
                type="password"
                value={props.values.newpassword}
                className="textfield"
                onChange={props.handleChange}
              />
            </Grid>
          </Grid>

          <Grid item xs={12}>
            <Grid item xs={10} sm={3}>
              <TextField
                id="newpasswordrepeat"
                label="Powtórz Nowe Hasło"
                variant="outlined"
                type="password"
                value={props.values.newpasswordrepeat}
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


        </Grid>
      </Card>
    </form>
  );
}

export default withSnackbar(formikEnhancer(SettingsComponent)); 
