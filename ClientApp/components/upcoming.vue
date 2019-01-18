<template>
    <div>
        <Loading :loading="mapsLoading" :size="50"/>
        <map-carousel :routes="routes">
            <template slot="top" slot-scope="slotProps">
                <h6 style="color: red">
                    {{slotProps.route.votedBy.length}} votes
                </h6>
                <h6>
                    Suggested By: {{slotProps.route.suggestedBy.fullname}}
                </h6>     
            </template>
            <template slot-scope="slotProps">
                <button v-on:click="vote(slotProps.route)" style="float:right">+1</button>
                <img class="avatar" v-for="(athlete, index) in slotProps.route.votedBy" :key="index" :src="athlete.profile" :title="athlete.fullname"/>
            </template>
        </map-carousel>
    </div>
</template>

<script>
import Loading from './loading.vue'
import MapCarousel from './map-carousel.vue'
    
export default {
    components: {MapCarousel, Loading},

    data () {
        return {
            routes: null,
            mapsLoading: true
        }
    },

    async mounted() {
        await this.updateSuggestedRoutes()
        this.mapsLoading = false
    },

    methods: {
        async vote(route) {
            await this.$http.post(`/api/main/vote?routeId=${route.id}`)
            await this.updateSuggestedRoutes();
        },
        async updateSuggestedRoutes() {
            let response = await this.$http.get(`/api/main/suggestedRoutes`)
            response.data.sort(this.sortRoutes)
            this.routes = response.data
        },
        sortRoutes(routeA, routeB){
            return routeB.votedBy.length - routeA.votedBy.length
        }
    }
}
</script>

<style scoped>
    .avatar{
        height: 24px;
        width: 24px;
        border-radius: 50%;
        color: transparent;
        border: 2px solid #fff;
    }
</style>