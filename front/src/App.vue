<template>
  <v-app>
    <AppBar />

    <v-main>
      <v-container fluid>
        <router-view></router-view>
      </v-container>
    </v-main>
  </v-app>
</template>

<script>
import AppBar from "./components/AppBar";
export default {
  name: "App",

  data: () => ({
  }),

  components: {
    AppBar,
  },
};
</script>
