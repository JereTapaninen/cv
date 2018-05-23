import express from 'express';
import shoppinglistRoutes from './shoppinglist.route';
import itemRoutes from './item.route';

const router = express.Router(); // eslint-disable-line new-cap

/** GET /health-check - Check service health */
router.get('/health-check', (req, res) =>
  res.send('OK')
);

// mount user routes at /users
//router.use('/users', userRoutes);

router.use('/shoppinglist', shoppinglistRoutes);
router.use('/item', itemRoutes);


export default router;
