<template>
    <div>
        <Loading :loading="mapsLoading" :size="50"/>
        <map-carousel :routes="routes">
            <template slot-scope="slotProps">
                <div style="float:left">
                    <img class="avatar" v-for="athlete in slotProps.route.votedBy" :key="athlete.id" :src="athlete.profile"/>
                    {{slotProps.route.votedBy == null ? 0 : slotProps.route.votedBy.length}} votes
                </div>
                <button v-on:click="vote(slotProps.route)" style="float:right">+1</button>

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
        vote: async function(route) {
            await this.$http.post(`/api/main/vote?routeId=${route.id}`)
            await this.updateSuggestedRoutes();
        },
        updateSuggestedRoutes: async function() {
            let response = await this.$http.get(`/api/main/suggestedRoutes`)
            this.routes = response.data
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