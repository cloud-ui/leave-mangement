import Vue from 'vue'
import Vuex from 'vuex'
import {getters} from './getters.js'
import addCompany from './addCompany'
import user from './user'

Vue.use(Vuex);

export const store = new Vuex.Store({
  modules: {
      addCompany,
      user
  },
  getters
});