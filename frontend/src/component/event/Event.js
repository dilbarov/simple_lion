import React, { Component } from 'react';
import { List } from 'semantic-ui-react'
import './Event.css';

class Event extends Component {
  constructor(props) {
    super(props);
    this.body = this.body.bind(this);
    this.openBody = this.openBody.bind(this);
    this.state = {
      open: false
    };
  }
  openBody() {
    this.setState({ open: !this.state.open })
  }
  body() {
    if(this.state.open) {
      return(
        <List.Description>
          <div className="event-comment">{ this.props.comment }</div>
          <div className="event-date">{ this.props.startTime }</div>
          <div className="event-location">{ this.props.locationName }</div>
        </List.Description>
      )
    }
  }
  render() {
    return(
      <div className="event-main">
        <List.Item 
          onClick={ () => {
            this.openBody();
          } 
        }>
          <div className="event-list">{ this.props.title }</div>
          { this.body() }
        </List.Item>
      </div>
    );
  }
}

export default Event;