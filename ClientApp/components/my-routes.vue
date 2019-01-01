<template>
    <div>
        <Loading :loading="mapsLoading" :size="50"/>
        <map-carousel :maps="maps">
            <template slot-scope="slotProps">
                <button v-on:click="suggested(slotProps.map)" style="margin: auto">Suggest</button>
            </template>
        </map-carousel>
    </div>
</template>

<script>
import Loading from './loading.vue'
import MapCarousel from './map-carousel.vue'
    
export default {
    components: {MapCarousel, Loading},

    data() {
        return {
            maps: null,
            mapsLoading: true
        }
    },
    
    async mounted() {
        let response = await this.$http.get(`/api/strava/routes`)
        this.maps = response.data
        this.mapsLoading = false
    },

    methods: {
        suggested: function(map) {
            console.log(map.name)
        }
    }
    
}
</script>

<style>
    @import "~leaflet/dist/leaflet.css";
</style>