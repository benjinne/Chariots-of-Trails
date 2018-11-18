<template>
    <div>
        <h1>Welcome to Chariots of Trails!</h1>
        <form action="https://www.strava.com/routes/new/" target="_blank">
            <input type="submit" value="Create a Route" class="button" />
        </form>
        <l-map ref="map" style="height:500px; width:500px">
            <l-tile-layer :url="url" :attribution="attribution"></l-tile-layer>
        </l-map>
        <l-map ref="map" style="height:500px; width:500px">
            <l-tile-layer :url="url" :attribution="attribution"></l-tile-layer>
        </l-map>
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
        map: null,
        url:'http://{s}.tile.osm.org/{z}/{x}/{y}.png',
        attribution:'&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors',
        points: null
    }
},

mounted() {
    this.$nextTick(() => {
        var polyline = require( 'google-polyline' )
        this.map = this.$refs.map.mapObject
        this.points = polyline.decode( 'w|xrF`kjsMq@gE{p@b\\zLj^{AdCxDfQaY|LxI`c@lE`qAvBtUbHxZgJrp@~Stj@vGbXsAb\\zIzoA|CdaBiH`BuRvNeLiA}WpOqJq[|G_T{Ewd@{Acc@cEgTd@sRwBsUnBgV_DpA~CqAG_BoLgm@e@ed@uE{RfS_YjEcNcGmLaAeHyD}{AjY}mAuYeuAr^iD|P`D~JxFfZp`@lg@~iA|A~BjCiCA_DsD{C~AeICuUcBeLaXyt@gK`FrBrLgBxBn@S' )
        L.polyline(this.points, {
		color: 'blue',
		weight: 5,
		opacity: .7,
		lineJoin: 'round'
	    }).addTo(this.map);
        this.map.fitBounds(this.points);
    })
},
}
</script>

<style>
 @import "~leaflet/dist/leaflet.css";
</style>
