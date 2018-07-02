import Cookies from 'js-cookie'

const sessionStorage = window.sessionStorage;

export class Auth {
  static setUserInfo(value = {}) {
    return sessionStorage.setItem('user.userInfo', JSON.stringify(value));
  }

  static getUserInfo() {
    return JSON.parse(sessionStorage.getItem('user.userInfo'))
  }

  static setMenu(value={}){
    return sessionStorage.setItem('user.menu', JSON.stringify(value));
  }

  static getMenu(){
    return JSON.parse(sessionStorage.getItem('user.menu'))
  }

  // 注销
  static logout() {
    Auth.removeUserInfo();
    Auth.removeLogin();
    Auth.removeToken();
    Auth.removeMenu();
  }

  static removeMenu(){
    return sessionStorage.removeItem('user.menu')
  }
  static removeUserInfo() {
    return sessionStorage.removeItem('user.userInfo')
  }

  static setLogin() {
    return Cookies.set('user.isLogin', true)
  }

  static getLogin() {
    return !!Cookies.get('user.isLogin')
  }

  static removeLogin() {
    return Cookies.remove('user.isLogin')
  }

  static setToken(value) {
    return Cookies.set('user.token', value)
  }

  static getToken() {
    return Cookies.get('user.token')
  }

  static removeToken() {
    return Cookies.remove('user.token')
  }
}
