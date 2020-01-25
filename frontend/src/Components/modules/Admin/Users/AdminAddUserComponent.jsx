/* eslint-disable no-template-curly-in-string */
import React from 'react';
import { withFormik } from 'formik';
import { TextField, Button, Typography, Card, Grid, Select, InputLabel, MenuItem } from '@material-ui/core';
import { withSnackbar } from './../../../navigation/SnackbarContext';

const formikEnhancer = withFormik({
  enableReinitialize: true,

  mapPropsToValues: props => ({
    Email: props.user && props.user.email ? props.user.email : '',
    Login: props.user && props.user.login ? props.user.login : '',
    Password: '',
    Firstname: props.user && props.user.firstname ? props.user.firstname : '',
    Surname: props.user && props.user.surname ? props.user.surname : '',
    Role: props.user && props.user.role ? props.user.role.id : 1,
  }),

  handleSubmit: (values, { props }) => {
    props.onSave(values);
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
                id="Email"
                label="Email"
                variant="outlined"
                type="email"
                value={props.values.Email}
                className="textfield"
                onChange={props.handleChange}
              />
            </Grid>
          </Grid>
          <Grid item xs={12}>
            <Grid item xs={10} sm={3}>
              <TextField
                id="Login"
                label="Login"
                variant="outlined"
                value={props.values.Login}
                className="textfield"
                onChange={props.handleChange}
              />
            </Grid>
          </Grid>
          <Grid item xs={12}>
            <Grid item xs={10} sm={3}>
              <TextField
                id="Password"
                label="Hasło"
                variant="outlined"
                value={props.values.Password}
                type="password"
                className="textfield"
                onChange={props.handleChange}
              />
            </Grid>
          </Grid>

          <Grid item xs={12}>
            <Grid item xs={10} sm={3}>
              <TextField
                id="Firstname"
                label="Imię"
                variant="outlined"
                value={props.values.Firstname}
                className="textfield"
                onChange={props.handleChange}
              />
            </Grid>
          </Grid>

          <Grid item xs={12}>
            <Grid item xs={10} sm={3}>
              <TextField
                id="Surname"
                label="Nazwisko"
                variant="outlined"
                value={props.values.Surname}
                className="textfield"
                onChange={props.handleChange}
              />
            </Grid>
          </Grid>
          <Grid item xs={12}>
            <Grid item xs={10} sm={3}>
              <InputLabel style={{ marginTop: '5%' }}>Rola</InputLabel>
              <Select
                disabled={!props.addMode}
                value={props.values.Role}
                onChange={props.handleChange('Role')}
                onBlur={props.handleBlur('Role')}
                margin="dense"
                style={{ width: '100%' }}
                id="Role"
              >
                {console.log(props.roles)}
                {props.roles.map(item => (
                  <MenuItem key={item.id} value={item.id}>
                    {item.name}
                  </MenuItem>
                ))}
              </Select>
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
                Anuluj
              </Button>
            </Grid>
          </Grid>


        </Grid>
      </Card>
    </form>
  );
}

export default withSnackbar(formikEnhancer(SettingsComponent)); 
