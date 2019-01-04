import Vue from 'vue'
import VueRouter from 'vue-router'
import { routes } from './routes'
import axios from 'axios'

let router = new VueRouter({
  mode: 'history',
  routes
})

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

Vue.use(VueRouter)

//test to see if logged in
async function sessionExistsTest(){
  let test = await axios.get(`/api/main/sessionTest`)
  return test.data
}

export default router
