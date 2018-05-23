import React, { Component } from 'react';
import './ShoppingList.css';
import { inject, observer } from 'mobx-react';
import ShoppingListListItem from './ShoppingListListItem/';
import Dropzone from 'react-dropzone';
import MdPlaylistAdd from 'react-icons/lib/md/playlist-add';

@inject("store")
@observer
class ShoppingList extends Component {
    constructor() {
        super();
        this.state = { picture: "", canContinue: true };
    }

    render() {
        const {selectedShoppingList} = this.props.store;
        const {creatingNewItem, createItem, endCreatingItem, startCreatingItem, newItemName, onNewItemNameChange, onNewItemIconChange, deleteItem, selectItem} = selectedShoppingList ? selectedShoppingList : {};
        const me = this;
        function onDrop(acceptedFiles, rejectedFiles) {
            console.log("File dropped!", acceptedFiles, rejectedFiles);
            me.setState({canContinue: false});

            const fileToLoad = acceptedFiles[0];

            let fileReader = new FileReader();

            fileReader.onload = (fileLoadedEvent) => {
                const srcData = fileLoadedEvent.target.result;
                
                me.setState({picture: srcData});
                onNewItemIconChange(me.state.picture);
                me.setState({canContinue: true});
            }

            fileReader.readAsDataURL(fileToLoad);
        };
        return selectedShoppingList ?
            <div className="shoppinglist-list-wrapper">
                <header className="shoppinglist-list-header">
                    <h2>{selectedShoppingList.name}</h2>
                </header>
                <main className="shoppinglist-list-main">
                    <ul className="shoppinglist-list-list">
                    {selectedShoppingList.items.map((item, i) => (
                        <ShoppingListListItem key={i} item={item} deleteItem={deleteItem} onSelect={selectItem} />
                    ))}
                    {
                        creatingNewItem && 
                            <form onSubmit={createItem}>
                                <label for="itemname">Name:</label>
                                <input type="text" id="itemname" value={newItemName} onChange={onNewItemNameChange} />
                                <Dropzone onDrop={(accepted, rejected) => onDrop(accepted, rejected)}>
                                    Drop a picture of the item here
                                </Dropzone>
                                <button type="submit" disabled={!this.state.canContinue}>Add</button>
                                <button onClick={endCreatingItem}>Cancel</button>
                            </form>
                    }
                    </ul>
                </main>
                <footer className="shoppinglist-list-footer">
                    <button className="shoppinglist-list-add" onClick={startCreatingItem}>
                        <MdPlaylistAdd color="green" />
                        Add a new item
                    </button>
                </footer>
            </div> : null;
    };
}

export default ShoppingList;