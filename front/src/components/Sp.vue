<template>
  <v-card class="mx-auto" max-width="344">
    <v-toolbar color="grey">
        <v-toolbar-title>Bookings</v-toolbar-title>
    </v-toolbar>

    <v-list>
        <v-list-item v-for="item in items" :key="item" limk>
            <v-list-title>
                {{ item.title }}
            </v-list-title>
            <v-list-subtitle>
                {{ item.date }}
            </v-list-subtitle>
        </v-list-item>
    </v-list>
  </v-card>
</template>

<script>

export default {
    props: {
        items: {
            type: Object,
            required: [],
        },
    },
    data() {
        return {

        }
    },

};
</script>

<style>
</style>