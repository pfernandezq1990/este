<template>
  <div class="locale-changer">
    <select class="white--text" v-model="$i18n.locale">
      <option class="grey--text" v-for="(lang, i) in langs" :key="`Lang${i}`" :value="lang">{{ lang }}</option>
    </select>
  </div>
</template>

<script>
export default {
  name: 'locale-changer',
  data () {
    return { langs: ['en', 'es'] }
  }
}
</script>