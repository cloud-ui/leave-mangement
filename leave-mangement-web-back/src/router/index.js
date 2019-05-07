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
          name: '系统主页',
          meta: {requireAuth: true},
          component: () => import('../view/shouye/index')
        },
        {
          path: '/company',
          name: '公司管理',
          meta: {requireAuth: true},
          component: () => import('../view/dangan/company/index')
        },
        {
          path: '/deparment',
          name: '部门管理',
          meta: {requireAuth: true},
          component: () => import('../view/dangan/deparment/index')
        },
        {
          path: '/worker/:depId',
          name: '员工管理',
          meta: {requireAuth: true},
          component: () => import('../view/dangan/worker/index')
        },
        {
          path:'/leave/addApplication/:id',
          name:'编辑请假',
          meta: {requireAuth: true},
          component:()=>import('../view/approval/leave/addApplication')
        },{
          path:'/leave/unApplicationList',
          name:'未提交申请',
          meta: {requireAuth: true},
          component:()=>import('../view/approval/leave/unApplicationList')
        },{
          path:'/addapply/:type',
          name:'编辑职位申请',
          meta: {requireAuth: true},
          component:()=>import('../view/approval/addapply')
        },{
          path:'/leave/applicationList',
          name:'已提交请假',
          meta: {requireAuth: true},
          component:()=>import('../view/approval/leave/applicationList')
        },{
          path:'/userpage',
          name:'个人中心',
          meta: {requireAuth: true},
          component:()=>import('../view/userpage/index')
        },{
          path:'/check',
          name:'审核列表',
          meta: {requireAuth: true},
          component:()=>import('../view/check/index')
        },{
          path:'/notice',
          name:'公告管理',
          meta: {requireAuth: true},
          component:()=>import('../view/notice/index'),
        },{
          path:'/notice/add',
          name:'编辑公告',
          meta: {requireAuth: true},
          component:()=>import('../view/notice/addnotice')
        },{
          path:'/forbidden',
          name:'禁止访问',
          meta: {requireAuth: true},
          component:()=>import('@/packages/pages/forbidden')
        },{
          path:'/wangluofanmang',
          name:'网络繁忙',
          meta:{requireAuth:true},
          component:()=>import('@/packages/pages/wangluofanmang')
        }
      ]
    }
  ]
})
