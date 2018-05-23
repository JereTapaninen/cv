import Shoppinglist from '../models/shoppinglist.model';

/**
 * Load shoppinglist and append to req.
 */
function load(req, res, next, id) {
    Shoppinglist.get(id)
      .then((shoppinglist) => {
        req.shoppinglist = shoppinglist; // eslint-disable-line no-param-reassign
        return next();
      })
      .catch(e => next(e));
}

/**
 * Get shoppinglist
 * @returns {Shoppinglist}
 */
function get(req, res) {
    return res.json(req.shoppinglist);
}

/**
 * Create new shoppinglist
 * @property {string} req.body.name - The name of shoppinglist.
 * @returns {Shoppinglist}
 */
function create(req, res, next) {
    const shoppinglist = new Shoppinglist({
        name: req.body.name
    });

    shoppinglist.save()
        .then(savedShoppinglist => res.json(savedShoppinglist))
        .catch(e => next(e));
}

/**
 * Get list of shoppinglists.
 * @returns {User[]}
 */
function list(req, res, next) {
    /*const { limit = 50, skip = 0 } = req.query;
    Shoppinglist.list({ limit, skip })
        .then(shoppinglists => res.json(shoppinglists))
        .catch(e => next(e));*/
        console.log(Shoppinglist);
    //Shoppinglist.list({ limit: 10000000, skip: 0}).then(shoppinglists => res.json(shoppinglists)).catch(e => next(e));
    Shoppinglist.find({}, function(err, shoppinglists) {
        let shoppinglistMap = [];

        shoppinglists.forEach(function(shoppinglist) {
            shoppinglistMap.push(shoppinglist);
        });

        res.json({ "shoppinglists": shoppinglistMap });
    });
}

/**
 * Delete shoppinglist.
 * @returns {Shoppinglist}
 */
function remove(req, res, next) {
    const shoppinglist = req.shoppinglist;
    shoppinglist.remove()
        .then(deletedShoppinglist => res.json(deletedShoppinglist))
        .catch(e => next(e));
}

export default { load, get, create, list, remove };
