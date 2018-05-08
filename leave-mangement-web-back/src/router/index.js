import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/login',
      name: 'login',
      component: () =>
        import ('../view/login/login')
    },
    {
      path: '/',
      name: 'shouye',
      component: () => import ('../packages/ui/container'),
      children: [
        {
          path: '/',
          name: 'shouye',
          component: () => import('../view/shouye/index')
        },
        {
          path: '/company',
          name: 'company',
          component: () => import('../view/dangan/company')
        }
      ]
    }
  ]
})
