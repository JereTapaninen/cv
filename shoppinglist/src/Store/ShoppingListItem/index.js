import { observable, action } from 'mobx';
import axios from 'axios';

class ShoppingListItem {
    @observable id = "";
    @observable icon = "";
    @observable owner = "";
    @observable name = "";
    @observable selected = false;

    constructor(id, name, icon, selected, owner) {
        this.id = id;
        this.icon = icon;
        this.name = name;
        this.selected = selected;
        this.owner = owner;
    }

    @action.bound select() {
        this.selected = true;

        axios.put("http://localhost:4040/api/item/" + this.id, { selected: this.selected }).then(({data}) => {
            console.log(data);
            //this.selected = data.selected;
        }).catch((err) => {
            console.error(err);
        });
    };

    @action.bound deselect() {
        this.selected = false;

        axios.put("http://localhost:4040/api/item/" + this.id, { selected: this.selected }).then(({data}) => {
            this.selected = data.selected;
        }).catch((err) => {
            console.error(err);
        });
    };
}

export default ShoppingListItem;