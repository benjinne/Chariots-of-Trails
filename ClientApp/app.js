import Vue from 'vue'
import axios from 'axios'
import router from './router/index'
import store from './store'
import { sync } from 'vuex-router-sync'
import App from 'components/app-root'
import { FontAwesomeIcon } from './icons'

// Registration of global components
Vue.component('icon', FontAwesomeIcon)

Vue.prototype.$http = axios

sync(store, router)

const app = new Vue({
  store,
  router,
  ...App//,
  //watch: {
    //'$route': function(from, to) {
      //called on page change
    //  sessionTest()
    //}
  //}
})


router.beforeEach((to, from, next) => {

    if(sessionExistsTest()){
      next()
    }else{

      
      next('login')
    }
  
  //router.push('login')
  // if(await sessionExistsTest())
  // {
  //   console.log('1')
  //   next()
  // }else{
  //   console.log('2')
  //   next('/login')
  // }
  

})


async function sessionExistsTest(){
  //test to see if logged in
  let response = await app.$http.get(`/api/strava/users`)
  if(response.data == 'session expired'){
    return false
    //router.push('login')
  }
  return true
}

router.push('login')
//called on page refresh
//sessionTest()

export {
  app,
  router,
  store
}
