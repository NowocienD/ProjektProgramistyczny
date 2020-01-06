import React, { Component } from 'react';
import GradesComponent from './GradesComponent';
import { getSubjects } from './../../../../Actions/subjects';
import { getGrades } from './../../../../Actions/grades';
import CircularProgress from '@material-ui/core/CircularProgress';

class GradesContainer extends Component {
  constructor() {
    super();
    this.state = {
      subjects: [{ name: "Matematyka" }, { name: "Polski" }, { name: "Biologia" }, { name: "Chemia" }],
      subject: {},
      grades: [],
    };
  }

  // componentDidMount = () => {
  //   getSubjects()
  //     .then(res => {
  //       this.setState({
  //         subjects: res.data,
  //         subject: res.data[0],
  //       }, () => {
  //         this.updateGrades();
  //       });
  //     })
  // }


  // updateGrades = () => {
  //   getGrades(this.state.subject.id)
  //     .then(res => {
  //       this.setState({
  //         grades: res.data,
  //       });
  //     });
  // }

  handleSelectChange = (event) => {
    this.setState({
      subject: event.target.value,
    }, () => {
      if (Object.keys(event.target.value).length === 0) {
        this.setState({
          grades: [],
        })
      } else {
        this.setState({
          grades: [
            {
              grade: 5,
              weight: 2,
              teacher: "Kowalski",
              topic: "Sprawdzian1",
            },
            {
              grade: 3,
              weight: 1,
              teacher: "Kowalski",
              topic: "Sprawdzian2",
            },
            {
              grade: 4,
              weight: 2,
              teacher: "Kowalski",
              topic: "Sprawdzian3",
            },
          ]
        })
      }

      // }, () => {
      //   this.updateGrades();
      // });
    }
    );
  }

  render() {
    return (
      <div>
        <GradesComponent
          subjects={this.state.subjects}
          handleSelectChange={this.handleSelectChange}
          subject={this.state.subject}
          grades={this.state.grades}
        />
      </div>
    );
  };
}
export default GradesContainer;
