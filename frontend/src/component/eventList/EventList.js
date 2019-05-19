import React, { Component } from 'react';
import { List } from 'semantic-ui-react'
import Event from '../event/Event'
import './EventList.css';

class EventList extends Component {
  constructor(props) {
    super(props);
    this.list = this.list.bind(this);
    this.state = {
      open: false
    };
  }
  list(arr) {
    if(!arr) return; 
    return arr.map( el => {
      return(
        <Event 
          key={ el.id }
          title={ el.title }
          comment={ el.comment }
          startTime={ el.startTime }
          locationName={ el.locationName }
        />
      )
    });
  }
  render() {
    const listEvents = this.list(this.props.events)
    return(
      <div className="eventList-main">
        <List>
          { listEvents }
        </List>
      </div>
    );
  }
}

export default EventList;