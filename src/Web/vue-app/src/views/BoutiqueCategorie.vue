<template>
  <div class="container">
    <div style="padding: 50px">
      <div style=" text-align: center; padding: 50px ;  width: 100%">
        <h1 class="mb-30" style="font-size: 2rem"> {{ categorie }}</h1>
        <p>{{format}}</p>
      </div>
      <div>
        <form action="" class="flex start">
          
        </form>
      </div>
      <div class="container-produit">
        <div v-for="produit in produitsRecents" :key="produit.idProduit"  >
          <ProduitApercu :produit="produit" />
        </div>
      </div>
      
    </div>
  </div>
</template>

<script setup lang="ts">
import {ref, onMounted, watch} from 'vue';
import { useRoute } from 'vue-router';
import { useProduitService } from "@/inversify.config";
import { Produit } from "@/types/entities";
import ProduitApercu from "@/components/produits/ProduitApercu.vue";


const produitService = useProduitService();
const produitsRecents = ref<Produit[]>([]);
const enChargement = ref(false);

const categorie = ref<string>('');
const format = ref<string>('');



const route = useRoute();
const categorieParam = ref<string>(route.params.categorie as string);


async function obtenirProduitParCategorie() {
  enChargement.value = true;
  let produits = await produitService.obtenirTousLesProduits();

  if (Array.isArray(produits) && produits.length > 0) {
    const produitsFiltrés = produits.filter(produit => produit.categorie === categorieParam.value);

    if (produitsFiltrés.length > 0) {
      categorie.value = produitsFiltrés[0].categorie;
      format.value = produitsFiltrés[0].format;
      produitsRecents.value = produitsFiltrés;
    }
  }

  enChargement.value = false;
  
}

onMounted(async () => {
  await obtenirProduitParCategorie();
});
watch(() => route.params.categorie, async (nouvelleCategorie) => {
  categorieParam.value = Array.isArray(nouvelleCategorie) ? nouvelleCategorie[0] : nouvelleCategorie ?? "";
  await obtenirProduitParCategorie();
});


</script>

<style scoped lang="scss">
.container-produit {
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  gap: 16px;
  width: 100%;
}

@media (min-width: 768px) {
  .container-produit  {
    grid-template-columns: repeat(3, 1fr);
  }
}

@media (min-width: 1024px) {
  .container-produit  {
    grid-template-columns: repeat(6, 1fr);
  }
}
</style>
