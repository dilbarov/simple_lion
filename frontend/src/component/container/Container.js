import React, { Component } from 'react';
import axios from 'axios';
import Rubric from '../rubric/Rubric'
import EventList from '../eventList/EventList'
import Form from '../form/Form';
import SimpleMap from '../simpleMap/SimpleMap'
import { Button, Sidebar, Menu } from 'semantic-ui-react';
import './Container.css';

class Continer extends Component {
  constructor(props) {
    super(props);
    this.changeVisible = this.changeVisible.bind(this);
    this.getEvents = this.getEvents.bind(this);
    this.setCoords = this.setCoords.bind(this);
    this.setRubric = this.setRubric.bind(this);
    this.buttonClick = this.buttonClick.bind(this);
    this.contentRender = this.contentRender.bind(this);
    this.changeContent = this.changeContent.bind(this);
    this.state = {
      visible: false,
      events: null,
      rubric: 'any',
      isForm: false,
      currentCoords: {
        lat: 50.4,
        lng: 40.5
      },
      content: 1
    }
  }
  changeVisible() {
    this.setState({ visible: false });
  }

  buttonClick(num) {
    console.log(this.state);
    return () => {
      this.setState({ content: num });
      if (num === 3) {
        this.setState({ isForm: true });
      }
    }
  }

  contentRender() {
    if (this.state.content === 1) {
      return (
        <SimpleMap
          center={this.state.currentCoords}
          setCoords={this.setCoords}
          points={this.state.events}
          isForm={this.state.isForm}
        />
      )
    } else if (this.state.content === 2) {
      return (<EventList events={this.state.events} changeVisible={ this.changeVisible }/>)
    } else if (this.state.content === 3) {
      return (<Form coords={this.state.currentCoords} getEvents={this.getEvents} getContent={this.buttonClick(1)} />)
    }
  }


  changeContent(num) {
    return () => this.setState({content: num});
  }

  getEvents() {
    const
      lat = this.state.currentCoords.lat,
      lng = this.state.currentCoords.lng,
      rubric = this.state.rubric,
      url = `http://95.217.1.188:5000/api/Events?lat=${lat}&lng=${lng}&distance=1000000&rubric=${rubric}`;

    axios.get(url)
      .then(res => this.setState({ events: res.data }))
      .catch(err => console.log(err));
  }

  setCoords(val) {
    this.setState({ currentCoords: val }, () => this.getEvents());
  }

  setRubric(val) {
    this.setState({ rubric: val }, () => this.getEvents());
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
    return (
      <div className="container-main">
        <div className={"header"}>
          <div className={"header_left"}>
            <Button 
              circular color='facebook' 
              icon={ this.state.visible ? 'close' : 'bars'} 
              onClick={() => this.setState({ visible: !this.state.visible }) }
            />
          </div>
          {/*<div className={"header_center"}></div>*/}
          <div className={"header_right"}>
            <Button circular color='facebook' icon='paper plane outline' />
            <Button circular color='facebook' icon='list' onClick={this.buttonClick(2)} />
            <Button circular color='facebook' icon='map outline' onClick={this.buttonClick(1)} />
            <Button circular color='facebook' icon='plus' onClick={this.buttonClick((3))} />
          </div>
        </div>
        <div className={"content"}>
          {this.contentRender()}
          <Sidebar
            as={ Menu }
            visible={ true }
          >
            <EventList events={this.state.events} />
          </Sidebar>
          <div className="sidebarRubric">
            <Sidebar
              animation='overlay'
              as={ Menu }
              visible={ this.state.visible }
            >
              <Rubric change={ this.setRubric } changeVisible={ this.changeVisible }/>
            </Sidebar>
          </div>
        </div>
      </div>
    );
  }
}

export default Continer;

// <Rubric change={ this.setRubric } />
// <EventList events={ this.state.events }/>
{/* <SimpleMap 
  center={ this.state.currentCoords }
  setCoords={ this.setCoords }
  points={ this.state.events }
  isForm={ this.state.isForm }
/> */}