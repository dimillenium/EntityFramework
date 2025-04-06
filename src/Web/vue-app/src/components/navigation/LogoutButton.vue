<template>
  <button class="flex justify-between items-center w-full hover:bg-gray-100 p-3 rounded-lg" :class="classes" @click="logout">
    <p class="font-karla font-bold">
      {{ t('global.logout') }}
    </p>
    <i class="pi pi-sign-out"></i>
  </button>
</template>

<script lang="ts" setup>
import {useI18n} from "vue3-i18n";
import {useRouter} from "vue-router";
import {useUserStore} from "@/stores/userStore";
import {useMemberStore} from "@/stores/memberStore";
import {useAuthenticationService} from "@/inversify.config";
import {useAdministratorStore} from "@/stores/administratorStore";
import {usePersonStore} from "@/stores/personStore";
import Button from "primevue/button";

// eslint-disable-next-line
const props = defineProps<{
  classes?: string
}>();

const {t} = useI18n()
const router = useRouter()
const userStore = useUserStore();
const personStore = usePersonStore();
const memberStore = useMemberStore();
const administratorStore = useAdministratorStore();
const authenticationService = useAuthenticationService()

async function logout() {
  let succeededOrNotResponse = await authenticationService.logout()
  if (succeededOrNotResponse.succeeded) {
    userStore.reset()
    personStore.reset()
    memberStore.reset()
    administratorStore.reset()
    await router.push(t("routes.login.path"))
  }
}
</script>