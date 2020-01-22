import React from 'react';
import { Button, Grid, Card, Typography } from '@material-ui/core';
import { MaterialTable } from 'material-table';
import YesNoDialog from '../../navigation/YesNoDialog';

const AddTeacherSubjectComponent = (props) => {
  return (
    <Card className="component-container">
    <Typography variant="h5" className="underline-title">
      Nnauczyciele uczący tego przedmiotu
    </Typography>
    {console.log(props.teachers)}
    <MaterialTable
      columns={props.columns}
      data={props.teachers}
      actions={props.actions}
      title="Przedmioty"
      editable={props.editable}
      options={
        {
          emptyRowsWhenPaging: false,
        }
      }
      localization={{
        header: {
          actions: 'Akcje',
        },
        body: {
          emptyDataSourceMessage: 'Brak danych do wyświetlenia',
        }
      }}
    />
     <YesNoDialog
      onHide={props.hideDialog}
      onSubmit={props.onDelete}
      visible={props.dialogVisible}
      title="Usuwanie przemiotu"
      content="Czy na pewno chcesz usunąć ten przedmiot?"
    />
    </Card>
  );
}

export default AddTeacherSubjectComponent;