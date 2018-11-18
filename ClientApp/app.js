import Vue from 'vue'
import axios from 'axios'
import router from './router/index'
import store from './store'
import { sync } from 'vuex-router-sync'
import App from 'components/app-root'
import { FontAwesomeIcon } from './icons'
//import "leaflet/dist/leaflet.css";

// Registration of global components
Vue.component('icon', FontAwesomeIcon)

Vue.prototype.$http = axios

sync(store, router)

//Adds ability to use <script> tags in vue components
Vue.use(require('vue-script2'))

const app = new Vue({
  store,
  router,
  ...App
})

//check for login on each router action
router.beforeEach(async (to, from, next) => {
  if(to.path != '/login'){
    if(await sessionExistsTest()){
      next()
    }else{
      next('login')
    }
  }else{
    next()
  }
})

//test to see if logged in
async function sessionExistsTest(){
  let test = await app.$http.get(`/api/strava/sessionTest`)
  return test.data
}

//check for login on each refresh
sessionExistsTest()
  .then(check => {
    if(!check){
      router.push('login')
    }
  })
  .catch(err => {
    console.log(err)
  })

export {
  app,
  router,
  store
}
