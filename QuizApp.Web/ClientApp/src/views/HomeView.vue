<template>
  <h1>Hello hellys</h1>
  <v-btn color="primary">
    click me
  </v-btn>
  <p v-if="error">Something went wrong</p>
  <p v-if="loading">Loading...</p>
  
</template>

<script lang="ts">
import { useQuery } from '@vue/apollo-composable';
import gql from 'graphql-tag';
import { defineComponent } from 'vue';

// Components
import HelloWorld from '../components/HelloWorld.vue';

const QUESTONS_QUERY = gql`
  query Questions {
    questions {
      id
      text
      responses {
        id
        text
        correctAnswer
      }
    }
  }
`

export default defineComponent({
  name: 'HomeView',
  components: {
    HelloWorld,
  },
  setup() {
    const { result, loading, error} = useQuery(QUESTONS_QUERY);
    console.log("Hello Elyes");
    
    return {
      result,
      loading,
      error
    }
  }
});
</script>
