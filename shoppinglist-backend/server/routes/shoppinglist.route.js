import express from 'express';
import validate from 'express-validation';
import paramValidation from '../../config/param-validation';
import shlCtrl from '../controllers/shoppinglist.controller';

const router = express.Router(); // eslint-disable-line new-cap

router.route('/')
  /** GET /api/users - Get list of users */
  .get(shlCtrl.list)

  /** POST /api/users - Create new user */
  .post(validate(paramValidation.createShoppinglist), shlCtrl.create);

router.route('/:id')
  /** GET /api/users/:userId - Get user */
  .get(shlCtrl.get)

  /** DELETE /api/users/:userId - Delete user */
  .delete(shlCtrl.remove);

/** Load user when API with userId route parameter is hit */
router.param('id', shlCtrl.load);

export default router;
