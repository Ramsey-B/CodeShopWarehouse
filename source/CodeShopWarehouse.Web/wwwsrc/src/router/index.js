import Vue from 'vue'
import Router from 'vue-router'
import UnprocessedFillOrders from '@/components/UnprocessedFillOrders'
import GetFillOrderById from '@/components/GetFillOrderById'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Get Unprocessed FillOrders',
      component: UnprocessedFillOrders
    },
    {
      path: '/fillorder',
      name: 'Get FillOrder by Id',
      component: GetFillOrderById
    }
  ]
})
