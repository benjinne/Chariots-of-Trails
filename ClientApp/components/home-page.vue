    <template>
        <div>

            <div v-if="!mapsLoaded" class="text-center">
                <p><em>Loading...</em></p>
                <h1><icon icon="spinner" pulse/></h1>            
            </div>

            <div v-if="mapsLoaded">
                <div v-for="map in maps" :key="map.id">
                    <h1>{{map.name}}</h1>
                    <l-map ref="map" style="height:500px; width:500px" @leaflet:load="insertPolyline(map.route)">
                        <l-tile-layer :url="url" :attribution="attribution"/>
                    </l-map>
                </div>
            </div>

        </div>
    </template>
    
    <script >
    import { LMap, LPolyline, LTileLayer} from 'vue2-leaflet';
    import { polyline } from 'leaflet';
    
    export default {
    components: {
        LMap,
        LPolyline,
        LTileLayer
      },
    data() {
        return {
            mapsLoaded: false,
            url:'http://{s}.tile.osm.org/{z}/{x}/{y}.png',
            attribution:'&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors',
            numberOfMaps: 0,
            mapIndex: 0,
            maps: null
        }
    },
    
    async mounted() {
        let response = await this.$http.get(`/api/strava/routes`)
        this.maps = response.data
        this.mapsLoaded = true
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
    }
    }
    </script>
    <style>
     @import "~leaflet/dist/leaflet.css";
    </style>