import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    Contacts: [],
    Reservations: [],
    ToolBar: {},
  },
  getters: {
    Contacts(state) {
      return state.Contacts;
    },

    Reservations(state) {
      return state.Reservations;
    },

    ToolBar(state) {
      return state.ToolBar;
    }
  },
  mutations: {
    Contacts(state, data) {
      state.Contacts = data;
    },

    Reservations(state, data) {
      state.Reservations = data;
    },

    ToolBar(state, data) {
      state.ToolBar = data;
    }
  },
  actions: {

    loadContacts(context, api) {
      axios.get(api).then((response) => {
        const data = response.data;

        localStorage.setItem("Contacts", data);
        context.commit("Contacts", data);
      })
    },

    loadReservations(context, api) {
      axios.get(api).then((response) => {
        const data = response.data;

        localStorage.setItem("Reservations", data);
        context.commit("Reservations", data);
      })
    },

    toolBar(contex, data) {      
      localStorage.setItem("ToolBar", data);
      contex.commit("ToolBar", data);
    }
  },
  modules: {
  }
})
