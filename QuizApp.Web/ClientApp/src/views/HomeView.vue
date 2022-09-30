<template>
  <h1>Hello hellys</h1>
  <v-btn color="primary">
    click me
  </v-btn>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

// Components
import HelloWorld from '../components/HelloWorld.vue';

export default defineComponent({
  name: 'HomeView',

  components: {
    HelloWorld,
  },
});
</script>
