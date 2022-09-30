import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import { loadFonts } from './plugins/webfontloader'
import { createVuetify } from 'vuetify';

loadFonts()

const vuetify = createVuetify()
const app = createApp(App)

app.use(router)
  .use(vuetify)

app.mount('#app')
