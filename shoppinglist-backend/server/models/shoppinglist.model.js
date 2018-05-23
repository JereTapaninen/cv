import Promise from 'bluebird';
import mongoose from 'mongoose';
import httpStatus from 'http-status';
import APIError from '../helpers/APIError';

/**
 * Shoppinglist Schema
 */
const ShoppinglistSchema = new mongoose.Schema({
    name: {
      type: String,
      required: true
    }
});

ShoppinglistSchema.method({
});

/**
 * Statics
 */
ShoppinglistSchema.statics = {
    /**
     * Get shoppinglist
     * @param {ObjectId} id - The objectId of shoppinglist.
     * @returns {Promise<User, APIError>}
     */
    get(id) {
      return this.findById(id)
        .exec()
        .then((shoppinglist) => {
          if (shoppinglist) {
            return shoppinglist;
          }
          const err = new APIError('No such shoppinglist exists!', httpStatus.NOT_FOUND);
          return Promise.reject(err);
        });
    },
  };
  
  /**
   * @typedef Shoppinglist
   */
  export default mongoose.model('Shoppinglist', ShoppinglistSchema);
  