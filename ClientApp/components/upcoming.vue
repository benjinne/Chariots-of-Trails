<template>
    <div class="listWrap">
        <!-- <VirtualList class="list" :size="360" :remain="2.5" :bench="8">
            <div class="item" v-for="(day, index) of items" :index="index" :key="index"> -->
                <h2>{{ day }}</h2>
                <carousel :mouse-drag="false" :navigationEnabled="true" :perPageCustom="[[0,1], [700, 2], [1100, 3], [1500, 4]]">
                    <slide v-for="n in numberOfMaps" :key="n.id">
                        <l-map ref="map" style="height:250px; width:250px" @leaflet:load="insertPolyline">
                            <l-tile-layer :url="url" :attribution="attribution"/>
                        </l-map>
                    </slide>
                </carousel>
            <!-- </div>
        </VirtualList> -->
        <Loading class="list-loading" :loading="loading"></Loading>
    </div>
</template>

<script>
import Loading from './loading.vue'
import VirtualList from 'vue-virtual-scroll-list'
import moment from 'moment';
// import Carousel from './carousel.vue'
import { Carousel, Slide } from 'vue-carousel';
import { LMap, LPolyline, LTileLayer} from 'vue2-leaflet';

const getList = (length,times) => {
    var arr = new Array(length);
    for (var i = 0; i < length; i++) { 
        arr[i] = moment().add(times*length+i, 'days').format('dddd D MMMM');
    }
    return arr;
}

export default {

    components: {  VirtualList, Loading, Carousel, Slide, LMap, LPolyline, LTileLayer},

    data () {
        return {
            loading: false,
            times:0,
            items: getList(20, 0),
            mapsLoaded: false,
            url:'http://{s}.tile.osm.org/{z}/{x}/{y}.png',
            attribution:'&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors',
            numberOfMaps: 0,
            mapIndex: 0
        }
    },

    mounted() {
        this.numberOfMaps = 6
    },

    methods: {
        toBottom () {
            if (!this.loading) {
                this.loading = true
                // Mock for requesting delay.
                setTimeout(() => {
                    this.times++
                    this.loading = false
                    this.items = this.items.concat(getList(20,this.times))
                }, 2017)
            }
        },

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

    .listWrap {
        height: 100%;
    }

    .col-sm-9 {
        padding-right: 50px;
        padding-left: 50px;
    }
    /* div#app {
        overflow: hidden;
    } */

    /* .counter {
        position: relative;
        padding-bottom: 20px;
    }
    .count {
        position: absolute;
        right: 0;
    }
    .listWrap {
        position: relative;
        height: 100%;
    }
    .list-loading {
        position: absolute;
        bottom: 0;
        left: 50%;
        transform: translateX(-50%);
    }
    .list {
        background: #fff;
        border-radius: 3px;
        border: 1px solid #ddd;
        -webkit-overflow-scrolling: touch;
        -overflow-scrolling: touch;
        height: 100% !important;
    }
    .source {
        text-align: center;
        padding-top: 20px;
    }
    .source a {
        color: #999;
        text-decoration: none;
        font-weight: 100;
    }
    @media (max-width: 640px) {
        .times, .count {
            display: block;
        }
        .count {
            position: relative;
        }
    }
    .item {
        line-height: 50px;
        padding-left: 20px;
        border-bottom: 1px solid #eee;
        display: block;
    } */
    
</style>
