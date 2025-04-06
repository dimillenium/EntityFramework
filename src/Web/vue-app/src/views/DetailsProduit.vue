

<template>
<main class="main">
  <div class="flex ecarte ecart-20">
    <img src="@/assets/produits/BBl-0001.png" alt="produits" class="f1" style="width: 100px"/>
    <div class="f2 padding-10">
      <div class="flex ecarte aligner-v">
        <div class="flex"><h3>Modèle: ###</h3><p>unique</p></div>
        <i class="fas fa-heart"></i>

      </div>
      <p>$7</p>
      <button class="bouton dore">Ajouter</button>
    </div>
  </div>
</main>
  
<!--  //composant recomendations-->
</template>

<script setup lang="ts">

import {useProduitService} from "@/inversify.config";

const produitService = useProduitService()

import {onMounted} from "vue";

onMounted(async () => {
  const produit = await produitService.obtenirProduit("BBl-0001");
  console.log(produit);


  
});
</script>