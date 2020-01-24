import React from 'react';
import { Card, Typography, Select, InputLabel, MenuItem, Grid } from '@material-ui/core';
import MaterialTable from 'material-table';
import AddNoteDialog from './AddNoteDialog';

const TeacherNotesComponent = (props) => {
  return (

    <Card className="component-container">
      <Typography variant="h5" className="underline-title">
        Uwagi
      </Typography>
      <Grid container spacing={3}>
        <Grid item xs={6}>
          <InputLabel>Klasa</InputLabel>
          <Select
            value={props.class}
            onChange={props.handleClassChange}
            className="select-fluid"
          >
            {props.classes.map(item => (
              <MenuItem value={item}>
                {item.name}
              </MenuItem>
            ))}
          </Select>
        </Grid>
        <Grid item xs={6}>
          <InputLabel>Uczeń</InputLabel>
          <Select
            value={props.student}
            onChange={props.handleStudentChange}
            className="select-fluid"
          >
            {props.students.map(item => (
              <MenuItem value={item}>
                {item.firstname} {item.surname}
              </MenuItem>
            ))}
          </Select>
        </Grid>
      </Grid>
      <MaterialTable
        columns={props.columns}
        data={props.notes}
        actions={props.actions}
        title="Uwagi"
        options={
          {
            emptyRowsWhenPaging: false,
          }
        }
      />
      <AddNoteDialog
        dialogVisible={props.dialogVisible}
        hideDialog={props.hideDialog}
        getNotes={props.getNotes}
        onAddNote={props.onAddNote}
      />
    </Card>
  );
}

export default TeacherNotesComponent;

