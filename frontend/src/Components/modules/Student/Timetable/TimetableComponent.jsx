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
          <TableBody>
            {props.timetable.map(row => (
              <TableRow>
                {row.length !== 0 && (
                  <TableCell style={{fontWeight: "bold"}} align="left">{row[0].day}</TableCell>
                  )}
                  {row.map(element => (
                    <TableCell align="left">{element.start + ' - '  + element.end + ' ' + element.name}</TableCell>
                  ))}

              </TableRow>
            ))}
          </TableBody>
        </Table>
      </Paper>
    </Card>
  )

};

export default TimetableComponent;