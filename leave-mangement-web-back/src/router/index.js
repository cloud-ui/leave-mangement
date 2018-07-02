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
      path: '/addCompany',
      name: 'addCompany',
      component:()=>import('../view/addCompany/addCompany')
    },
    {
      path: '/',
      name: 'shouye',
      component: () => import ('../packages/ui/container'),
      children: [
        {
          path: '/',
          name: 'shouye',
          meta: {requireAuth: true},
          component: () => import('../view/shouye/index')
        },
        {
          path: '/company',
          name: 'company',
          meta: {requireAuth: true},
          component: () => import('../view/dangan/company/index')
        },
        {
          path: '/deparment',
          name: 'deparment',
          meta: {requireAuth: true},
          component: () => import('../view/dangan/deparment/index')
        },
        {
          path: '/worker/:depId',
          name: 'worker/:depId',
          meta: {requireAuth: true},
          component: () => import('../view/dangan/worker/index')
        },
        {
          path:'/addApplication/:id',
          name:'addApplication/:id',
          meta: {requireAuth: true},
          component:()=>import('../view/approval/addApplication')
        },{
          path:'/unApplicationList',
          name:'unApplicationList',
          meta: {requireAuth: true},
          component:()=>import('../view/approval/unApplicationList')
        },{
          path:'/applicationList',
          name:'applicationList',
          meta: {requireAuth: true},
          component:()=>import('../view/approval/applicationList')
        },{
          path:'/userpage',
          name:'userpage',
          meta: {requireAuth: true},
          component:()=>import('../view/userpage/index')
        }
      ]
    }
  ]
})
