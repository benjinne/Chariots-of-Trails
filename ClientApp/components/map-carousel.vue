<template>
    <vue-carousel ref="root" :touch-drag="false" :mouse-drag="false" :navigationEnabled="true" :perPageCustom="[[0,1], [830, 2], [1130, 3], [1430, 4], [1730, 5]]">
        <slide v-for="route in routes" :key="route.id">
            <div class="map-wrapper">
                <h2>{{route.name}}</h2>
                <slot name="top" v-bind:route="route"/>
                <l-map class="map" ref="map" @leaflet:load="insertPolyline(route.map.summary_polyline)">
                    <l-tile-layer :url="url" :attribution="attribution"/>
                </l-map>
                <div>{{route.description}}</div>
                <div style="font-size:small">
                    <div style="float:right;">
                        Elevation: {{route.elevationFt}} ft
                    </div>
                    <div style="text-align: left">
                        Distance: {{route.miles}} miles
                    </div>
                </div>
                <slot v-bind:route="route"/>
            </div>
        </slide>
    </vue-carousel>
</template>

<script>
import { Carousel as VueCarousel, Slide } from 'vue-carousel';
import { LMap, LPolyline, LTileLayer} from 'vue2-leaflet';

export default {
    
    components: {LMap, LPolyline, LTileLayer, VueCarousel, Slide},

    props: {
        routes: {
            required: true
        }
    },

    data() {
        return {
            url:'http://{s}.tile.osm.org/{z}/{x}/{y}.png',
            attribution:'&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors',
            mapIndex: 0
        }
    },

    methods: {
        insertPolyline: function(polyline) {
            var map = this.$refs.map[this.mapIndex++].mapObject
            var googlePolyline = require( 'google-polyline' )
            var points = googlePolyline.decode(polyline)
            L.polyline(points, {
            color: 'blue',
            weight: 5,
            opacity: .7,
            lineJoin: 'round'
            }).addTo(map);
            map.fitBounds(points);
        }
    },

    //special styling only required for carousels
    mounted() {
        document.getElementsByClassName("main-view")[0].className = 'main-view-list'
    },

    destroyed() {
        document.getElementsByClassName("main-view-list")[0].className = 'main-view'
    }
}
</script>

<style scoped>
    @import "~leaflet/dist/leaflet.css";

    .map {
        height: 250px;
        width: 250px;
    }
    .map-wrapper{
        width: fit-content;
        margin: auto;
        text-align:center;
        height: 100%;
        width: 250px;
    }
</style>