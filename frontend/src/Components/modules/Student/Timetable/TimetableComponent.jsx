import React from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import { Card, Paper, Typography } from '@material-ui/core';

const TimetableComponent = (props) => {
  return (
    <Card className="component-container">
      <Typography variant="h5" className="underline-title">
        Plan lekcji
      </Typography>
      <Paper>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell align="center"></TableCell>
              <TableCell align="center">9:00-9:45</TableCell>
              <TableCell align="center">10:00 - 10:45</TableCell>
              <TableCell align="center">11:00 - 11:45</TableCell>
              <TableCell align="center">12:00 - 12:45</TableCell>
              <TableCell align="center">13:00 - 13:45</TableCell>
              <TableCell align="center">14:00 - 14:45</TableCell>
              <TableCell align="center">15:00 - 15:45</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {props.timetable.map(row => (
              <TableRow>
                {row.map((element, index) => {
                  if (index === 0) {
                    return <TableCell style={{ fontWeight: "bold" }} align="left">{element.name}</TableCell>
                  } else {
                    return <TableCell align="center">{element.name}</TableCell>
                  }
                })}
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </Paper>
    </Card>
  )

};

export default TimetableComponent;