import App from './App.vue'
import router from './router'
import { loadFonts } from './plugins/webfontloader'
import { DefaultApolloClient } from '@vue/apollo-composable';
import { createApp, provide, h } from 'vue';
import { InMemoryCache, ApolloClient } from '@apollo/client/core';

loadFonts()

const cache = new InMemoryCache()
const apolloClient = new ApolloClient(
  {
    cache,
    uri: 'https://localhost:7199/graphql'
  }
)

// https://www.apollographql.com/blog/frontend/getting-started-with-vue-apollo/
const app = createApp({
  setup(){
    provide(DefaultApolloClient, apolloClient)
  },
  render: () => h(App)
})

app.use(router)

app.mount('#app')
