<template>
  <v-card>
    <v-app-bar
      app
      clipped-left
      fixed
      dark
      prominent
      src="https://picsum.photos/1920/1080?random"
      fade-img-on-scroll
      scroll-threshold="500"
    >
      <template v-slot:img="{ props }">
        <v-img
          v-bind="props"
          gradient="to top right, rgba(55,236,186,.7), rgba(25,32,72,.7)"
        ></v-img>
      </template>
      
      <v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon>

      <v-container class="mx-md-16 px-md-16 my-auto" row>
        <span class="display-2 text-uppercase white--text mx-4">
          isuc<span class="display-1">orp</span>
        </span>
        <span class="d-none d-sm-flex mx-4 text-subtitle-1">
          {{ $t("appBar.Des1") }}<br> {{ $t("appBar.Des2") }}
        </span>
      </v-container>

      <v-spacer></v-spacer>
      <SelectLocale />
    </v-app-bar>

    <v-navigation-drawer v-model="drawer" app clipped dark>
      <v-list tile>
        <v-list-item
          v-for="item in items"
          :key="item.title"
          router
          :to="item.route"
          link
          @click="close"
        >
          <v-list-item-icon>
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-item-icon>

          <v-list-item-content>
            <v-list-item-title>{{ $t(item.title) }}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>
  </v-card>
</template>

<script>
import SelectLocale from './SelectLocale'
export default {
  data() {
    return {
      drawer: false,
      group: null,
      items: [
        {
          title: "links.contact",
          icon: "mdi-account-cog-outline",
          route: "/Contacs",
        },
        {
          title: "links.reservations",
          icon: "mdi-calendar-arrow-right",
          route: "/",
        },
      ],
    };
  },
  components: { SelectLocale },

  watch: {
    group() {
      this.drawer = false;
    },
  },

  methods: {
    close() {
      this.drawer = false;
    },
  },
};
</script>

<style>
.nav__end {
  display: flex;
  align-items: center;
}
</style>