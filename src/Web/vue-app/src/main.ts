import { createApp } from "vue";
import App from "./App.vue";
import PrimeVue from 'primevue/config';
import Aura from '@primeuix/themes/aura';
import Button from "primevue/button"
import { pinia } from "@/stores/pinia";
import { Router } from "./router";
import i18n from "@/i18n";
import { VueWindowSizePlugin } from 'vue-window-size/plugin';
import Notifications from "@kyvg/vue3-notification";
import Vue3EasyDataTable from "vue3-easy-data-table";
import "vue3-easy-data-table/dist/style.css";
import VueTippy from 'vue-tippy';
import "primeflex/primeflex.css";
import ToastService from "primevue/toastservice";
import 'primeicons/primeicons.css';
import "@fortawesome/fontawesome-free/css/all.min.css";

createApp(App)
  .use(i18n)
    .use(PrimeVue, {
        theme: {
            preset: Aura
        },
        locale: {
            decimalSeparator: ',',
            groupSeparator: ' ',
            currency: 'CAD',
            currencyDisplay: 'symbol',
            // Pour les boutons ou autres si nécessaire
            accept: 'Accepter',
            reject: 'Rejeter',
            // ...
        }
    })
    .use(ToastService)
.use(VueWindowSizePlugin)
  .use(PrimeVue, {
    theme: {
        preset: Aura
    }
  })
  .use(Router)
  .use(pinia) // pinia store should be loaded after router to access  (https://pinia.vuejs.org/core-concepts/outside-component-usage.html#single-page-applications)
  .use(Notifications)
  .component('EasyDataTable', Vue3EasyDataTable)
  .use(VueTippy, {
    defaultProps: {
        offset: [0, 12],
        zIndex: 30000,
        placement: "bottom",
        theme: "custom-em-no-joyaux-app",
        interactive: true
    },
  })
  .mount("#app");
