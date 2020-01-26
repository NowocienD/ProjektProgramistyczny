import React from 'react';
import { Card, Typography } from '@material-ui/core';
import MaterialTable from 'material-table';
import YesNoDialog from '../../../navigation/YesNoDialog';

const AdminUsersComponent = (props) => {
  return (
    <Card className="component-container">
      <Typography variant="h5" className="underline-title">
        Użytkownicy
    </Typography>
      <MaterialTable
        columns={props.columns}
        data={props.users}
        actions={props.actions}
        title="Nauczyciele"
        options={
          {
            emptyRowsWhenPaging: false,
            pageSize: 10,
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
        title="Usuwanie użytkownika"
        content="Czy na pewno chcesz usunąć tego użytkownika?"
      />
    </Card>
  );
}

export default AdminUsersComponent;