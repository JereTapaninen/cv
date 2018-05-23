import express from 'express';
import validate from 'express-validation';
import paramValidation from '../../config/param-validation';
import itemCtrl from '../controllers/item.controller';

const router = express.Router(); // eslint-disable-line new-cap

router.route('/')
  /** GET /api/users - Get list of users */
  .get(itemCtrl.list)

  /** POST /api/users - Create new user */
  .post(validate(paramValidation.createShoppinglist), itemCtrl.create);

router.route('/:id')
  /** GET /api/users/:userId - Get user */
  .get(itemCtrl.get)

  .put(validate(paramValidation.updateItem), itemCtrl.update)

  /** DELETE /api/users/:userId - Delete user */
  .delete(itemCtrl.remove);

/** Load user when API with userId route parameter is hit */
router.param('id', itemCtrl.load);

export default router;
