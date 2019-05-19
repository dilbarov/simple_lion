import React, { Component } from 'react';
import './Rubric.css'

class Rubric extends Component {
  constructor(props) {
    super(props);
    this.state = {}
  }
  render() {
    return(
      <div className="rubric-main">
        <div className="rubric-line" onClick={ () => 
        {this.props.change('Выставки') 
        this.props.changeVisible();}
        } >Выставки</div>
        <div className="rubric-line" onClick={ () =>
         {this.props.change('Фестивали') 
         this.props.changeVisible();}
         } >Фестивали</div>
        <div className="rubric-line" onClick={ () => 
        {this.props.change('Игры') 
        this.props.changeVisible();}
        } >Игры</div>
        <div className="rubric-line" onClick={ () => 
        {this.props.change('Спорт') 
        this.props.changeVisible();}
        } >Спорт</div>
        <div className="rubric-line" onClick={ () => 
        {this.props.change('Музыка') 
        this.props.changeVisible();}
        } >Музыка</div>
        <div className="rubric-line" onClick={ () => 
        {this.props.change('НЕЧТО') 
        this.props.changeVisible();}
        } >Прочее</div>
        <div className="rubric-line" onClick={ () => 
        {this.props.change('any') 
        this.props.changeVisible();}
        } >Показать все</div>
      </div>
    );
  }
}

export default Rubric;