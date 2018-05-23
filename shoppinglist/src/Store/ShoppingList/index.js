import { observable, action, computed } from 'mobx';
import ShoppingListItem from '../ShoppingListItem/';
import axios from 'axios';

class ShoppingList {
    @computed get selectedItem() {
        return this.items.filter(itm => itm.selected)[0] || null;
    };

    @observable id = "";
    @observable name = "Hello";
    @observable items = [
        /*new ShoppingListItem("https://i.ytimg.com/vi/SfLV8hD7zX4/maxresdefault.jpg", "Koira 1"),
        new ShoppingListItem("https://i.ytimg.com/vi/SfLV8hD7zX4/maxresdefault.jpg", "Koira 2"),    
        new ShoppingListItem("https://i.ytimg.com/vi/SfLV8hD7zX4/maxresdefault.jpg", "Koira 3"),    
        new ShoppingListItem("https://i.ytimg.com/vi/SfLV8hD7zX4/maxresdefault.jpg", "Koira 4")*/        
    ];
    @observable selected = false;
    @observable creatingNewItem = false;
    @observable newItemName = "";
    @observable newItemIcon = "";

    @action.bound select() {
        this.selected = true;
    };

    @action.bound deselect() {
        this.selected = false;
    };

    @action.bound selectItem(itm) {
        const itm2 = this.selectedItem;

        if (itm.selected)
            itm.deselect();
        else
            itm.select();
    };

    @action.bound onNewItemIconChange(b64) {
        this.newItemIcon = b64;
    };

    @action.bound onNewItemNameChange(e) {
        this.newItemName = e.target.value;
    };

    @action.bound createItem(e) {
        e.preventDefault();

        axios.post("http://localhost:4040/api/item", { name: this.newItemName, icon: this.newItemIcon, selected: false, owner: this.id }).then(({data}) => {
            this.items.push(new ShoppingListItem(data._id, data.name, data.icon, false, data.owner));
        }).catch((err) => {
            console.log(err);
        });
        //this.items.push(new ShoppingListItem(this.newItemIcon, this.newItemName));

        this.endCreatingItem();
    };

    @action.bound startCreatingItem() {
        this.creatingNewItem = true;
    };

    @action.bound endCreatingItem() {
        this.creatingNewItem = false;
        this.newItemName = "";
        this.newItemIcon = "";
    };

    @action.bound deleteItem(item) {
        //this.items = this.items.filter(itm => itm.id != item.id);
        axios.delete("http://localhost:4040/api/item/" + item.id, {}).then(({data}) => {
            this.items = this.items.filter(itm => itm.id != data._id);
        }).catch(err => {
            console.error(err);
        });
    };

    @action.bound loadItems() {
        axios.get("http://localhost:4040/api/item").then(({data}) => {
            this.items.push(...data.items.filter(itm => itm.owner == this.id).map(itm => new ShoppingListItem(itm._id, itm.name, itm.icon, itm.selected, itm.owner)));
        }).catch((err) => {
            console.log(err);
        });
    }; 

    constructor(id, name, items) {
        this.id = id;
        this.name = name;

        this.loadItems();
        //this.items = [];
    }
}

export default ShoppingList;