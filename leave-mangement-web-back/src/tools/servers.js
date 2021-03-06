import axios from 'axios'
import qs from 'qs'
import { store } from '../store'
import router from '../router'

/**
 * 请求类
 */
export class BaseApi {
  servers;

  /**
   * 初始化servers
   */
  constructor() {
    const headers = new Headers();
    headers.set('App-Version', '0.1.0');
    this.servers = axios.create({
      baseURL: process.env.BASE_API,
      headers,
      withCredentials: true
    });
    this.servers.defaults.timeout = 50000;
    this.servers.interceptors.request.use(function (config) {
      return config
    }, function (error) {
      return Promise.reject(error || '网络繁忙，请稍候再试！');
    });
    this.servers.interceptors.response.use(function (response) {
      return response
    }, function (error) {
      let msg = '';
      if (error.response) {
        console.log(error.response.status)
        switch (error.response.status) {
          case 403:
            router.replace({
              path: '/forbidden',
              // query: {redirect: router.currentRoute.fullPath}
            })
            break;
          case 401:
            store.dispatch('accountLogoutSubmit').then(res => {
                this.$router.push('/login');
            }).catch(err => {})
            // router.replace({
            //   path: '/login'
            // })
          default:
            router.replace({
              path: '/wangluofanmang'
            })
        }
      } else {
        router.replace({
          path: '/wangluofanmang'
        })
      }
      return Promise.reject(error.response || '网络繁忙，请稍候再试！');
    })
  }

  /**
   * fetch
   * @param method
   * @param url
   * @param body
   * @param fileList
   * @param fileKey
   * @returns {Promise<any>}
   */
  connection(method = 'GET', url, body, fileList, fileKey = 'files') {
    this.getStatusToken();
    if (typeof body !== 'object') body = {};
    method = method.toLocaleLowerCase();
    if (fileList && (fileList instanceof Array)) {
      let headers = { 'Content-Type': 'multipart/form-data' };
      const param = new window.FormData();
      for (const key in body) {
        if (Object.prototype.hasOwnProperty.call(body, key)) param.append(key, body[key]);
      }
      fileList.forEach(file => param.append(fileKey, file));
      return Promise.resolve(this.servers[method](url, param, { headers }))
    }
    if (method === 'get') {
      url = `${url}?${qs.stringify(body)}`;
      body = {}
    }
    return Promise.resolve(this.servers[method](url, body))
  }

  /**
   * 设置token
   * @param isLogin
   * @param token
   */
  setToken({ isLogin, token }) {
    if (isLogin) {
      this.servers.defaults.headers.common['Authorization'] = token;
    }
  }

  /**
   * 获取登录状态，token值
   * @returns {{isLogin: string, token: string}}
   */
  getStatusToken() {
    const { isLogin, token } = store.getters;
    this.setToken({ isLogin, token })
  }
}

export const server = new BaseApi();
