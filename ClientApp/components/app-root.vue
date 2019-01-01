<template>
    <div id="app" class="container-fluid">
        <div class="main-nav">
            <nav-menu params="route: route"></nav-menu>
        </div>
        <div class="main-view">
            <router-view></router-view>
        </div>
    </div>
</template>

<script>
    import NavMenu from './nav-menu'
    export default {
      components: {
        'nav-menu': NavMenu
      },
      data () {
        return {}
      }
    }
</script>