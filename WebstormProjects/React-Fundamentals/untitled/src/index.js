import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import * as serviceWorker from './serviceWorker';

const Render = () => {
    ReactDOM.render(<App />, document.getElementById('root'));
};

const Increase = () => {
    counter++;
    Render();
};

let counter = 0;

const App = () => (
    <div>
        <div>{counter}</div>
        <button onClick={Increase}></button>
    </div>
);

Render();


// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
