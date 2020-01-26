import React from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import { Card, Paper, Typography, MenuItem } from '@material-ui/core';
import Select from '@material-ui/core/Select';
import InputLabel from '@material-ui/core/InputLabel';
import CloseIcon from '@material-ui/icons/Close';
import CheckIcon from '@material-ui/icons/Check';
import MinimizeIcon from '@material-ui/icons/Minimize';
import PlaylistAddCheckIcon from '@material-ui/icons/PlaylistAddCheck';
import Grid from '@material-ui/core/Grid';

const days = [
  'Poniedziałek',
  'Wtorek',
  'Środa',
  'Czwartek',
  'Piątek',
];

const StudentPresenceComponent = (props) => {
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
            {item.begin} - {item.end}
          </MenuItem>
        ))}
      </Select>
      <Grid container>
        <Grid xs={12}>
          <Paper>
            <Table>
              <TableHead>
                <TableRow>
                  <TableCell align="center" ></TableCell>
                  <TableCell align="center">8:00-8:45</TableCell>
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
                {console.log(props.presence)}
                {props.presence.map((row, index) => (
                  <TableRow>
                    <TableCell style={{ fontWeight: "bold" }} align="left">{days[index]}</TableCell>
                    {row.attendances.map((element, index) => {
                      if (element === 'nieobecny') {
                        return <TableCell align="center" style={{ color: 'red' }}><CloseIcon />NO</TableCell>
                      } else if (element === 'obecny') {
                        return <TableCell align="center" style={{ color: 'green' }}><CheckIcon />O</TableCell>
                      } else if (element === 'nie wpisano') {
                        return <TableCell align="center" style={{ color: 'gray' }}><MinimizeIcon />NW</TableCell>
                      } else {
                        return <TableCell align="center" style={{ color: 'orange' }}><PlaylistAddCheckIcon />USP</TableCell>
                      }
                    })}
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

export default StudentPresenceComponent;