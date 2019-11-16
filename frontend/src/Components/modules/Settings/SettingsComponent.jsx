import React from 'react';
import { withFormik } from 'formik';
import { TextField, Button, Typography } from '@material-ui/core';
import Paper from '@material-ui/core/Paper';
import { makeStyles } from '@material-ui/core/styles';
import { Grid } from '@material-ui/core';
import styles from './../../../layout';

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

const useStyles = makeStyles(() => ({
  root: {
    padding: "1%",
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
  }
}));

const SettingsComponent = (props) => {
  const {
    values,
    touched,
    errors,
    status,
    handleChange,
    handleBlur,
  } = props;
  const classes = useStyles();
  return (
    <Paper className={classes.root}>
      <Typography variant="h5" className={classes.underline} >
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
              className={classes.textfield}
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
              className={classes.textfield}
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
              value={values.newpassword}
              className={classes.textfield}
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
              value={values.newpasswordrepeat}
              className={classes.textfield}
              onChange={props.handleChange}
            />
          </Grid>
        </Grid>

        <Grid item xs={12}>
          <Grid item xs={10} sm={3}>
            <Button
              variant="contained"
              style={styles.BUTTON}
              className={classes.textfield}
            // onClick={}TODO
            >
              Zapisz
              </Button>
          </Grid>
        </Grid>


      </Grid>

    </Paper>
  );
}

export default formikEnhancer(SettingsComponent); 
