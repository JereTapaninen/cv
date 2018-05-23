import React, { Component } from 'react';
import './ShoppingLists.css';
import { inject, observer } from 'mobx-react';
import ShoppingListListItem from './ShoppingListsListItem/';
import MdPlaylistAdd from 'react-icons/lib/md/playlist-add';

@inject("store")
@observer
class ShoppingLists extends Component {
    render() {
        const {shoppingLists, selectShoppingList, startCreatingShoppingList, creatingShoppingList, createShoppingList, endCreatingShoppingList,
                newShoppingListName, onNewShoppingListChange, deleteShoppingList} = this.props.store;
        return (
            <div className="shoppinglists-wrapper">
                <header className="shoppinglists-header">
                    <h2>Your shopping lists</h2>
                </header>
                <main className="shoppinglists-main">
                    <ul className="shoppinglists-list">
                        {shoppingLists.map((shoppingList, i) => (
                            <ShoppingListListItem key={i} shoppingList={shoppingList} deleteShoppingList={deleteShoppingList} onSelect={selectShoppingList} />
                        ))}
                        {
                            creatingShoppingList && <form onSubmit={createShoppingList}>
                                                        <input type="text" value={newShoppingListName} onChange={onNewShoppingListChange} autofocus="true" />
                                                        <button type="submit">Create</button>
                                                        <button onClick={endCreatingShoppingList}>Cancel</button>
                                                    </form>
                        }
                    </ul>
                </main>
                <footer className="shoppinglists-footer">
                    <button className="shoppinglists-button" onClick={startCreatingShoppingList}>
                        <MdPlaylistAdd color="green" />
                        Create shopping list
                    </button>
                </footer>
            </div>
        );
    };
}

export default ShoppingLists;