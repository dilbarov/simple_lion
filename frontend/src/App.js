import React, { Component } from 'react';
import axios from 'axios';
import './App.css';

class App extends Component {
  constructor(props) {
    super(props);
    this.get = this.get
    this.state = {
      test: 'old'
    }
  }
  get() {
    axios.get('http://localhost:3002/getUser')
      .then(res => {
        this.setState({ test: res.data });
        console.log(res);
      })
      .catch(err => console.log(err));
  }
  componentDidMount() {
    this.get()
  }
  success(position) {
    console.log(position.coords);
  }
  render() {
    return (
      <div className="App">
        { navigator.geolocation.getCurrentPosition(pos => console.log(pos.coords)) }
        Hi !!! 
      </div>
    );
  }
}

export default App;
