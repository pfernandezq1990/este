<template>
  <div class="AddReservation">
    <ToolBar
      :title="title"
      :description="description"
      :btnText="btnText"
      :btnLink="btnLink"
    />

    <v-container class="my-5">
      <v-card>
        <v-card-title>
          <h3 class="grey--text">Contact Information</h3>
        </v-card-title>
        <v-card-text>
          <validation-observer ref="observer" v-slot="{ invalid }">
            <form @submit.prevent="submit">
              <v-row align="center">
                <v-col cols="12" xs="12" sm="3" md="3" lg="3">
                  <v-autocomplete
                    required
                    :counter="15"
                    @change="autoComplete"
                    :items="Tolist"
                    item-text="name"
                    label="Name"
                    v-model="item"
                    return-object
                  >
                    <template slot="no-data">
                      <div class="pa-3">
                        <span class="my-3"
                          >No contact found.... <br />
                          <v-btn text color="red" @click="AddContact">
                            Add new</v-btn
                          >
                        </span>
                      </div>
                    </template>
                  </v-autocomplete>
                </v-col>

                <v-col cols="12" xs="12" sm="3" md="3" lg="3">
                  <v-text-field
                    required
                    label="Contact Type"
                    v-model="type"
                    readonly
                  ></v-text-field>
                </v-col>

                <v-col cols="12" xs="12" sm="3" md="3" lg="3">
                  <v-text-field
                    required
                    :counter="12"
                    label="Phone"
                    v-model="phone"
                    readonly
                  ></v-text-field>
                </v-col>

                <v-col cols="12" xs="12" sm="3" md="3" lg="3">
                  <v-text-field
                    readonly
                    v-model="dateContact"
                    label="Birthday date"
                  >
                  </v-text-field>
                </v-col>
              </v-row>
              <v-divider class="my-16" color="black"></v-divider>
              <v-card-title>
                <h3 class="grey--text">Reservation data</h3>
              </v-card-title>

              <!-- Reservation -->

              <v-row class="my-5" align="center">
                <validation-provider
                  v-slot="{ errors }"
                  name="titleR"
                  rules="required|max:25"
                >
                  <v-col cols="12" xs="12">
                    <v-text-field
                      v-model="titleR"
                      :counter="25"
                      :error-messages="errors"
                      label="Title"
                      required
                    ></v-text-field>
                  </v-col>
                </validation-provider>

                <v-col cols="12" xs="12" sm="4" md="4" lg="4">
                  <v-datetime-picker
                    label="Select Datetime"
                    v-model="datetime"
                    :datePickerProps="datepiker"
                  >
                  </v-datetime-picker>
                </v-col>

                <validation-provider
                  v-slot="{ errors }"
                  name="ranking"
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
              </v-row>
              <v-row>
                <v-btn
                  text
                  color="red darken-2"
                  class="mr-4 my-5"
                  type="submit"
                  :disabled="invalid"
                >
                  submit
                </v-btn>
              </v-row>
            </form>
          </validation-observer>
          <v-dialog v-model="dialogAdd" max-width="500px">
            <v-card>
              <v-card-title> Add Contact </v-card-title>
              <v-card-text>
                <v-container>
                  <v-row align="center" justify="center">
                    <validation-observer ref="observerAdd" v-slot="{ invalid }">
                      <form @submit.prevent="addContact">
                        <!-- Name -->
                        <validation-provider
                          v-slot="{ errors }"
                          name="Name"
                          rules="required|max:20"
                        >
                          <v-text-field
                            v-model="name"
                            :counter="20"
                            :error-message="errors"
                            label="Name"
                            required
                          >
                          </v-text-field>
                        </validation-provider>

                        <!-- Type -->
                        <validation-provider
                          v-slot="{ errors }"
                          name="type"
                          rules="required"
                        >
                          <v-select
                            :error-messages="errors"
                            required
                            :items="items"
                            item-text="name"
                            label="Contact Type"
                            v-model="type"
                          ></v-select>
                        </validation-provider>

                        <!-- Phone -->
                        <validation-provider
                          v-slot="{ errors }"
                          name="phone"
                          :rules="{
                            required: true,
                            digits: 10,
                          }"
                        >
                          <v-text-field
                            v-model="phone"
                            :counter="10"
                            :error-messages="errors"
                            label="Phone"
                            required
                          ></v-text-field>
                        </validation-provider>

                        <!-- BirthDate -->
                        <v-menu
                          ref="menu"
                          v-model="menu"
                          :close-on-content-click="false"
                          transition="scale-transition"
                          offset-y
                          min-width="auto"
                        >
                          <template v-slot:activator="{ on, attrs }">
                            <v-text-field
                              v-model="date"
                              label="Birthday date"
                              prepend-icon="mdi-calendar"
                              readonly
                              v-bind="attrs"
                              v-on="on"
                            ></v-text-field>
                          </template>
                          <v-date-picker
                            ref="picker"
                            v-model="date"
                            :max="new Date().toISOString().substr(0, 10)"
                            min="1950-01-01"
                            @change="save"
                          ></v-date-picker>
                        </v-menu>

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
        </v-card-text>
      </v-card>
    </v-container>
  </div>
</template>

<script>
import axios from "axios";
import ToolBar from "../components/ToolBar";
import DatetimePicker from "vuetify-datetime-picker";
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
  name: "AddReservation",
  data() {
    return {
      api: "https://localhost:5001/api/contact",
      apiR: "https://localhost:5001/api/Bookings",
      title: "Create Reservation",
      description:
        "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Similique illum recusandae itaque, ipsam sequi reiciendis reprehenderit ab perspiciatis",
      btnText: "List Reservation",
      btnLink: "/",

      contact: null,
      dateContact: null,
      date: null,
      datetime: null,
      item: null,
      items: [
        {
          name: "Contact Type 1",
        },
        {
          name: "Contact Type 2",
        },
        {
          name: "Contact Type 3",
        },
      ],
      type: null,
      phone: null,
      titleR: "",
      itemsRanking: [1, 2, 3, 4, 5],
      ranking: null,
      switch1: false,

      dialogAdd: false,
      menu: false,
      name: null,
      datepiker: {
        min: new Date().toISOString().substr(0, 10)
      },
    };
  },

  components: {
    ToolBar,
    ValidationProvider,
    ValidationObserver,
    DatetimePicker,
  },

  mounted() {
    this.loadContacts();
  },

  computed: {
    Tolist() {
      return this.$store.getters.Contacts;
    },
  },

  methods: {
    loadContacts() {
      this.$store.dispatch("loadContacts", this.api);
    },

    autoComplete() {
      if (this.item != null) {
        this.type = this.item.contactType;
        this.phone = this.item.phone;
        this.dateContact = new Date(this.item.birthdate).toISOString().substr(0,10);
      }
    },

    save(date) {
      this.$refs.menu.save(date);
    },

    submit() {
      this.$refs.observer.validate();
      let datos = {
        title: this.titleR,
        date: this.datetime,
        ranking: this.ranking,
        favorite: !this.switch1,
        contactId: this.item.id,
      };
      axios.post(this.apiR, datos).then(() => {
        this.$router.push({ name: "Home" });
      });
    },

    AddContact() {
      this.dialogAdd = true;
    },

    close1() {
      this.dialogAdd = false;
    },

    addContact() {
      this.$refs.observerAdd.validate();
      let datos = {
        name: this.name,
        contactType: this.type,
        phone: this.phone,
        birthdate: this.date,
      };
      axios.post(this.api, datos).then(() => {
        this.dialogAdd = false;
        this.$store.dispatch("loadContacts", this.api);
      });
    },
  },
};
</script>

<style>
</style>