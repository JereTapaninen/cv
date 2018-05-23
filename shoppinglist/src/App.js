import React, { Component } from 'react';
//import logo from './logo.png';
import './App.css';
import ShoppingLists from './Components/ShoppingLists/';
import ShoppingList from './Components/ShoppingList/';
import { Provider } from 'mobx-react';
import ShoppingListStore from './Store/';

class App extends Component {
  render() {
    return (
      <Provider store={ShoppingListStore}>
        <div className="shoppinglist-body">
          <header className="shoppinglist-header">
      {/*<img src={logo} className="App-logo" alt="logo" />*/}
            <h1>Shopping List</h1>
          </header>
          <div className="shoppinglist-wrapper">
            <main className="shoppinglist-main">
              <ShoppingLists />
            </main>
            <aside className="shoppinglist-aside">
              <ShoppingList />
            </aside>
          </div>
          <footer className="shoppinglist-footer">
            <p>(C) Jere Tapaninen 2018</p>
          </footer>
        </div>
      </Provider>
    );
  }
}

export default App;
