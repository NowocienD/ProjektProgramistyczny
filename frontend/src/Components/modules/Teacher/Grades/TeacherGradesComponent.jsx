import React from 'react';
import { Card, Typography, Select, InputLabel, MenuItem, Grid } from '@material-ui/core';
import MaterialTable from 'material-table';
import AddGradeDialog from './AddGradeDialog';

const TeacherGradesComponent = (props) => {
  return (
    <Card className="component-container">
      <Typography variant="h5" className="underline-title">
        Oceny
      </Typography>
      <Grid container spacing={3}>
        <Grid item xs={4}>
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
        <Grid item xs={4}>
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
        <Grid item xs={4}>
          <InputLabel>Przedmiot</InputLabel>
          <Select
            value={props.subject}
            onChange={props.handleStudentChange}
            className="select-fluid"
          >
            {props.subjects.map(item => (
              <MenuItem value={item}>
                {item.name}
              </MenuItem>
            ))}
          </Select>
        </Grid>
      </Grid>
      <MaterialTable
        columns={props.columns}
        data={props.grades}
        actions={props.actions}
        title="Oceny"
        options={
          {
            emptyRowsWhenPaging: false,
          }
        }
      />
      <AddGradeDialog
        dialogVisible={props.dialogVisible}
        hideDialog={props.hideDialog}
        getGrades={props.getGrades}
        onAddGrade={props.onAddGrade}
      />
    </Card>
  );
}

export default TeacherGradesComponent;

