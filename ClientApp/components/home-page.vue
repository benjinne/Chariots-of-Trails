    <template>
        <div>
            <div v-for="n in numberOfMaps" :key="n.id">
                <l-map ref="map" style="height:500px; width:500px" @leaflet:load="insertPolyline">
                    <l-tile-layer :url="url" :attribution="attribution"/>
                </l-map>
            </div>
        </div>
    </template>
    
    <script >
    import { LMap, LPolyline, LTileLayer} from 'vue2-leaflet';
    
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
            mapIndex: 0
        }
    },
    
    mounted() {
        this.numberOfMaps = 5
    },
    
    methods: {
        insertPolyline: function() {
            var map = this.$refs.map[this.mapIndex++].mapObject
            var polyline = require( 'google-polyline' )
            var points = polyline.decode( 'w|xrF`kjsMq@gE{p@b\\zLj^{AdCxDfQaY|LxI`c@lE`qAvBtUbHxZgJrp@~Stj@vGbXsAb\\zIzoA|CdaBiH`BuRvNeLiA}WpOqJq[|G_T{Ewd@{Acc@cEgTd@sRwBsUnBgV_DpA~CqAG_BoLgm@e@ed@uE{RfS_YjEcNcGmLaAeHyD}{AjY}mAuYeuAr^iD|P`D~JxFfZp`@lg@~iA|A~BjCiCA_DsD{C~AeICuUcBeLaXyt@gK`FrBrLgBxBn@S' )
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