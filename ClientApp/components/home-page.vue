<template>
    <div>
        <h1>Welcome to Chariots of Trails!</h1>
        <form action="https://www.strava.com/routes/new/" target="_blank">
            <input type="submit" value="Create a Route" class="button" />
        </form>

        <div v-if="!mapsLoaded" class="text-center">
            <p><em>Loading...</em></p>
            <h1><icon icon="spinner" pulse/></h1>            
        </div>

        <div v-if="mapsLoaded" class="text-center">

            <div v-for="(map, index) in maps" :key="map.id">

            <l-map ref="map" style="height:500px; width:500px">
                <l-tile-layer :url="url" :attribution="attribution"></l-tile-layer>
            </l-map>

            <div v-if="lastIteration(index)" />

            </div>

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
        maps: [],
        url:'http://{s}.tile.osm.org/{z}/{x}/{y}.png',
        attribution:'&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors',
    }
},

mounted() {

    this.maps.push('test1')
    this.maps.push('test2')
    this.maps.push('test3')
    this.mapsLoaded = true;

    // this.$nextTick(() => {

    //     var map = this.$refs.map.mapObject
    //     var polyline = require( 'google-polyline' )
    //     var points = polyline.decode( 'w|xrF`kjsMq@gE{p@b\\zLj^{AdCxDfQaY|LxI`c@lE`qAvBtUbHxZgJrp@~Stj@vGbXsAb\\zIzoA|CdaBiH`BuRvNeLiA}WpOqJq[|G_T{Ewd@{Acc@cEgTd@sRwBsUnBgV_DpA~CqAG_BoLgm@e@ed@uE{RfS_YjEcNcGmLaAeHyD}{AjY}mAuYeuAr^iD|P`D~JxFfZp`@lg@~iA|A~BjCiCA_DsD{C~AeICuUcBeLaXyt@gK`FrBrLgBxBn@S' )
    //     L.polyline(points, {
    //     color: 'blue',
    //     weight: 5,
    //     opacity: .7,
    //     lineJoin: 'round'
    //     }).addTo(map);
    //     map.fitBounds(points);
    // })


},

methods: {

    lastIteration(mapid) {
      console.log(mapid)
    }

}
}
</script>

<style>
 @import "~leaflet/dist/leaflet.css";
</style>
