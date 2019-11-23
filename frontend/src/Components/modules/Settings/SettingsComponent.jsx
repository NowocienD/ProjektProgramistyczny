import React from 'react';
import { withFormik } from 'formik';
import { TextField, Button, Typography, Card } from '@material-ui/core';
import { Grid } from '@material-ui/core';

const formikEnhancer = withFormik({
  enableReinitialize: true,

  mapPropsToValues: props => ({
    id: props.user ? props.user.id : 0,
    email: props.user ? props.user.email : 'email@email.com',
    password: '',
    newpassword: '',
    newpasswordrepeat: '',
  }),

  handleSubmit: (values, { props }) => {
    console.log("dosth");
    // TODO:backend
  }
});

const SettingsComponent = (props) => {
  const {
    values,
    touched,
    errors,
    status,
    handleChange,
    handleBlur,
  } = props;

  return (
    <Card className="component-container">
      <Typography variant="h5" className="underline-title" >
        Opcje konta
      </Typography>
      <Grid container>
        <Grid item xs={12}>
          <Grid item xs={10} sm={3}>
            <TextField
              id="email"
              label="E-mail"
              variant="outlined"
              value={values.email}
              disabled
              className="textfield"
            />
          </Grid>
        </Grid>
        <Grid item xs={12}>
          <Grid item xs={10} sm={3}>
            <TextField
              id="password"
              label="Hasło"
              variant="outlined"
              value={values.password}
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
              value={values.newpassword}
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
              value={values.newpasswordrepeat}
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
            // onClick={}TODO
            >
              Zapisz
              </Button>
          </Grid>
        </Grid>


      </Grid>

    </Card>
  );
}

export default formikEnhancer(SettingsComponent); 
