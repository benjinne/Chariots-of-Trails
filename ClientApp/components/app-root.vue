<template>
    <div id="app" class="container-fluid">
        <div class="row">
            <div class="col-md-3">
                <nav-menu params="route: route"></nav-menu>
            </div>
            <div class="mainView">
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
@media (max-width: 767px) {
        .mainView {margin-left: 0%;}
}
@media (min-width: 768px) {
        .mainView {margin-left: 25%;}
}
.mainView {
    width: 100%; height: 100%;
}

</style>
