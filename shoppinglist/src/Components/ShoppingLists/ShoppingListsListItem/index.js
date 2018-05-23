import React, { Component } from 'react';
import { observer } from 'mobx-react';
import './ShoppingListsListItem.css';
import MdDelete from 'react-icons/lib/md/delete';

@observer
class ShoppingListsListItem extends Component {
    render() {
        const {shoppingList, onSelect, deleteShoppingList} = this.props;
        const clsName = shoppingList.selected ? "shoppinglists-listitem active" : "shoppinglists-listitem";
        return (
            <div className={clsName} onClick={() => onSelect(shoppingList)}>
                <p>
                    {shoppingList.name}
                </p>
                <button className="shoppinglists-listitem-delete" onClick={() => deleteShoppingList(shoppingList)}>
                    <MdDelete color="darkred" />
                </button>
            </div>
        );
    };
}

export default ShoppingListsListItem;