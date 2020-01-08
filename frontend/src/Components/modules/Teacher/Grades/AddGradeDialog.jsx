/* eslint-disable no-template-curly-in-string */
import React from 'react';
import { withFormik } from 'formik';
import {
  TextField,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  Select,
  MenuItem,
  InputLabel,
  Grid,
} from '@material-ui/core';

const formikEnhancer = withFormik({
  enableReinitialize: true,

  mapPropsToValues: props => ({
    value: 5,
    importance: 5,
    date: new Date(),
    topic: '',
  }),

  handleSubmit: (values, { props }) => {
    props.onAddGrade(values)
      .then(() => {
        props.getGrades()
      })
      .then(props.hideDialog)
      .then(() => {
        values.topic = '';
        values.date = new Date();
        values.value = 5;
        values.importance = 5;
      })
  }
});

const AddGradeDialog = (props) => {
  return (
    <Dialog
      open={props.dialogVisible}
      onClose={props.hideDialog}
    >
      <DialogTitle>
        Dodawanie oceny
        </DialogTitle>
      <DialogContent>
        <Grid container spacing={1}>
          <Grid item xs={12} sm={2}>
            <InputLabel>Ocena</InputLabel>
            <Select
              id="value"
              value={props.values.value}
              onChange={props.handleChange}
              maxWidth
              margin="normal"
            >
              {[1, 2, 3, 4, 5].map(item => (
                <MenuItem value={item}>
                  {item}
                </MenuItem>
              ))}
            </Select>
          </Grid>
          <Grid item xs={12} sm={2}>
            <InputLabel>Waga</InputLabel>
            <Select
              id="importance"
              value={props.values.importance}
              onChange={props.handleChange}
              autoWidth
              margin="normal"
            >
              {[1, 2, 3, 4, 5].map(item => (
                <MenuItem value={item}>
                  {item}
                </MenuItem>
              ))}
            </Select>
          </Grid>
          <Grid item xs={12} sm={8}>
            <TextField
              fullWidth
              id="date"
              label="Data oceny"
              value={props.values.date}
              variant="outlined"
              onChange={props.handleChange}
              type="date"
              InputLabelProps={{
                shrink: true,
              }}
            />
          </Grid> 
          <Grid item xs={12}>
            <TextField
              id="topic"
              label="Temat"
              value={props.values.topic}
              variant="outlined"
              onChange={props.handleChange}
              fullWidth
              margin="normal"
            />
          </Grid>
        </Grid>
      </DialogContent>
      <DialogActions>
        <Button onClick={props.hideDialog} color="primary">
          Anuluj
          </Button>
        <Button onClick={props.handleSubmit} color="primary">
          Dodaj
        </Button>
      </DialogActions>
    </Dialog>
  );
}

export default formikEnhancer(AddGradeDialog); 
