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
  Select,
  MenuItem,
  InputLabel,
  Grid,
  Checkbox,
} from '@material-ui/core';

const formikEnhancer = withFormik({
  enableReinitialize: true,

  mapPropsToValues: props => ({
    date: props.grade && props.grade.date ? props.grade.date : '',
    topic: props.grade && props.grade.topic ? props.grade.topic : '',
    correction: true,
  }),

  handleSubmit: (values, { props }) => {
    const onSubmit = props.grade && props.grade.value ? props.onUpdateGrade : props.onAddGrade;
    onSubmit(values)
      .then(() => {
        props.getGrades()
      })
      .then(props.hideDialog)
      .then(() => {
        values.topic = '';
        values.date = '';
      })
  }
});
const grades = [1, 2, 3, 4, 5];

const AddGradeDialog = (props) => {
  const [checked, setChecked] = React.useState(false);

  const handleCheckbox = event => {
    setChecked(event.target.checked);
  };

  return (
    <Dialog
      open={props.dialogVisible}
      onClose={props.hideDialog}
    >
      {props.grade && props.grade.value && 
       <DialogTitle>
         Edycja oceny
       </DialogTitle>
      }
        {!(props.grade && props.grade.value) && 
       <DialogTitle>
         Dodawanie oceny
       </DialogTitle>
      }
     
      <DialogContent>
        <Grid container spacing={1}>
          <Grid item xs={12} sm={2}>
            <InputLabel>Ocena</InputLabel>
            <Select
              value={props.value}
              onChange={props.handleValue}
              margin="normal"
            >
              {grades.map(item => (
                <MenuItem key={item} value={item}>
                  {item}
                </MenuItem>
              ))}
            </Select>
          </Grid>
          <Grid item xs={12} sm={2}>
            <InputLabel>Waga</InputLabel>
            <Select
              disabled={checked}
              value={checked && props.grade && props.grade.importance ? props.grade.importance : props.importance}
              onChange={props.handleImportance}
              margin="normal"
            >
              {grades.map(item => (
                <MenuItem key={item} value={item}>
                  {item}
                </MenuItem>
              ))}
            </Select>
          </Grid>
          {props.grade && props.grade.value &&
            <Grid item xs={12} sm={2}>
              <InputLabel>Poprawa</InputLabel>
              <Checkbox
                defaultValue="false"
                checked={checked}
                value="secondary"
                onChange={handleCheckbox}
              />
            </Grid>
          }
          <Grid item xs={12} sm={props.grade && props.grade.value ? 6 : 8}>
            <TextField
              fullWidth
              id="date"
              label="Data oceny"
              value={props.values.date}
              variant="outlined"
              onChange={props.handleChange}
              type="date"
              InputLabelProps={{
                shrink: true,
              }}
            />
          </Grid>
          <Grid item xs={12}>
            <TextField
              id="topic"
              label="Temat"
              value={checked && props.grade && props.grade.topic ? props.grade.topic : props.values.topic}
              disabled={checked}
              variant="outlined"
              onChange={props.handleChange}
              fullWidth
              margin="normal"
            />
          </Grid>
        </Grid>
      </DialogContent>
      <DialogActions>
        <Button onClick={props.hideDialog} color="primary">
          Anuluj
          </Button>
        <Button onClick={props.handleSubmit} color="primary">
          Zapisz
        </Button>
      </DialogActions>
    </Dialog>
  );
}

export default formikEnhancer(AddGradeDialog); 
