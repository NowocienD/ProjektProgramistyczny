import React from 'react';
import MaterialTable from 'material-table';
import { Card, Typography } from '@material-ui/core';
import YesNoDialog from '../../../navigation/YesNoDialog';

const AdminSubjectsComponent = (props) => {
  return (
    <Card className="component-container">
      <Typography variant="h5" className="underline-title">
        Oceny
      </Typography>

      <MaterialTable
        columns={props.columns}
        data={props.subjects}
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

export default AdminSubjectsComponent;