import React from 'react';
import AdminLessonsComponent from './AdminLessonsComponent';
import { getClassLessons, deleteLesson } from '../../../../Actions/lessonPlan';
import { getAllClasses } from '../../../../Actions/class';
import { withSnackbar } from '../../../navigation/SnackbarContext';

const allDays = [
  { id: 0, name: "Poniedziałek" },
  { id: 1, name: "Wtorek" },
  { id: 2, name: "Śrooda" },
  { id: 3, name: "Czwartek" },
  { id: 4, name: "Piątek" },
];

class AdminLessonsContainer extends React.Component {
  constructor() {
    super();
    this.state = {
      lessons: [],
      dialogVisible: false,
      lesson: '',
      classes: [],
      class: '',
      days: allDays,
      day: allDays[0],
    };
  }

  showDialog = (event, data) => {
    this.setState({
      dialogVisible: true,
      lesson: data,
    })
  }

  hideDialog = () => {
    this.setState({
      dialogVisible: false,
      lesson: '',
    })
  }

  componentDidMount = () => {
    this.fetchAll();
  }


  handleClassChange = (event) => {
    this.setState({
      class: event.target.value,
    }, () => {
      this.getData();
    });
  }

  handleDayChange = (event) => {
    this.setState({
      day: event.target.value,
    }, () => {
      this.getData();
    });
  }

  getData = () => {
    getClassLessons(this.state.class.id, this.state.day.id)
      .then(res => {
        this.setState({
          lessons: res.data.lessons.map((element, index) => {
            return {
              lessonNumber: index,
              ...element,
            }
          }),
        });
      });
  }

  fetchAll = () => {
    getAllClasses()
      .then(res => {
        this.setState({
          classes: res.data.classList,
          class: res.data.classList ? res.data.classList[0] : '',
        }, () => {
          this.getData();
        });
      })
  }

  onDeleteLesson = () => deleteLesson(this.state.lesson.id)
    .then(res => {
      this.getData();
      this.props.showMessage(res.data)
      this.hideDialog();
    })
    .catch(error => {
      this.props.showMessage(error.response.data);
      this.hideDialog();
    })

    onEdit = (event, rowData) => {
      if (rowData.id === 0) {
        this.props.showMessage("Nie można modyfikować lekcji, która nie istnieje");
      } else {
        this.props.history.push(`/lessons/${this.state.class.id}/${this.state.day.id}/${rowData.id}`);
      }
    }

  // onModifyLesson = data => addSubject(data)
  //   .catch(error => {
  //     this.props.showMessage(error.response.data);
  //   })
  //   .then(res => {
  //     this.update();
  //     this.props.showMessage(res.data)
  //   })

  render() {
    return (
      <AdminLessonsComponent
        classes={this.state.classes}
        class={this.state.class}
        days={this.state.days}
        day={this.state.day}
        handleClassChange={this.handleClassChange}
        handleDayChange={this.handleDayChange}
        lessons={this.state.lessons}
        actions={[
          {
            icon: 'edit',
            toolTip: 'Modyfikuj lekcję',
            onClick: this.onEdit,
          },
          {
            icon: 'add',
            toolTip: 'Dodaj lekcję',
            isFreeAction: true,
            onClick: (event, rowData) => { this.props.history.push(`/lessons/${this.state.class.id}/${this.state.day.id}/add`) }
          },
          {
            icon: 'delete',
            toolTip: 'Usuń lekcję',
            onClick: (event, rowData) => this.showDialog(event, rowData),
          }
        ]}
        columns={[
          {
            title: 'Numer lekcji',
            field: 'lessonNumber',
          },
          {
            title: 'Nazwa',
            field: 'name',
          },
          {
            title: 'Nauczyciel',
            field: 'teacherName',
          },
        ]}
        hideDialog={this.hideDialog}
        onDelete={this.onDeleteLesson}
        dialogVisible={this.state.dialogVisible}
      />
    );
  }
}

export default (withSnackbar(AdminLessonsContainer))