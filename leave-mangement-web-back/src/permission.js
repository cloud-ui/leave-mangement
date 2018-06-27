import router from './router'
import {store} from './store'

/** 路由控制 */
router.beforeEach((to, from, next) => {
  if(to.matched.some(record => record.meta.requireAuth)){
    /** 判断用户是否已经登录 */
    if (store.getters.isLogin) {
      /** 已经登录情况下访问 /login, 则直接进入 /admin */
      if (to.path === '/login') {
        next();
      } 
      else {
      next();
      }
    } else {
        next({path:'/login'});
    }
  }else{
    next();
  }
  
});

router.afterEach(() => {
});

// location.reload()
