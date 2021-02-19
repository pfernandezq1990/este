<template>
  <div class="Contacs">
    <v-container class="my-5">
      <Table
        :headers="headers"
        :title="title"
        :items="Tolist"
        :api="api"
      />
    </v-container>
  </div>
</template>

<script>
import Table from "../components/Table";
export default {
  name: "Contacs",
  data() {
    return {
      api: "https://localhost:5001/api/contact",
      title: "Contacts List",
      headers: [
        {
          text: "Name",
          align: "start",
          value: "name",
        },
        {
          text: "Contact Type ",
          align: "start",
          value: "contactType",
        },
        {
          text: "Phone Number",
          align: "start",
          value: "phone",
        },
        {
          text: "Birth Date",
          align: "start",
          value: "birthdate",
        },
        {
          text: "Actions",
          sortable: false,
          value: "actions",
        },
      ],
      items: [],
    };
  },
  components: {
    Table,
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
  },
};
</script>

<style>
</style>