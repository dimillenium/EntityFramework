<template>
  <div class="layout">
    <header class="header flex justify-center items-center">
      <NavbarAccueil/>
    </header>

    <main style="">
      <router-view>
        
      </router-view>
    </main>

    <footer class="footer flex justify-center items-center">
      <Footerbase/>
    </footer>
  </div>
</template>

<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router';
import NavbarAccueil from '@/components/navigation/NavbarAccueil.vue';
import Footerbase from '@/components/footer/footerbase.vue';

</script>

<style scoped lang="scss">
.layout {
  display: flex;
  flex-direction: column;
  min-height: 100vh; /* Assure que le layout occupe toute la hauteur de l'écran */
}


.footer {
  background-color: #141414;
  color: #fff;
  padding: 10px;
  text-align: center;
}
</style>
