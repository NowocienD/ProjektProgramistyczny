import React from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import { Card, Paper, Typography, MenuItem } from '@material-ui/core';
import Select from '@material-ui/core/Select';
import InputLabel from '@material-ui/core/InputLabel';

const PresenceComponent = (props) => {
  return (
    <Card className="component-container">
      <Typography variant="h5" className="underline-title">
        Obecności
      </Typography>
      <InputLabel>Tydzień</InputLabel>
      <Select
        value={props.week}
        onChange={props.handleSelectChange}
        className="select"
      >
        {props.weeks.map(item => (
          <MenuItem value={item}>
            {item.name}
          </MenuItem>
        ))}
      </Select>
      <Paper>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell align="center"></TableCell>
              <TableCell align="center">9:00-9:45</TableCell>
              <TableCell align="center">10:00-10:45</TableCell>
              <TableCell align="center">11:00-11:45</TableCell>
              <TableCell align="center">12:00-12:45</TableCell>
              <TableCell align="center">13:00-13:45</TableCell>
              <TableCell align="center">14:00-14:45</TableCell>
              <TableCell align="center">15:00-15:45</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {props.presence.map(row => (
              <TableRow>
                {row.map((element, index) => {
                  if (index === 0) {
                    return <TableCell style={{ fontWeight: "bold" }} align="left">{element.name}</TableCell>
                  } else {
                    if (element.state === 'Obecny') {
                      return <TableCell style={{color: 'green'}} align="center">{element.name}: <b>{element.state}</b> </TableCell>
                    } else if (element.state === "Nieobecny") {
                      return <TableCell style={{color: 'red'}} align="center">{element.name}: <b>{element.state}</b> </TableCell>
                    } else {
                      return <TableCell style={{color: 'orange'}} align="center">{element.name}: <b>{element.state}</b> </TableCell>
                    }

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

export default PresenceComponent;