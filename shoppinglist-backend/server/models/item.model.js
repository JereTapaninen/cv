import Promise from 'bluebird';
import mongoose from 'mongoose';
import httpStatus from 'http-status';
import APIError from '../helpers/APIError';

/**
 * Shoppinglist Schema
 */
const ItemSchema = new mongoose.Schema({
    name: {
        type: String,
        required: true
    },
    icon: {
        type: String,
        required: true
    },
    selected: {
        type: Boolean,
        required: true
    },
    owner: {
        type: String,
        required: true
    }
});

ItemSchema.method({
});

/**
 * Statics
 */
ItemSchema.statics = {
    /**
     * Get item
     * @param {ObjectId} id - The objectId of the item.
     * @returns {Promise<User, APIError>}
     */
    get(id) {
      return this.findById(id)
        .exec()
        .then((item) => {
          if (item) {
            return item;
          }
          const err = new APIError('No such item exists!', httpStatus.NOT_FOUND);
          return Promise.reject(err);
        });
    },
  };
  
  /**
   * @typedef Item
   */
  export default mongoose.model('Item', ItemSchema);
  