<template>
    <div>
               <div class="listWrap">
            <VirtualList class="list"
                :size="50"
                :remain="6"
                :tobottom="toBottom"
            >
                <div class="item" v-for="(day, index) of items" :index="index" :key="index">{{ day }}
                </div>
            </VirtualList>
            <Loading class="list-loading" :loading="loading"></Loading>
        </div>

    </div>
</template>

<script>
    import Loading from './loading.vue'
    import VirtualList from 'vue-virtual-scroll-list'
import moment from 'moment';

const getList = (length,times) => {
    var arr = new Array(length);
        for (var i = 0; i < length; i++) { 
    arr[i] = moment().add(times*length+i, 'days').format('dddd D MMMM');
}
        return arr;
    }

export default {
  components: {  VirtualList, Loading },

  data () {
    return {
                        loading: false,
                        times:0,
      items: getList(20, 0)
    }
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
            }
        }
}
</script>

<style>
    .counter {
        position: relative;
        padding-bottom: 20px;
    }
    .count {
        position: absolute;
        right: 0;
    }
    .listWrap {
        position: relative;
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
        overflow-scrolling: touch;
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
        height: 50px;
        line-height: 50px;
        padding-left: 20px;
        border-bottom: 1px solid #eee;
    }
</style>
