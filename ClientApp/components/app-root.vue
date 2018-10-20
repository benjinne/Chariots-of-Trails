<template>
    <div id="app" class="container-fluid">
        <div class="row">
            <div class="col-md-3">
                <nav-menu params="route: route"></nav-menu>
            </div>
            <div style="width: 100%; margin-left: 25%; height: 100%;">
                <router-view></router-view>
            </div>
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

<style>
.row { height: 100%;}
</style>
