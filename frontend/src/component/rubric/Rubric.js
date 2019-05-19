import React, { Component } from 'react';
import { List } from 'semantic-ui-react'

class Rubric extends Component {
  constructor(props) {
    super(props);
    this.state = {}
  }
  render() {
    return(
      <div className="rubric-main">
        <List>
          <List.Item onClick={ () => this.props.change('exhibitions') } >Выставки</List.Item>
          <List.Item onClick={ () => this.props.change('festivals') } >Фестивали</List.Item>
          <List.Item onClick={ () => this.props.change('games') } >Игры</List.Item>
          <List.Item onClick={ () => this.props.change('sports') } >Спорт</List.Item>
          <List.Item onClick={ () => this.props.change('music') } >Музыка</List.Item>
          <List.Item onClick={ () => this.props.change('other') } >Прочее</List.Item>
          <List.Item onClick={ () => this.props.change('all') } >Показать все</List.Item>
        </List>
      </div>
    );
  }
}

export default Rubric;