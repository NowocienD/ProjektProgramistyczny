import React, { Component } from 'react';
import GradesComponent from './GradesComponent';
import { getMySubjects } from './../../../../Actions/subjects';
import { getMyGrades } from './../../../../Actions/grades';
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

  componentDidMount = () => {
    getMySubjects()
      .then(res => {
        console.log(res)
        this.setState({
          subjects: res.data.subjectList,
          subject: res.data.subjectList[0],
        }, () => {
          this.updateGrades();
        });
      })
  }


  updateGrades = () => {
    getMyGrades(this.state.subject.id)
      .then(res => {
        console.log(res);
        this.setState({
          grades: res.data.gradeDTOs,
        });
      });
  }

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
