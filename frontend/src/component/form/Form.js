import React, { Component } from 'react';
import { Button, Form } from 'semantic-ui-react';
import { DateInput, TimeInput } from 'semantic-ui-calendar-react';
import axios from 'axios';
import './Form.css';

const rubric = [
  {
    key: 'Выставки',
    text: 'Выставки',
    value: 'Выставки'
  },
  {
    key: 'Фестивали',
    text: 'Фестивали',
    value: 'Фестивали'
  },
  {
    key: 'Игры',
    text: 'Игры',
    value: 'Игры'
  },
  {
    key: 'Спорт',
    text: 'Спорт',
    value: 'Спорт'
  },
  {
    key: 'Музыка',
    text: 'Музыка',
    value: 'Музыка'
  },
  {
    key: 'Прочее',
    text: 'Прочее',
    value: 'НЕЧТО'
  }
]

class Forms extends Component { 
  constructor(props) {
    super(props);
    this.handleClick = this.handleClick.bind(this);
    this.handleClickForDate = this.handleClickForDate.bind(this);
    this.sendData = this.sendData.bind(this);
    this.getContent = this.getContent.bind(this);
    this.state = {
      blockBtn: true,
      rubric: '',
      title: '',
      comment: '',
      date: '',
      startTime: '',
      endTime: ''
    };
  }

  handleClick(key) {
    return e => {
      let obj = {}
      obj[key] = e.target.value
      this.setState(obj);
    }
  }

  handleClickForDate(event, {name, value}) {
      if (this.state.hasOwnProperty(name)) {
        this.setState({ [name]: value });
      }
  }

  sendData() {
    const date = this.state.date.split('-');
    const start = this.state.startTime;
    const end = this.state.endTime; 
    const sendObj = {
      latitude: this.props.coords.lat,
      longitude: this.props.coords.lng,
      rubric: this.state.rubric,
      title: this.state.title,
      comment: this.state.comment,
      startTime: `${date[2]}-${date[1]}-${date[0]}T${start}`,
      endTime: `${date[2]}-${date[1]}-${date[0]}T${end}`
    }
    axios.post('http://95.217.1.188:5000/api/Events', sendObj)
      .then(res => {
        if(res.status == 201) {
          this.props.getEvents();
          this.getContent(); 
        }
      })
      .catch(err => console.log(err));
  }

  getContent() {
    this.props.getContent()
  }

  render() {
    return(
      <div className="form-main">
        <Form>
          <Form.Field>
            <label>Выбирете рубрику</label>
            <Form.Select name="rubric" options={rubric} onChange={ this.handleClickForDate }/>
          </Form.Field>
          <Form.Field>
            <label>Укажите название</label>
            <Form.Input onChange={ this.handleClick('title') } value={ this.state.title }/>
          </Form.Field>
          <Form.Field>
            <label>Краткое описание</label>
            <Form.TextArea onChange={ this.handleClick('comment') } value={ this.state.comment }/>
          </Form.Field>
          <Form.Field>
            <label>Дата</label>
            <DateInput name="date" onChange={ this.handleClickForDate } value={ this.state.date }/>
          </Form.Field>
          <Form.Field>
            <label>Начло</label>
            <TimeInput name="startTime" onChange={ this.handleClickForDate } value={ this.state.startTime }/>
          </Form.Field>
          <Form.Field>
            <label>Конец</label>
            <TimeInput name="endTime" onChange={ this.handleClickForDate } value={ this.state.endTime }/>
          </Form.Field>
          <Button 
            onClick={ () => {
              this.sendData();       
            }
          }>
            Сохранить
          </Button>
        </Form>
        
      </div>
    );
  }
}

export default Forms;