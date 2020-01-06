import React, { Component } from 'react';
import PresenceComponent from './PresenceComponent';

class PresenceContainer extends Component {
  constructor() {
    super();
    this.state = {
      weeks: [{ name: "25.11.2019 - 30.12.2019" }, { name: "16.12.2019 - 20.12.2019" }],
      week: {},
      presence: [],
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
      week: event.target.value,
    }, () => {
      if (Object.keys(event.target.value).length === 0) {
        this.setState({
          presence: [],
        })
      } else {
        this.setState({
          presence :
          [
            [
              {
                name: "Poniedziałek",
                day: true,
              },
              {
                name: "Matematyka",
                state: "Obecny"
              },
              {
                name: "Angielski",
                state: "Niebecny"
              },
              {
                name: "Biologia",
                state: "Nieobecny"
              },
              {
                name: "Chemia",
                state: "Obecny"
              },
              {
                name: "Polski",
                state: "Obecny"
              },
              {
                name: "Historia",
                state: "Obecny"
              },
              {
                name: "Chemia",
                state: "Usprawiedliwione"
              }
            ],
            [
              {
                name: "Wtorek",
                day: true,
              },
              {
                name: "Matematyka",
                state: "Nieobecny"
              },
              {
                name: "Angielski",
                state: "Obecny"
              },
              {
                name: "Biologia",
                state: "Obecny"
              },
              {
                name: "Chemia",
                state: "Obecny"
              },
              {
                name: "Polski",
                state: "Obecny"
              },
              {
                name: "Historia",
                state: "Obecny"
              },
              {
                name: "Chemia",
                state: "Obecny"
              }
            ],
            [
              {
                name: "Środa",
                day: true,
              },
              {
                name: "Matematyka",
                state: "Obecny"
              },
              {
                name: "Angielski",
                state: "Obecny"
              },
              {
                name: "Biologia",
                state: "Obecny"
              },
              {
                name: "Chemia",
                state: "Obecny"
              },
              {
                name: "Polski",
                state: "Obecny"
              },
              {
                name: "Historia",
                state: "Obecny"
              },
              {
                name: "Chemia",
                state: "Obecny"
              }
            ],
            [
              {
                name: "Czwartek",
                day: true,
              },
              {
                name: "Matematyka",
                state: "Obecny"
              },
              {
                name: "Angielski",
                state: "Obecny"
              },
              {
                name: "Biologia",
                state: "Obecny"
              },
              {
                name: "Chemia",
                state: "Obecny"
              },
              {
                name: "Polski",
                state: "Obecny"
              },
              {
                name: "Historia",
                state: "Obecny"
              },
              {
                name: "Chemia",
                state: "Usprawiedliwone"
              }
            ],
            [
              {
                name: "Piątek",
                day: true,
              },
              {
                name: "Matematyka",
                state: "Obecny"
              },
              {
                name: "Angielski",
                state: "Obecny"
              },
              {
                name: "Biologia",
                state: "Obecny"
              },
              {
                name: "Chemia",
                state: "Obecny"
              },
              {
                name: "Polski",
                state: "Obecny"
              },
              {
                name: "Historia",
                state: "Obecny"
              },
              {
                name: "Chemia",
                state: "Obecny"
              }
            ],
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
        <PresenceComponent
          weeks={this.state.weeks}
          handleSelectChange={this.handleSelectChange}
          week={this.state.week}
          presence={this.state.presence}
        />
      </div>
    );
  };
}
export default PresenceContainer;
