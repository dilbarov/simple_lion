import React, { Component } from 'react';
import axios from 'axios';
import Rubric from '../rubric/Rubric'
import EventList from '../eventList/EventList'
import Form from '../form/Form';
import SimpleMap from '../simpleMap/SimpleMap'
import './Container.css';

class Continer extends Component {
  constructor(props) {
    super(props);
    this.getEvents = this.getEvents.bind(this);
    this.setCoords = this.setCoords.bind(this);
    this.setRubric = this.setRubric.bind(this);
    this.state = {
      events: null,
      rubric: 'all',
      isForm: false,
      currentCoords: {
        lat: 50.4,
        lng: 40.5
      }
    }
  }

  getEvents() {
    const 
      lat = this.state.currentCoords.lat,
      lng = this.state.currentCoords.lng,
      rubric = this.state.rubric,
      url = `http://localhost:3002/getEvents?lat=${lat}&lng=${lng}&distance=500$rubric=${rubric}`;

    axios.get('http://localhost:3002/getEvents')
      .then(res => this.setState({ events: res.data }))
      .catch(err => console.log(err));
  }

  setCoords(val, get=true){
    this.setState({ currentCoords: val }, () => {
      if(get) { this.getEvents() }
    });
  }

  setRubric(val) {
    this.setState({ rubric: val });
  }

  componentDidMount() {
    navigator.geolocation.getCurrentPosition(position => {
      this.setCoords({
        lat: position.coords.latitude,
        lng: position.coords.longitude
      });
    });
  }

  render() {
    return(
      <div className="container-main">
        <Form />
        <SimpleMap 
          center={ this.state.currentCoords }
          setCoords={ this.setCoords }
          points={ this.state.events }
          isForm={ this.state.isForm }
        />
        { console.log(this.state) }
      </div>
    );
  }
}

export default Continer;

// <Rubric change={ this.setRubric } />
// <EventList events={ this.state.events }/>