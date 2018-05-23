import React, { Component } from 'react';
import './ShoppingListListItem.css';
import MdDelete from 'react-icons/lib/md/delete';
import { observer } from 'mobx-react';

@observer
class ShoppingListsListItem extends Component {
    render() {
        const {item, deleteItem, onSelect} = this.props;
        const clsName = item.selected ? "shoppinglist-listitem active" : "shoppinglist-listitem";
        return (<div className={clsName} onClick={(e) => onSelect(item)}>
                    <header className="shoppinglist-listitem-header">
                        <img src={item.icon} />
                    </header>
                    <main className="shoppinglist-listitem-main">
                        <p>
                            {item.name}
                        </p>
                    </main>
                    <footer className="shoppinglist-listitem-footer">
                        <button className="shoppinglist-listitem-delete" onClick={() => {deleteItem(item)}}>
                            <MdDelete color="darkred" />
                        </button>
                    </footer>
                </div>);
    };
};

export default ShoppingListsListItem;