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
                        <li class="nav-item" v-for="(route, index) in routes" :key="index" v-if="route.icon != null">
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
    import { routes } from '../router/routes'

    export default {
      data () {
        return {
          routes,
          collapsed: true
        }
      },
      methods: {
          toggleCollapsed: function (event) {
            this.collapsed = !this.collapsed
            // used for mobile when clicking menu button, this shifts the main-view down
            var mainView = document.body.childNodes[1].childNodes[2]
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