import React from 'react';
import { Card, Typography, Select, InputLabel, MenuItem, Grid } from '@material-ui/core';
import MaterialTable from 'material-table';
import AddGradeDialog from './AddGradeDialog';
import YesNoDialog from './../../../navigation/YesNoDialog';

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
              <MenuItem key={item.name} value={item}>
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
              <MenuItem key={item.id} value={item}>
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
              <MenuItem key={item.id} value={item}>
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
        localization={{
          header: {
            actions: 'Akcje',
          },
          body: {
            emptyDataSourceMessage: 'Brak danych do wyświetlenia',
          }
        }}
      />
      <AddGradeDialog
        dialogVisible={props.dialogVisible}
        hideDialog={props.hideDialog}
        getGrades={props.getGrades}
        onAddGrade={props.onAddGrade}
        onUpdateGrade={props.onUpdateGrade}
        grade={props.grade}
        handleValue={props.handleValue}
        handleImportance={props.handleImportance}
        value={props.value}
        importance={props.importance}
        checked={props.checked}
        handleChecked={props.handleChecked}
      />
      <YesNoDialog
        onHide={props.hideDeleteDialog}
        onSubmit={props.onSubmitDeleteDialog}
        visible={props.deleteDialogVisible}
        title="Usuwanie oceny"
        content="Czy na pewno chcesz usunąć tę ocenę?"
      />
    </Card>
  );
}

export default TeacherGradesComponent;

