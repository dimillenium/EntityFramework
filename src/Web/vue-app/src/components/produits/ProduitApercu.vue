<template>
  <div class="relative w-64 bg-white overflow-hidden group mx-3">
    <!-- Image et bouton "Ajouter au panier" en hover -->
    <div class="relative w-full overflow-hidden z-0">
      <img
          :src="produit.photoUrl?.[0]"
          :alt="`Produit: ${produit.idProduit}`"
          class="w-full h-[350px] object-cover transition-transform duration-300 group-hover:scale-105 -z-10"
      />

      <!-- Effet Overlay en Hover -->
      <div
          class="absolute inset-0 bg-gradient-to-t from-black/50 to-transparent group-hover:opacity-100 transition-opacity duration-300 flex justify-center items-end pb-4 z-10"
      >
        <button
            class="bg-gray-900 text-white px-4 py-2 text-sm shadow-lg transform translate-y-5 group-hover:translate-y-0 transition-all duration-300 ease-in-out z-20"
        >
          Ajouter au panier
        </button>
      </div>
    </div>

    <!-- Contenu produit -->
    <div class="p-4">
      <h3 class="text-lg font-semibold text-gray-900">{{ produit.idProduit }}</h3>
      <div class="flex items-center justify-between mt-2">
        <p class="text-gray-700 font-medium text-lg">{{ produit.prix }} $</p>
        <button class="text-gray-500 hover:text-red-500 transition">
          <i class="pi pi-heart text-xl"></i>
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { Produit } from "@/types";
import { defineProps } from "vue";

defineProps<{ produit: Produit }>();
</script>