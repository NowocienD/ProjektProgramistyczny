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
} from '@material-ui/core';

const formikEnhancer = withFormik({
  enableReinitialize: true,

  mapPropsToValues: props => ({
    statement: '',
    date: '',
  }),

  handleSubmit: (values, { props }) => {
    props.onAddNote(values)
      .then(() => {
        props.getNotes()
      })
      .then(props.hideDialog)
      .then(() => {
        values.statement = '';
        values.date = '';
      })
  }
});

const AddNoteDialog = (props) => {
  return (
    <Dialog
      open={props.dialogVisible}
      onClose={props.hideDialog}
      fullWidth='50%'
    >
      <DialogTitle>
        Dodawanie uwagi
        </DialogTitle>
      <DialogContent>
        <TextField
          id="statement"
          label="Treść uwagi"
          variant="outlined"
          multiline
          rows={4}
          value={props.values.statement}
          onChange={props.handleChange}
          fullWidth
          margin="normal"
        />
        <TextField
          id="date"
          label="Data uwagi"

          value={props.values.date}
          variant="outlined"
          onChange={props.handleChange}
          type="date"
          fullWidth
          margin="normal"
          InputLabelProps={{
            shrink: true,
          }}
        />
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

export default formikEnhancer(AddNoteDialog); 
