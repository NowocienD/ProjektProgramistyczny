import React from 'react';
import TextField from '@material-ui/core/TextField';
import { Button, Paper } from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';
import { withFormik } from 'formik';
import { Grid } from '@material-ui/core';
import { Typography } from '@material-ui/core';
import { Card } from '@material-ui/core';
import styles from './../../layout';

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
      });
  }
});

const useStyles = makeStyles(theme => ({
  background: {
    background: styles.COLORS.primary,
    height: "100%",
    width: "100%",
    position: "absolute",
    paddingTop: "10%",
  },

  loginPanel: {
    width: "30%",
    background: "white",
    marginLeft: "auto",
    marginRight: "auto",
    padding: "2%"
  },
  textfield: {
    marginTop: "4%",
    marginBottom: "4%",
    width: "100%"
  },
  underline: {
    borderBottom: "1px solid currentColor",
    lineHeight: 2,
    marginBottom: "2%",
    marginTop: "0",
  },
}));

const LoginComponent = (props) => {
  const {
    values,
  } = props;
  const classes = useStyles();

  return (
    <div className="login-background">
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
                >
                  Zaloguj się
              </Button>
              </Grid>
            </Grid>
      </Card>

    </div>
  )
}

export default formikEnhancer(LoginComponent);