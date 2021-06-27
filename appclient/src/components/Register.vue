<template>
  <div class="mb-5" id="register">
    <Navbar></Navbar>
  </div>
  <body>
    <h1>Register Page {{ $route.meta.title }}</h1>
  </body>
</template>

<script>
import Navbar from "./Navbar.vue";
export default {
  name: "Register",
  components: {
    Navbar,
  },
  created() {
    document.title = "Register Page";
  },
};
</script>

<style scoped>
body {
  padding-top: 30px;
}
</style>
