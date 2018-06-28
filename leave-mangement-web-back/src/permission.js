import router from './router'
import {store} from './store'
import axios from 'axios'

/** 路由控制 */
router.beforeEach((to, from, next) => {
  if(to.matched.some(record => record.meta.requireAuth)){
    /** 判断用户是否已经登录 */
    if (store.getters.isLogin) {
      
        next();
      
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
