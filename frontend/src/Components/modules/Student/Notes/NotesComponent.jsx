import React from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import { Card, Paper, Typography } from '@material-ui/core';

const GradesComponent = (props) => {

  return (
  <Card className="component-container">
      <Typography variant="h5" className="underline-title">
        Uwagi
      </Typography>
    
      <Paper>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell align="center">Wystawiona przez</TableCell>
              <TableCell align="center">Data</TableCell>
              <TableCell align="center">Treść</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {props.notes.map(row => (
              <TableRow>
                <TableCell align="center">{row.via}</TableCell>
                <TableCell align="center">{row.date}</TableCell>
                <TableCell align="center">{row.content}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </Paper>
    </Card>
  )

};

export default GradesComponent;