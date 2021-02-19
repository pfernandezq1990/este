import Vue from 'vue'
import './plugins/axios'
import App from './App.vue'
import './registerServiceWorker'
import router from './router'
import store from './store'
import vuetify from './plugins/vuetify';
import date from './filters/date';
import datetime from './filters/datetime'
import DatetimePicker from 'vuetify-datetime-picker'


Vue.use(DatetimePicker)

Vue.config.productionTip = false

Vue.filter('date', date)

Vue.filter('datetime', datetime)

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app')
