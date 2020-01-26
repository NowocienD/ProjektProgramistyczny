/* eslint-disable no-template-curly-in-string */
import React from 'react';
import {
  Button,
  Select,
  MenuItem,
  InputLabel,
  Grid,
  Typography,
  Card,
} from '@material-ui/core';

const lessonNumbers = [1, 2, 3, 4, 5, 6, 7, 8];
const AdminAddLessonComponent = (props) => {
  return (
    <Card className="component-container">
      {props.addMode ? (
        <Typography variant="h5" className="underline-title">
          Dodawanie lekcji
          </Typography>
      ) : (
          <Typography variant="h5" className="underline-title">
            Edycja lekcji
        </Typography>
        )
      }
      <Grid container spacing={1}>
        <Grid item xs={12} sm={4}>
          <InputLabel>Przedmiot</InputLabel>
          <Select
            value={props.subject}
            onChange={props.handleSubjectChange}
            margin="dense"
            style={{ width: '100%' }}
          >
            {
              props.subjects.map(item => (
                <MenuItem key={item} value={item}>
                  {item.name}
                </MenuItem>
              ))
            }
          </Select>
        </Grid>
        <Grid item xs={8} />
        <Grid item xs={12} sm={4}>
          <InputLabel style={{ marginTop: '5%' }}>Nauczyciel</InputLabel>
          <Select
            value={props.teacher}
            onChange={props.handleTeacherChange}
            margin="dense"
            style={{ width: '100%' }}
          >
            {props.teachers.map(item => (
              <MenuItem key={item} value={item}>
                {item.firstnameSurname}
              </MenuItem>
            ))}
          </Select>
        </Grid>
        <Grid item xs={8} />
        <Grid item xs={12} sm={4}>
          <InputLabel style={{ marginTop: '5%' }}>Numer lekcji</InputLabel>
          <Select
            disabled={!props.addMode}
            value={props.lessonNumber}
            onChange={props.handleLessonNumberChange}
            margin="dense"
            style={{ width: '100%' }}
          >
            {lessonNumbers.map(item => (
              <MenuItem key={item} value={item}>
                {item}
              </MenuItem>
            ))}
          </Select>
        </Grid>
        <Grid item xs={8} />
        <Grid xs={12} sm={2} style={{ textAlign: 'right', maringTop: '5%' }}>
          <Button
            variant="contained"
            className="button"
            onClick={props.onSave}
            type="submit"
          >
            Zapisz
          </Button>
        </Grid>
        <Grid xs={12} sm={2} style={{ textAlign: 'right', maringTop: '5%' }}>
          <Button
            variant="contained"
            className="button"
            onClick={props.goBack}
            style={{marginLeft: '1%'}}
          >
            Anuluj
          </Button>
        </Grid>
      </Grid>
    </Card>

  );
}

export default AdminAddLessonComponent; 
