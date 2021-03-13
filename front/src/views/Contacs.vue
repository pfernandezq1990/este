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
      title: "table.title",      
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
    headers() {
      return [
        {          
          text: this.$t("table.name"),
          align: "start",
          value: "name",
        },
        {
          text: this.$t("table.type"),
          align: "start",
          value: "contactType",
        },
        {
          text: this.$t("table.phone"),
          align: "start",
          value: "phone",
        },
        {
          text: this.$t("table.birthdate"),
          align: "start",
          value: "birthdate",
        },
        {
          text: this.$t("table.actions"),
          sortable: false,
          value: "actions",
        },        
      ]
    }
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