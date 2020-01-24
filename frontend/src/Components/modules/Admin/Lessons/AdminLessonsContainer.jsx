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
      lesson: {},
      classes: [],
      class: {},
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

  addNumber = (lessons) => {
    let newLessons = lessons.map((element, index) => { return { name: element, number: index+1 } });
    return newLessons;
  }

  getData = () => {
    getClassLessons(this.state.class.id, this.state.day.id)
      .then(res => {
        this.setState({
          lessons: this.addNumber(res.data.lessons),
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

  // onDeleteLesson = () => deleteLesson({LessonNumber: this.state.lesson.number-1, DayOfTheWeek: this.state.day.id, SubjectId, })
  //   .catch(error => {
  //     this.props.showMessage(error.response.data);
  //     this.hideDialog();
  //   })
  //   .then(res => {
  //     this.update();
  //     this.props.showMessage(res.data)
  //     this.hideDialog();
  //   })


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
            onClick: (event, rowData) => { this.props.history.push(`/lessons/${this.state.class.id}/${rowData.number}`) }
          },
          {
            icon: 'add',
            toolTip: 'Dodaj lekcję',
            isFreeAction: true,
            onClick: (event, rowData) => { this.props.history.push(`/lessons/${this.state.class.id}/add`) }
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
            field: 'number',
          },
          {
            title: 'Nazwa',
            field: 'name',
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