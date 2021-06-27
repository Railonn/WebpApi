<template>
  <Navbar></Navbar>
  <body>
    <Login></Login>
  </body>
</template>

<script>
import Navbar from "./Navbar.vue";
import Login from "./Login.vue";

export default {
  name: "Home",
  components: {
    Navbar,
    Login,
  },
};
</script>

<style scoped>
body {
  background-image: linear-gradient(500deg, #c98bb9 0%, #eea16d 100%);
  display: flex;
  align-items: center;
  justify-content: center;
  min-height: 100vh;
  padding: 32px;
}
</style>
