import Joi from 'joi';

export default {
  createShoppinglist: {
    body: {
      name: Joi.string().required()
    }
  },

  updateItem: {
    body: {
      selected: Joi.boolean().required()
    }
  }
};
