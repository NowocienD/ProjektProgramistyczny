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
    Class: props.user && props.user.classId ? props.user.classId : 1,
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

        <Grid container spacing={3}>
          <Grid item xs={10} sm={4}>
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
          <Grid item xs={10} sm={4}>
            <TextField
              id="Login"
              label="Login"
              variant="outlined"
              value={props.values.Login}
              className="textfield"
              onChange={props.handleChange}
            />
          </Grid>
          <Grid item xs={10} sm={4}>
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
          <Grid item xs={10} sm={4}>
            <TextField
              id="Firstname"
              label="Imię"
              variant="outlined"
              value={props.values.Firstname}
              className="textfield"
              onChange={props.handleChange}
            />
          </Grid>
          <Grid item xs={10} sm={4}>
            <TextField
              id="Surname"
              label="Nazwisko"
              variant="outlined"
              value={props.values.Surname}
              className="textfield"
              onChange={props.handleChange}
            />
          </Grid>



          <Grid item xs={10} sm={4}>
            <InputLabel style={{ marginTop: '5%' }}>Rola</InputLabel>
            <Select
              disabled={!props.addMode}
              value={props.values.Role}
              onChange={props.handleChange('Role')}
              onBlur={props.handleBlur('Role')}
              margin="dense"
              style={{ width: '100%' }}
              variant="outlined"
              id="Role"
            >
              {props.roles.map(item => (
                <MenuItem key={item.id} value={item.id}>
                  {item.name}
                </MenuItem>
              ))}
            </Select>
          </Grid>

          {props.values.Role === 1 && (

            <Grid item xs={10} sm={4}>
              <InputLabel style={{ marginTop: '5%' }}>Klasa</InputLabel>
              <Select
                variant="outlined"
                value={props.values.Class}
                onChange={props.handleChange('Class')}
                onBlur={props.handleBlur('Class')}
                margin="dense"
                style={{ width: '100%' }}
                id="Class"
              >
                {props.classes.map(item => (
                  <MenuItem key={item.id} value={item.id}>
                    {item.name}
                  </MenuItem>
                ))}
              </Select>
            </Grid>

          )}
          <Grid item xs={12} style={{float: 'right'}}>
            <Grid item xs={10} sm={2}>
              <Button
                variant="contained"
                className="button"
                onClick={props.handleSubmit}
                type="submit"
              >
                Zapisz
              </Button>
            </Grid>
            <Grid item xs={10} sm={2}>
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
    </form >
  );
}

export default withSnackbar(formikEnhancer(SettingsComponent)); 
