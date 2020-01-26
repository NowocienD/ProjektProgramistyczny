import React from 'react';
import { Card, Typography, Button } from '@material-ui/core';
import MaterialTable from 'material-table';
import YesNoDialog from '../../../navigation/YesNoDialog';
import AdminAddSubjectDialog from './AdminAddSubjectDialog';

const AdminAddSubjectComponent = (props) => {
  return (
    <Card className="component-container">
      <Typography variant="h5" className="underline-title">
        Przedmioty klasy
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
        title="Usuwanie ucznia"
        content="Czy na pewno chcesz usunąć ten przedmiot z listy przedmiotów tej klasy?"
      />
      <AdminAddSubjectDialog
        leftSubjects={props.leftSubjects}
        subject={props.subject}
        handleSubjectChange={props.handleSubjectChange}
        visible={props.addDialogVisible}
        hideDialog={props.hideAddDialog}
        onSubmit={props.onAddSubject}
      />
      <Button onClick={props.goBack} style={{ float: 'right', marginTop: '2%' }} color="primary">
        Powrót
          </Button>
    </Card>
  );
}

export default AdminAddSubjectComponent;