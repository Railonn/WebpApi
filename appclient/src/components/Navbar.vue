<template>
  <nav class="navbar navbar-expand-lg fixed-top navbar-dark bg-primary">
    <div class="container-fluid">
      <a href="/" class="navbar-brand">Home</a>
      <ul class="navbar-nav ml-auto">
        <button class="button" v-if="userInfos.userId" @click="logout()">
          <span>Disconnect</span>
        </button>
      </ul>
    </div>
  </nav>
</template>

<script>
import { mapState } from "vuex";
export default {
  name: "Navbar",
  computed: {
    ...mapState(['userInfos']),
  },
  methods: {
    logout: function() {
      this.$store.commit('logout');
      this.$router.push('/');
    }
  }
};
</script>

<style scoped>
.button {
  background: #f54748;
}
.button:hover {
  cursor: pointer;
  background: #ff5b5c;
}
</style>
