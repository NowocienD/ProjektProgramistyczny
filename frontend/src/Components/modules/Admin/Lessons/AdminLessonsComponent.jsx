import React from 'react';
import {
  Select,
  InputLabel,
  Typography,
  Card,
  MenuItem,
  Grid,
} from '@material-ui/core';

import MaterialTable from 'material-table';
import YesNoDialog from '../../../navigation/YesNoDialog';

const AdminLessonsComponent = (props) => {
  return (
    <Card className="component-container">

      <Typography variant="h5" className="underline-title">
        Obecności
      </Typography>
      <Grid container spacing={3}>
        <Grid item xs={12} sm={6}>
          <InputLabel>Klasa</InputLabel>
          <Select
            value={props.class}
            onChange={props.handleClassChange}
            className="select-fluid"
          >
            {props.classes.map(item => (
              <MenuItem key={item.id} value={item}>
                {item.name}
              </MenuItem>
            ))}
          </Select>
        </Grid>
        <Grid item xs={12} sm={6}>
          <InputLabel>Dzień</InputLabel>
          <Select
            value={props.day}
            onChange={props.handleDayChange}
            className="select-fluid"
          >
            {props.days.map(item => (
              <MenuItem key={item.number} value={item}>
                {item.name}
              </MenuItem>
            ))}
          </Select>
        </Grid>
      </Grid>
      <MaterialTable
        columns={props.columns}
        data={props.lessons}
        title="Obecności"
        actions={props.actions}
        options={
          {
            emptyRowsWhenPaging: false,
            pageSize: 8
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
      <YesNoDialog
        onHide={props.hideDialog}
        onSubmit={props.onDelete}
        visible={props.dialogVisible}
        title="Usuwanie lekcji"
        content="Czy na pewno chcesz usunąć tę lekcję?"
      />
    </Card>
  )
}

export default AdminLessonsComponent;