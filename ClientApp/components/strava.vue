<template>
    <div>
        <h1>Profile page</h1>

        <div v-if="!user" class="text-center">
            <p><em>Loading...</em></p>
            <h1><icon icon="spinner" pulse/></h1>            
        </div>

        <template v-if="user">
            <h1>{{ user }}</h1>
            <img style="width: 200px; height: 200px;" v-bind:src="pic"/>
            <h1>Groups</h1>
            <h4>YCPXC</h4>
        </template>
    </div>
</template>

<script>

export default {

  data () {
    return {
        user: null,
        pic: null
    }
  },

  methods: {
    async loadPage () {
      try {

        let response = await this.$http.get(`/api/strava/users`)
        this.user = response.data.name
        this.pic = response.data.pic

      } catch (err) {
        window.alert(err)
        console.log(err)
      }
    }
  },

  async created () {
    this.loadPage()
  }
}
</script>

<style>
</style>
