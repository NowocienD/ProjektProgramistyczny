import React from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import { Card, Paper, Typography, MenuItem } from '@material-ui/core';
import Select from '@material-ui/core/Select';
import InputLabel from '@material-ui/core/InputLabel';

const GradesComponent = (props) => {
  const rows = [
    {
      grade: 5,
      weight: 2,
      teacher: "Kowalski",
      topic: "Sprawdzian z ułamków",
    },
    {
      grade: 3,
      weight: 1,
      teacher: "Kowalski",
      topic: "Sprawdzian z geometrii",
    },
  ];

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
      <Paper>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell align="center">Ocena</TableCell>
              <TableCell align="center">Waga</TableCell>
              <TableCell align="center">Wystawiona przez</TableCell>
              <TableCell align="center">Temat</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {props.grades.map(row => (
              <TableRow>
                <TableCell align="center">{row.grade}</TableCell>
                <TableCell align="center">{row.weight}</TableCell>
                <TableCell align="center">{row.teacher}</TableCell>
                <TableCell align="center">{row.topic}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </Paper>
    </Card>
  )

};

export default GradesComponent;