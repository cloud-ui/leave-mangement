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
      meta: {requireAuth: true},
      component: () => import ('../packages/ui/container'),
      children: [
        {
          path: '/home',
          name: 'home',
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
          path:'/leave/addApplication/:id',
          name:'addApplication/:id',
          meta: {requireAuth: true},
          component:()=>import('../view/approval/leave/addApplication')
        },{
          path:'/leave/unApplicationList',
          name:'unApplicationList',
          meta: {requireAuth: true},
          component:()=>import('../view/approval/leave/unApplicationList')
        },{
          path:'/leave/applicationList',
          name:'applicationList',
          meta: {requireAuth: true},
          component:()=>import('../view/approval/leave/applicationList')
        },{
          path:'/userpage',
          name:'userpage',
          meta: {requireAuth: true},
          component:()=>import('../view/userpage/index')
        },{
          path:'/check',
          name:'check',
          meta: {requireAuth: true},
          component:()=>import('../view/check/index')
        },{
          path:'/notice',
          name:'notice',
          meta: {requireAuth: true},
          component:()=>import('../view/notice/index'),
        },{
          path:'/notice/add',
          name:'add',
          meta: {requireAuth: true},
          component:()=>import('../view/notice/addnotice')
        }
      ]
    }
  ]
})
