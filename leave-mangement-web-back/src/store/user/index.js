import {Auth} from './auth';
export default{
    state:{
        token: Auth.getToken()||null,
        userInfo: Auth.getUserInfo() || {},
        isLogin: Auth.getLogin() || false,
    },
    mutations:{
        setToken(state,data){
            Auth.setToken(data)
        },
        setUserInfo(state,data){
            Auth.setUserInfo(JSON.stringify(data));
            Auth.setLogin();
        },
    },
    getters: {
        // doneTodos: state => {
        //   return state.todos.filter(value => value.done)
        // }
      },
    actions:{
        setUser({commit},data={}){
            commit('setToken',data.token);
            commit('setUserInfo',data.user);
        }
    }
}