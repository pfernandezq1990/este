import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Contacs from '../views/Contacs.vue'
import AddReservation from '../views/AddReservation.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/Contacs',
    name: 'Contacs',
    component: Contacs
  },
  {
    path: '/AddReservation',
    name: 'AddReservation',
    component: AddReservation
  },
  
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
