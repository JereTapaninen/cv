import Item from '../models/item.model';

/**
 * Load Item and append to req.
 */
function load(req, res, next, id) {
    Item.get(id)
      .then((item) => {
        req.item = item; // eslint-disable-line no-param-reassign
        return next();
      })
      .catch(e => next(e));
}

/**
 * Get item
 * @returns {Item}
 */
function get(req, res) {
    return res.json(req.item);
}

/**
 * Create new item
 * @property {string} req.body.name - The name of item.
 * @returns {Item}
 */
function create(req, res, next) {
    const item = new Item({
        name: req.body.name,
        icon: req.body.icon,
        selected: req.body.selected,
        owner: req.body.owner
    });

    item.save()
        .then(savedItem => res.json(savedItem))
        .catch(e => next(e));
}

function update(req, res, next) {
    const item = req.item;
    item.selected = req.body.selected;

    item.save()
        .then(savedItem => res.json(savedItem))
        .catch(e => next(e));
}

/**
 * Get list of items.
 * @returns {User[]}
 */
function list(req, res, next) {
    /*const { limit = 50, skip = 0 } = req.query;
    Item.list({ limit, skip })
        .then(items => res.json(items))
        .catch(e => next(e));*/
        //console.log(Item);
    //Item.list({ limit: 10000000, skip: 0}).then(items => res.json(items)).catch(e => next(e));
    Item.find({}, function(err, items) {
        let itemMap = [];

        items.forEach(function(item) {
            itemMap.push(item);
        });

        res.json({ "items": itemMap });
    });
}

/**
 * Delete item.
 * @returns {Item}
 */
function remove(req, res, next) {
    const item = req.item;
    item.remove()
        .then(deletedItem => res.json(deletedItem))
        .catch(e => next(e));
}

export default { load, get, create, update, list, remove };
