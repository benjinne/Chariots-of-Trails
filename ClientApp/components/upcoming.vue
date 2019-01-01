<template>
    <div>
        <map-carousel :maps="maps">
            <template slot-scope="slotProps">
                <div style="float:left">4 votes</div>
                <button v-on:click="voted(slotProps.map)" style="float:right">+1</button>
            </template>
        </map-carousel>
    </div>
</template>

<script>
import MapCarousel from './map-carousel.vue'

const mapPolyLine = 'w|xrF`kjsMq@gE{p@b\\zLj^{AdCxDfQaY|LxI`c@lE`qAvBtUbHxZgJrp@~Stj@vGbXsAb\\zIzoA|CdaBiH`BuRvNeLiA}WpOqJq[|G_T{Ewd@{Acc@cEgTd@sRwBsUnBgV_DpA~CqAG_BoLgm@e@ed@uE{RfS_YjEcNcGmLaAeHyD}{AjY}mAuYeuAr^iD|P`D~JxFfZp`@lg@~iA|A~BjCiCA_DsD{C~AeICuUcBeLaXyt@gK`FrBrLgBxBn@S'
    
export default {
    components: {MapCarousel},

    data () {
        return {
            maps: [ {name:'test1', route:mapPolyLine}, {name:'test2', route:mapPolyLine} ]
        }
    },

    methods: {

        voted: function(map) {
            console.log(map.name)
        }

    }
}
</script>