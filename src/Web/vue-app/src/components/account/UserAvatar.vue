<template>
  <div class="user-avatar">
    <div class="user-avatar__img-container">
      <IconFaceMan class="icon icon--black"/>
    </div>
    <p class="user-avatar__name">{{ personStore.person.fullName }}</p>
  </div>
</template>

<script lang="ts" setup>
import IconFaceMan from 'vue-material-design-icons/FaceMan.vue';
import {usePersonStore} from "@/stores/personStore";

const personStore = usePersonStore();

</script>

<style scoped>
.user-avatar__name{
  color: black;
}
.user-avatar__img-container{
  background-color: #1A1A1A;
}
</style>