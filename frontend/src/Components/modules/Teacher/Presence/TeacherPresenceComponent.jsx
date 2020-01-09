import React from 'react';
import {
  TextField,
  Select,
  InputLabel,
  Typography,
  Card,
  MenuItem,
  Grid,
} from '@material-ui/core';

import { MaterialTable } from 'material-table';

const TeacherPresenceComponent = (props) => {
  return (
    <Card className="component-container">

      <Typography variant="h5" className="underline-title">
        Obecności
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
          <InputLabel>Numer lekcji</InputLabel>
          <Select
            value={props.lesson}
            onChange={props.handleLessonChange}
            className="select-fluid"
          >
            {props.lessons.map(item => (
              <MenuItem key={item} value={item}>
                {item}
              </MenuItem>
            ))}
          </Select>
        </Grid>
        <Grid item xs={4}>
          <InputLabel>Data</InputLabel>
          <TextField
            type="date"
            value={props.date}
            onChange={props.handleDateChange}
            className="select-fluid"
          />
        </Grid>
      </Grid>
      <MaterialTable
        columns={props.columns}
        data={props.data}
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
    </Card>
  )
}

export default TeacherPresenceComponent;