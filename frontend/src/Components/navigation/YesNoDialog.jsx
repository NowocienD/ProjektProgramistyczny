import React from 'react';
import {
  Dialog,
  DialogTitle,
  DialogActions,
  DialogContentText,
  Button
} from '@material-ui/core';

const YesNoDialog = (props) => {
  return (
    <Dialog
      open={props.visible}
      onClose={props.onHide}
      maxWidth='xs'
      fullWidth
    >
      <DialogTitle>
        {props.title}
      </DialogTitle>
      <DialogContentText style={{ textAlign: 'center' }}>
        {props.content}
      </DialogContentText>
      <DialogActions>
        <Button onClick={props.onHide} color="primary">
          Nie
          </Button>
        <Button onClick={props.onSubmit} color="primary">
          Tak
        </Button>
      </DialogActions>
    </Dialog>
  )
}

export default YesNoDialog;