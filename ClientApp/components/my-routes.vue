<template>
    <div>
        <Loading :loading="mapsLoading" :size="50"/>
        <map-carousel :routes="routes">
            <template slot-scope="slotProps">
                <button v-on:click="suggested(slotProps.route)" style="margin: auto">Suggest</button>
            </template>
        </map-carousel>
        <div>
            <button v-on:click="update()" >update routes</button>
            <div>This will get the latested updates from strava. Warning! this will remove your suggested routes from upcomming and reset its votes</div>
        </div>
    </div>
</template>

<script>
import Loading from './loading.vue'
import MapCarousel from './map-carousel.vue'
    
export default {
    components: {MapCarousel, Loading},

    data() {
        return {
            routes: null,
            mapsLoading: true
        }
    },
    
    async mounted() {
        let response = await this.$http.get(`/api/main/userRoutes`)
        this.routes = response.data
        this.mapsLoading = false
    },

    methods: {
        suggested: async function(route) {
            this.$http.post(`/api/main/suggestRoute?routeId=${route.id}`)
        },
        update: async function() {
            let response = await this.$http.get(`/api/main/updateUserRoutes`)
            this.routes = response.data
        }
    }
    
}
</script>

<style>
    @import "~leaflet/dist/leaflet.css";
    .VueCarousel{
        border-bottom: 1px solid #444;
        padding-bottom: 15px;
        margin-bottom: 15px;
    }
</style>