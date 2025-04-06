<template>
  <section class="hero mb-30">
    <img src="@/assets/hero/lookbook.jpg" alt="Image de fond" height="300">
    <div>
      <h2>Boutique</h2>
      <p>Découvrez notre collection de bijoux artisanaux, façonnés à la main avec passion. Chaque pièce est unique,
        conçue avec des matériaux de qualité et un savoir-faire d'exception.</p>
    </div>
  </section>

  <main class="main">
    
    <div v-for="(categorie, index) in categories" :key="index" >
      <RangeeProduit :categorie="categorie"/>

    </div>
    
  </main>

</template>


<script lang="ts" setup>
import {onMounted, ref} from "vue";
import {useProduitService} from "@/inversify.config";
// import { useI18n } from "vue3-i18n";
import RangeeProduit from "@/components/produits/RangeeProduit.vue";





// const {t} = useI18n()

const produitService = useProduitService()


const categories = ref<string[]>([]);


onMounted(async () => {
  await chargerCategories();
  
});

async function chargerCategories() {
  categories.value = await produitService.obtenirCategories()
  console.log(categories)
}




</script>