<template>
  <h1>Quiz</h1>
  <p v-if="error">Something went wrong</p>
  <p v-if="loading">Loading...</p>
  <div>
    <div>
      <div v-for="item in result.questions">
        <h3>{{ item.text }}</h3>
        <ul>
          <li v-for="response in item.responses">
            {{ response.text }}
          </li>
        </ul>
      </div>
    </div>
  </div>
  
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

const { result, loading, error} = useQuery(QUESTONS_QUERY);
</script>
