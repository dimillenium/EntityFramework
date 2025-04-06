<template>
  <div class="flex ecarte"><h2>{{ premiereEnMaj(props.categorie) }}</h2>
    <button class="bouton bord-noir">Magasiner tous</button>
  </div>

  <div class="flex mb-30 ">
    <div v-for="(produit, index) in produits" :key="index" class="padding-5 width-20">
      <ProduitApercu :produit="produit"/>
    </div>
  </div>


</template>

<script setup lang="ts">
import ProduitApercu from "@/components/produits/ProduitApercu.vue";
import {Produit} from "@/types";
import {useProduitService} from "@/inversify.config";
import {onMounted, ref} from "vue";
import {premiereEnMaj} from "@/outils";


// eslint-disable-next-line no-undef
const props = defineProps<{ categorie: string }>();

const produitService = useProduitService()

const produits = ref<Produit[]>([]);

onMounted(() => {
  chercherProduits(props.categorie);
  
});

async function chercherProduits( categorie: string){

  produits.value = (await produitService.obtenirParCategorie(categorie))
  console.log(produits)
  return produits;

}

</script>


