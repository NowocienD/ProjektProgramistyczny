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
  Checkbox,
} from '@material-ui/core';
import { withSnackbar } from './../../../navigation/SnackbarContext';

const formikEnhancer = withFormik({
  enableReinitialize: true,

  mapPropsToValues: props => ({
    date: props.grade && props.grade.date ? props.grade.date : '',
    topic: props.grade && props.grade.topic ? props.grade.topic : '',
  }),

  handleSubmit: (values, { props }) => {
    const onSubmit = props.grade && props.grade.value ? props.onUpdateGrade : props.onAddGrade;
    onSubmit(values)
      .then(res => {
        props.showMessage(res.data)
        props.getGrades()
        props.hideDialog()
        values.topic = '';
        values.date = '';
      })
      .catch(error => {
        props.showMessage(error.response.data);
      })
  }
});
const grades = [1, 2, 3, 4, 5];

const AddGradeDialog = (props) => {
  return (
    <Dialog
      open={props.dialogVisible}
      onClose={props.hideDialog}
    >
      {props.grade && props.grade.value &&
        <DialogTitle>
          Edycja oceny
       </DialogTitle>
      }
      {!(props.grade && props.grade.value) &&
        <DialogTitle>
          Dodawanie oceny
       </DialogTitle>
      }

      <DialogContent>
        <Grid container spacing={1}>
          <Grid item xs={12} sm={2}>
            <InputLabel>Ocena</InputLabel>
            <Select
              value={props.value}
              onChange={props.handleValue}
              margin="dense"
            >
              {grades.map(item => (
                <MenuItem key={item} value={item}>
                  {item}
                </MenuItem>
              ))}
            </Select>
          </Grid>
          <Grid item xs={12} sm={2}>
            <InputLabel>Waga</InputLabel>
            <Select
              disabled={props.checked}
              value={props.checked && props.grade && props.grade.importance ? props.grade.importance : props.importance}
              onChange={props.handleImportance}
              margin="dense"
            >
              {grades.map(item => (
                <MenuItem key={item} value={item}>
                  {item}
                </MenuItem>
              ))}
            </Select>
          </Grid>
          {props.grade && props.grade.value &&
            <Grid item xs={12} sm={2}>
              <InputLabel>Poprawa</InputLabel>
              <Checkbox
                checked={props.checked}
                value="secondary"
                onChange={props.handleChecked}
              />
            </Grid>
          }
          <Grid item xs={12} sm={props.grade && props.grade.value ? 6 : 8}>
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
              value={props.checked && props.grade && props.grade.topic ? props.grade.topic : props.values.topic}
              disabled={props.checked}
              variant="outlined"
              onChange={props.handleChange}
              fullWidth
              margin="dense"
            />
          </Grid>
        </Grid>
      </DialogContent>
      <DialogActions>
        <Button onClick={props.hideDialog} color="primary">
          Anuluj
          </Button>
        <Button onClick={props.handleSubmit} color="primary">
          Zapisz
        </Button>
      </DialogActions>
    </Dialog>
  );
}

export default withSnackbar(formikEnhancer(AddGradeDialog)); 
