import React from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import { Card, Paper, Typography, MenuItem } from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';
import Select from '@material-ui/core/Select';
import InputLabel from '@material-ui/core/InputLabel';

const useStyles = makeStyles(() => ({
  underline: {
    borderBottom: "1px solid currentColor",
    lineHeight: 2,
    marginBottom: "2%",
    marginTop: "0",
  },
}));

const TimetableComponent = (props) => {
  const classes = useStyles();

  return (
    <Card style={{ paddingLeft: "4%", paddingRight: "4%", paddingBottom: "4%" }}>
      <Typography variant="h5" className={classes.underline}>
        Plan lekcji
      </Typography>
      <Paper>
        <Table>
          {/* <TableHead>
            <TableRow>
              <TableCell align="center">8:00-8:45</TableCell>
              <TableCell align="center">9:00-9:45</TableCell>
              <TableCell align="center">10:00-10:45</TableCell>
              <TableCell align="center">11:00-11:45</TableCell>
              <TableCell align="center">12:00-12:45</TableCell>
              <TableCell align="center">13:00-13:45</TableCell>
              <TableCell align="center">14:00-11:45</TableCell>
              <TableCell align="center">12:00-12:45</TableCell>
            </TableRow>
          </TableHead> */}
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