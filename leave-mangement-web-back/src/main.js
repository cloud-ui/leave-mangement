// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import ElementUI from 'element-ui'
import router from './router'
import {store} from './store'
import './assets/styles/index.scss'
import './assets/styles/iconfont.css'
import 'element-ui/lib/theme-chalk/index.css'
import './permission'

Vue.config.productionTip = false
Vue.use(ElementUI, {
  size: 'medium',
});
/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  store,
  components: { App },
  template: '<App/>'
})
