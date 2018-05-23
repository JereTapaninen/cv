import { observable, action, computed } from 'mobx';
import ShoppingList from './ShoppingList/';
import axios from 'axios';

class ShoppingListStore {
    @computed get selectedShoppingList() {
        return this.shoppingLists.filter(shoppingList => shoppingList.selected)[0] || null;
    };

    @observable creatingShoppingList = false;
    @observable newShoppingListName = "";
    @observable shoppingLists = [ /*new ShoppingList("Testi1"), new ShoppingList("Testi2")*/ ];

    @action.bound selectShoppingList(shoppingList) {
        const shpList = this.selectedShoppingList;

        if (this.selectedShoppingList) 
            this.selectedShoppingList.deselect();

        if (shpList !== shoppingList)
            shoppingList.select();
    };

    @action.bound onNewShoppingListChange(e) {
        this.newShoppingListName = e.target.value;
    };

    @action.bound createShoppingList(e) {
        e.preventDefault();

        axios.post("http://localhost:4040/api/shoppinglist", { name: this.newShoppingListName }).then(({data}) => {
            this.shoppingLists.push(new ShoppingList(data._id, data.name));
        }).catch((err) => {
            console.log(err);
        });
        //this.shoppingLists.push(new ShoppingList(this.newShoppingListName));

        this.endCreatingShoppingList();
    };

    @action.bound startCreatingShoppingList() {
        this.creatingShoppingList = true;
    };

    @action.bound endCreatingShoppingList() {
        this.newShoppingListName = "";
        this.creatingShoppingList = false;
    };

    @action.bound deleteShoppingList(shoppingList) {
        axios.delete("http://localhost:4040/api/shoppinglist/" + shoppingList.id, {}).then(({data}) => {
            this.shoppingLists = this.shoppingLists.filter(sL => sL.id != data._id);
        }).catch(err => {
            console.error(err);
        });
    };

    @action.bound loadLists() {
        axios.get("http://localhost:4040/api/shoppinglist").then(({data}) => {
            this.shoppingLists.push(...data.shoppinglists.map(shoppinglist => new ShoppingList(shoppinglist._id, shoppinglist.name)));
        }).catch((err) => {
            console.log(err);
        });
    };

    constructor() {
        this.loadLists();
    }
}

export default new ShoppingListStore();