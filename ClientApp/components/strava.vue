<template>
    <div>
        <h1>Profile page</h1>

        <div v-if="!user" class="text-center">
            <p><em>Loading...</em></p>
            <h1><icon icon="spinner" pulse/></h1>            
        </div>

        <template v-if="user">
            <h1>{{ user }}</h1>
            <img style="width: 200px; height: 200px;" src="../../images/profile-pictures/michael-scott.png"/>
            <h1>Groups</h1>
            <h4>YCPXC</h4>
        </template>
    </div>
</template>

<script>

export default {

  data () {
    return {
        user: null
    }
  },

  methods: {
    async loadPage () {
      // ES2017 async/await syntax via babel-plugin-transform-async-to-generator
      // TypeScript can also transpile async/await down to ES5
      //this.currentPage = page

      try {

        let response = await this.$http.get(`/api/strava/users`)
        console.log(response.data)
        this.user = response.data

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
