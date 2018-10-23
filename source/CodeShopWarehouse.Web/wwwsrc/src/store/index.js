import vue from 'vue'
import vuex from 'vuex'
import axios from 'axios'
import router from '../router'

vue.use(vuex)

var server = axios.create({
  baseURL: "https://localhost:44389/api",
  timeout: 3000,
  withCredentials: true
})

export default new vuex.Store({
  state:{
      fillOrders: [],
      fillOrder: {}
  },
  mutations: {
    setFillOrders(state, fillOrders) {
        state.fillOrders = fillOrders;
    },
    setFillOrder(state, fillOrder) {
        state.fillOrder = fillOrder;
    }
  },
  actions: {
    getUnprocessedFillOrders({commit}) {
        server.get("/fillorder")
            .then(response => {
                commit("setFillOrders", response.data);
            })
            .catch(error => {
                console.log(error);
            })
    },
    processFillOrder({dispatch, commit}, fillOrder) {
        server.put("/fillorder", fillOrder)
            .then(response => {
                dispatch("getUnprocessedFillOrders");
            })
            .catch(error => {
                console.log(error);
            })
    },
    getFillOrderById({commit}, fillOrderId) {
        server.get("/fillorder/" + fillOrderId)
            .then(response => {
                commit("setFillOrder", response.data);
            })
            .catch(error => {
                console.log(error);
            })
    },
    getFillOrdersByProductId({commit}, productId) {
        server.get("/fillorder/product/" + productId)
            .then(response => {
                commit("setFillOrders", response.data);
            })
            .catch(error => {
                console.log(error);
            })
    },
    createNewFillOrder({commit, dispatch}, newFillOrder) {
        server.post("/fillorder", newFillOrder)
            .then(response => {
                dispatch("getUnprocessedFillOrders");
            })
            .catch(error => {
                console.log(error);
            })
        
    }
  }
})