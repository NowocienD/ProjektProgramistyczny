import React from 'react';
import { Snackbar } from '@material-ui/core';

const SnackbarComponent = (props) => {
  return (
    <Snackbar
      anchorOrigin={{
        vertical: 'bottom',
        horizontal: 'right',
      }}
      open={props.open}
      autoHideDuration={12000}
      onClose={props.onClose}
      message={props.message}
    >
    </Snackbar>
  );
}

export default SnackbarComponent;