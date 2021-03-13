<template>
  <div class="home">
    <ToolBar
      :title="title"
      :description="description"
      :btnText="btnText"
      :btnLink="btnLink"
    />

    <v-container class="my-5">
      <v-menu custom rounded offset-y color="grey">
        <template v-slot:activator="{ attrs, on }">
          <v-btn color="grey" class="white--text ma-5" v-bind="attrs" v-on="on">
            {{ $t(sort) }}
          </v-btn>
        </template>

        <v-list>
          <v-list-item v-for="item in itemList" :key="item" link>
            <v-list-item-title
              v-text=" $t( item) "
              @click="sortBy(item)"
            ></v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>

      <div>
        <v-card
          flat
          v-for="reservation in computedBookings"
          :key="reservation.id"
          align="start"
        >
          <v-layout
            row
            wrap
            :class="`pa-3 reservation ${!reservation.favorite}`"
          >
            <v-col xs="6" sm="3" md="3">
              <div>{{ reservation.title }}</div>
              <div class="caption grey--text">
                {{ reservation.date | datetime }}
              </div>
            </v-col>
            <v-col sm="3" md="3" class="d-none d-sm-flex">
              <div>
                {{ $t("home.textRanking") }} <br />
                <v-rating
                  color="amber"
                  :value="reservation.ranking"
                  dense
                  readonly
                  size="14"
                ></v-rating>
              </div>
            </v-col>
            <v-col xs="3" sm="3" md="3">
              <v-btn
                text
                v-if="!reservation.favorite"
                @click="addFavorite(reservation, reservation.id)"
              >
                {{ $t("home.btnFavorite") }} <v-icon> mdi-heart-settings </v-icon></v-btn
              >
              <v-btn text disabled v-if="reservation.favorite">
                {{ $t("home.btnFavorite") }} <v-icon> mdi-heart-settings </v-icon></v-btn
              >
            </v-col>
            <v-col xs="3" sm="3" md="3">
              <v-btn @click="editReservation(reservation)">{{ $t("home.btnEdit")}}</v-btn>
            </v-col>
          </v-layout>
        </v-card>
        <v-pagination
          class="my-2"
          v-model="pagination.current"
          :length="numOfPage"
          :total-visible="7"
          @input="onPageChange"
          circle
        >
        </v-pagination>
      </div>
      <v-dialog v-model="dialog" max-width="500px">
        <v-card>
          <v-card-title> Edit Reservation </v-card-title>
          <v-card-text>
            <v-container>
              <v-row align="center" justify="center">
                <validation-observer ref="observer" v-slot="{ invalid }">
                  <form @submit.prevent="Edit">
                    <!-- title -->
                    <validation-provider
                      v-slot="{ errors }"
                      name="TitleR"
                      rules="required|max:25"
                    >
                      <v-text-field
                        v-model="titleR"
                        :counter="25"
                        :error-message="errors"
                        label="Title"
                        required
                      >
                      </v-text-field>
                    </validation-provider>

                    <!-- Ranking -->

                    <validation-provider
                      v-slot="{ errors }"
                      name="Ranking"
                      rules="required"
                    >
                      <v-col cols="12" xs="12">
                        <v-select
                          :error-messages="errors"
                          required
                          :items="itemsRanking"
                          label="Ranking"
                          v-model="ranking"
                        ></v-select>
                      </v-col>
                    </validation-provider>

                    <!-- Favorite -->

                    <validation-provider
                      v-slot="{ errors }"
                      name="switch1"
                      rules="required"
                    >
                      <v-col cols="12" xs="12">
                        <v-switch
                          v-model="switch1"
                          label="Favorite"
                          :error-messages="errors"
                        ></v-switch>
                      </v-col>
                    </validation-provider>

                    <!-- Date -->
                    <v-col cols="12" xs="12" sm="4" md="4" lg="4">
                      <v-datetime-picker
                        label="Select Datetime"
                        v-model="datetime"
                        :datePickerProps="datepiker"
                      >
                      </v-datetime-picker>
                    </v-col>
                    <v-row>
                      <v-btn
                        color="red darken-2"
                        text
                        class="mr-4 my-5"
                        type="submit"
                        :disabled="invalid"
                      >
                        Save
                      </v-btn>

                      <v-btn
                        color="red darken-2"
                        class="my-5"
                        text
                        @click="close1"
                      >
                        Back
                      </v-btn>
                    </v-row>
                  </form>
                </validation-observer>
              </v-row>
            </v-container>
          </v-card-text>
        </v-card>
      </v-dialog>
    </v-container>
  </div>
</template>

<script>
import ToolBar from "../components/ToolBar";
import DatetimePicker from "vuetify-datetime-picker";
import axios from "axios";
import { required, digits, max } from "vee-validate/dist/rules";
import {
  extend,
  ValidationObserver,
  ValidationProvider,
  setInteractionMode,
} from "vee-validate";

setInteractionMode("eager");

extend("digits", {
  ...digits,
  message: "{_field_} needs to be {length} digits. ({_value_})",
});

extend("required", {
  ...required,
  message: "{_field_} can not be empty",
});

extend("max", {
  ...max,
  message: "{_field_} may not be greater than {length} characters",
});

export default {
  name: "Home",
  data() {
    return {
      pagination: {
        current: 1,
        total: 0,
        perPage: 5,
      },
      api: "https://localhost:5001/api/Bookings",
      itemList: [
        "sort.opcion1",
        "sort.opcion2",
        "sort.opcion3",
        "sort.opcion4",
        "sort.opcion5",
      ],
      sort: "sort.text",
      title: "toolBar.title",
      description:
        "toolBar.des",
      btnText: "toolBar.btn",
      btnLink: "/AddReservation",
      titleR: null,
      ranking: null,
      itemsRanking: [1, 2, 3, 4, 5],
      switch1: false,
      dialog: false,
      id: null,
      datetime: null,
      datepiker: {
        min: new Date().toISOString().substr(0, 10)
      },
      contactId: null,
    };
  },
  components: {
    DatetimePicker,
    ToolBar, 
    ValidationProvider,
    ValidationObserver,
  },

  mounted() {
    this.loadReservations();
  },

  computed: {
    bookings() {
      return this.$store.getters.Reservations;
    },

    offset() {
      return (this.pagination.current - 1) * this.pagination.perPage;
    },

    numOfPage() {
      return Math.ceil(this.bookings.length / this.pagination.perPage);
    },

    limit() {
      return this.offset + this.pagination.perPage;
    },

    computedBookings() {
      if (this.offset > this.bookings.length) {
        this.pagination.current = this.numOfPage;
      }
      return this.bookings.slice(this.offset, this.limit);
    },
  },

  methods: {
    loadReservations() {
      this.$store.dispatch("loadReservations", this.api);
    },

    sortBy(prop) {
      console.log(prop);
      this.sort = prop;

      if (prop == "sort.opcion5") {
        this.bookings.sort((a, b) => (a.ranking < b.ranking ? -1 : 1));
      }
      if (prop == "sort.opcion3") {
        this.bookings.sort((a, b) => (a.title > b.title ? -1 : 1));
      }
      if (prop == "sort.opcion4" ) {
        this.bookings.sort((a, b) => (a.title < b.title ? -1 : 1));
      }
      if (prop == "sort.opcion1") {
        this.bookings.sort((a, b) => (a.date > b.date ? -1 : 1));
      }
      if (prop == "sort.opcion2") {
        this.bookings.sort((a, b) => (a.title < b.title ? -1 : 1));
      }
    },

    onPageChange(page) {
      this.pagination.current = page;
    },

    addFavorite(item, id) {
      let data = {
        id: item.id,
        title: item.title,
        date: item.date,
        ranking: item.ranking,
        contactId: item.contactId,
        favorite: !item.favorite,
      };
      console.log(data);
      axios.put(`${this.api}/${id}`, data).then(() => {
        this.$store.dispatch("loadReservations", this.api);
      });
    },

    editReservation(item) {
      this.id = item.id;
      this.titleR = item.title;
      this.ranking = item.ranking;
      this.favorite = item.favorite;
      this.datetime = new Date(item.date);
      this.contactId = item.contactId;
      this.dialog = true;
    },

    close1() {
      this.dialog = false;
    },

    Edit() {
      this.$refs.observer.validate();
      let datos = {
        id: this.id,
        title: this.titleR,
        ranking: this.ranking,
        contactId: this.contactId,
        favorite: this.switch1,
        date: this.datetime,        
      };
      axios.put(`${this.api}/${this.id}`, datos).then(() => {
        this.dialog = false;
        this.$store.dispatch("loadReservations", this.api);
      })
    }
  },
};
</script>

<style>

.reservation.false {
  border-left: 5px solid red;
}
</style>
