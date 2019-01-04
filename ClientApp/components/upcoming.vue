<template>
    <div>
        <Loading :loading="mapsLoading" :size="50"/>
        <map-carousel :routes="routes">
            <template slot-scope="slotProps">
                <div style="float:left">4 votes</div>
                <button v-on:click="voted(slotProps.route)" style="float:right">+1</button>
            </template>
        </map-carousel>
    </div>
</template>

<script>
import Loading from './loading.vue'
import MapCarousel from './map-carousel.vue'
    
export default {
    components: {MapCarousel, Loading},

    data () {
        return {
            routes: null,
            mapsLoading: true
        }
    },

    async mounted() {
        let response = await this.$http.get(`/api/main/suggestedRoutes`)
        this.routes = response.data
        this.mapsLoading = false
    },

    methods: {

        voted: function(route) {
            console.log(route)
        }

    }
}
</script>