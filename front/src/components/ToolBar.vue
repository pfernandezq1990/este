<template>
  <v-container class="my-5" >
    <v-card flat align="center">
      <v-layout row wrap>
        <v-col cols="12" xs="12" sm="4" md="4">
          <h2 class="red--text">{{ title }}</h2>
        </v-col>
        <v-col cols="12" xs="12" md="4" lg="4" class="d-none d-sm-flex">
          <span class="text--subtitle-1">{{ description }}</span>
        </v-col>
        <v-col cols="12" xs="12" md="4" lg="4" >
          <v-btn rounded color="grey" class="white--text" :to="btnLink"
            >{{ btnText }}
          </v-btn>
        </v-col>
      </v-layout>
    </v-card>
  </v-container>
</template>

<script>
export default {
  props: {
    title: {
      type: String,
      required: true,
    },
    description: {
      type: String,
      required: true,
    },
    btnText: {
      type: String,
      required: true,
    },
    btnLink: {
      type: String,
      required: true,
    },
  },
};
</script>

<style>
</style>