/* eslint-disable no-template-curly-in-string */
import React from 'react';
import {
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
import { withSnackbar } from './../../../navigation/SnackbarContext';

const AdminAddSubjectDialog = (props) => {
  return (
    <Dialog
      open={props.visible}
      onClose={props.hideDialog}
      maxWidth="sm"
      fullWidth
    >
      <DialogTitle>
        Dodaj nauczyciela
      </DialogTitle>

      <DialogContent>
        <Grid container spacing={1}>
          <Grid item xs={12}>
            <InputLabel>Przedmiot</InputLabel>
            <Select
              value={props.teacher}
              onChange={props.handleSubjectChange}
              margin="dense"
              style={{ width: '100%' }}
            >
              {props.leftSubjects.map(item => (
                <MenuItem key={item.id} value={item}>
                  {item.name}
                </MenuItem>
              ))}
            </Select>
          </Grid>
        </Grid>
      </DialogContent>
      <DialogActions>
        <Button onClick={props.hideDialog} color="primary">
          Anuluj
          </Button>
        <Button onClick={props.onSubmit} color="primary">
          Zapisz
        </Button>
      </DialogActions>
    </Dialog >
  );
}

export default withSnackbar(AdminAddSubjectDialog); 
