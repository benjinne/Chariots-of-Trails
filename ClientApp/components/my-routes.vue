<template>
    <div>
        <Loading :loading="mapsLoading" :size="50"/>
        <MapCarousel :maps="maps"/>
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
    
}
</script>

<style>
    @import "~leaflet/dist/leaflet.css";
</style>