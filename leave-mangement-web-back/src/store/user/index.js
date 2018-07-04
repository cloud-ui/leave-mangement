import {Auth} from './auth';
export default{
    state:{
        token: Auth.getToken()|| {},
        userInfo: Auth.getUserInfo() || {},
        isLogin: Auth.getLogin() || false,
        menu:Auth.getMenu() || {},
        inform:Auth.getInform() || {}
    },
    mutations:{
        ACCOUNT_SET(state,data={}){
            Auth.setToken(data.token)
            Auth.setUserInfo(data.user);
            Auth.setMenu(data.menu);
            Auth.setLogin();
        },
        ACCOUNT_LOGOUT_FAILURE(state) {
            state.token = null;
            state.userInfo = {};
            state.isLogin = false;
            Auth.logout();
          },
        SET_INFORM(state,data={}){
            Auth.setInform(data)
        }
    },
    getters: {
        // doneTodos: state => {
        //   return state.todos.filter(value => value.done)
        // }
      },
    actions:{
        setUser({commit},data={}){
            return new Promise((resolve, reject) => {
                commit('ACCOUNT_SET',{...data});
                resolve()
                })
            
        },
        setInform({commit},data={}){
            return new Promise((resolve, reject) => {
                commit('SET_INFORM',{...data});
                resolve()
                })
        },
        /** 登出 */
        accountLogoutSubmit({commit}) {
            return new Promise((resolve, reject) => {
            commit('ACCOUNT_LOGOUT_FAILURE');
            resolve()
            })
        },
        }
}