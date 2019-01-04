<template>
    <div style="height: inherit;">
        <nav class="navbar navbar-expand-sm navbar-dark bg-dark">
            <button class="navbar-toggler" type="button" @click="toggleCollapsed">
                <span class="navbar-toggler-icon"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </button>
            <router-link class="navbar-brand" to="/"><icon :icon="['fas', 'fire']"/> Chariots of Trails</router-link>
            <transition name="slide">
                <div :class="'collapse navbar-collapse' + (!collapsed ? ' show':'')" v-show="!collapsed">
                    <ul class="navbar-nav mr-auto">
                        <li class="nav-item" v-for="(route, index) in routes" :key="index">
                            <router-link :to="route.path" exact-active-class="active">
                                <icon :icon="route.icon" class="mr-2" /><span>{{ route.display }}</span> 
                            </router-link>
                        </li>
                    </ul>
                </div>
            </transition>
        </nav>
    </div>
</template>

<script>
    export default {
      data () {
        return {
          routes: null,
          collapsed: true
        }
      },

      mounted(){
          //removes the catchall route from being displayed
          var _routes = require('../router/routes').routes
          _routes.pop();
          this.routes = _routes;
      },

      methods: {
          toggleCollapsed: function (event) {
            this.collapsed = !this.collapsed
            // used for mobile when clicking menu button, this shifts the main-view down
            var mainView = document.getElementById("main")
            if(this.collapsed){
                mainView.style.paddingTop = ""
            }else{
                mainView.style.paddingTop = "179px"
            }
        }
      }
    }
</script>

<style scoped>
    .slide-enter-active, .slide-leave-active {
    transition: max-height .35s
    }
    .slide-enter, .slide-leave-to {
    max-height: 0px;
    }
    .slide-enter-to, .slide-leave {
    max-height: 20em;
    }
</style>