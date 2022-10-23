import { createApp, h, provide } from 'vue'
import App from './App.vue'
import router from './router'
import { loadFonts } from './plugins/webfontloader'
import { createVuetify } from 'vuetify';
import { ApolloClient, InMemoryCache } from '@apollo/client/core'
import { DefaultApolloClient } from '@vue/apollo-composable';

loadFonts()

const cache = new InMemoryCache()
const apolloClient = new ApolloClient(
  {
    cache,
    uri: '<https://localhost:7199/graphql>'
  }
)

// https://www.apollographql.com/blog/frontend/getting-started-with-vue-apollo/
const vuetify = createVuetify()
const app = createApp({
  setup(){
    provide(DefaultApolloClient, apolloClient)
  },
  render: () => h(App)
})

app.use(router)
  .use(vuetify)

app.mount('#app')
