import React from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import { Card, Paper, Typography, MenuItem, Grid } from '@material-ui/core';
import Select from '@material-ui/core/Select';
import InputLabel from '@material-ui/core/InputLabel';



const StudentGradesComponent = (props) => {
  return (
  <Card className="component-container">
      <Typography variant="h5" className="underline-title">
        Oceny
      </Typography>
      <InputLabel>Przedmiot</InputLabel>
      <Select
        value={props.subject}
        onChange={props.handleSelectChange}
        className="select"
      >
        {props.subjects.map(item => (
          <MenuItem value={item}>
            {item.name}
          </MenuItem>
        ))}
      </Select>
      <Grid container>
        <Grid xs={12}>
<Paper>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell align="center">Ocena</TableCell>
              <TableCell align="center">Waga</TableCell>
              <TableCell align="center">Wystawiona przez</TableCell>
              <TableCell align="center">Temat</TableCell>
              <TableCell align="center">Data</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {props.grades.map(row => (
              <TableRow>
                <TableCell align="center">{row.value}</TableCell>
                <TableCell align="center">{row.importance}</TableCell>
                <TableCell align="center">{row.teacherFirstname} {row.teacherSurname}</TableCell>
                <TableCell align="center">{row.topic}</TableCell>
                <TableCell align="center">{row.date}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </Paper>
        </Grid>
      </Grid>
      
    </Card>
  )

};

export default StudentGradesComponent;