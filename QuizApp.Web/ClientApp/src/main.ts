import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import { loadFonts } from './plugins/webfontloader'
import { createVuetify } from 'vuetify';
import { ApolloClient, InMemoryCache } from '@apollo/client/core'

loadFonts()

const cache = new InMemoryCache()
const apolloClient = new ApolloClient(
  {
    cache,
    uri: '<https://localhost:7199/graphql>'
  }
)

const vuetify = createVuetify()
const app = createApp(App)
app.use(router)
  .use(vuetify)

app.mount('#app')
