<template>
  <Navbar></Navbar>
  <body>
    <div class="card">
      <h1 class="card__title">Welcome</h1>
      <h1>{{ userInfos.name }} {{ userInfos.lastName }}</h1>
      <button v-if="userInfos.userserviceid == 1" class="button">
        <span>Messaging</span>
      </button>
      <button v-if="userInfos.userserviceid == 2" class="button">
        <span>Documentation</span>
      </button>
    </div>
  </body>
</template>

<script>
import { mapState } from "vuex";
import Navbar from "./Navbar.vue";

export default {
  name: "Account",
  components: {
    Navbar,
  },
  data: function () {
    return {
      mode: "login",
      email: "",
      name: "",
      lastname: "",
      phonenumber: "",
      adress: "",
      city: "",
      country: "",
      userserviceid: "",
      password: "",
    };
  },
  mounted: function () {
    if (this.$store.state.user.userId == -1) {
      this.$router.push('/');
      return;
    }
    this.$store.dispatch('getUserInfos');
  },
  computed: {
    ...mapState(['userInfos']),
  },
};
</script>

<style scoped>
body {
  background-image: linear-gradient(150deg, #50bb6b 0%, #5f458f 100%);
  display: flex;
  align-items: center;
  justify-content: center;
  min-height: 100vh;
  padding: 32px;
}
.card {
  max-width: 100%;
  width: 1040px;
  background: white;
  border-radius: 16px;
  padding: 32px;
  margin-bottom: 300px;
}
.card__title {
  text-align: center;
  font-weight: 800;
}
.card__subtitle {
  text-align: center;
  color: #666;
  font-weight: 500;
}
</style>
