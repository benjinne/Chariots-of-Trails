import Vue from 'vue'
import axios from 'axios'
import router from './router/index'
import store from './store'
import { sync } from 'vuex-router-sync'
import App from 'components/app-root'
import { FontAwesomeIcon } from './icons'

// Registration of global components
Vue.component('icon', FontAwesomeIcon)

//redirects if error was thrown see
//https://github.com/axios/axios/issues/932
axios.interceptors.response.use((response) => {
  return response
}, (error) => {
  if (error.response && error.response.data && error.response.data.location) {
    console.log(error.response.data.error)
    window.location = error.response.data.location
  } else {
    return Promise.reject(error)
  }
})

Vue.prototype.$http = axios

sync(store, router)

const app = new Vue({
  store,
  router,
  ...App
})


export {
  app,
  router,
  store
}
