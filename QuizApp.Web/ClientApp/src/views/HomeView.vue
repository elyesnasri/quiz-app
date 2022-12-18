<template>
  <h1>Quizzzzzz</h1>
  <v-btn color="primary">
    click me
  </v-btn>
  <h1>{{number}}</h1>
  <p v-if="error">Something went wrong</p>
  <p v-if="loading">Loading...</p>
  
</template>

<script setup lang="ts">
import { useQuery } from '@vue/apollo-composable';
import gql from 'graphql-tag';

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

let number = 10;
const { result, loading, error} = useQuery(QUESTONS_QUERY);
</script>
