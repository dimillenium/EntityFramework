<template>
  <div class="flex flex-col justify-between bg-white shadow-md rounded-xl p-4 min-h-[220px] hover:shadow-lg">
    <!-- Première section (image + infos) -->
    <div class="flex justify-between gap-3">
      <img
          :src="imageUrl"
          alt="Image produit"
          class="w-20 h-20 object-cover rounded-lg"
      />
      <div class="flex-1 flex justify-between items-start gap-2">
        <div class="flex flex-col justify-start flex-1">
          <p class="font-bold text-base">{{ produit.idProduit }}</p>
          <p class="text-gray-600 text-sm">{{ produit.categorie }}</p>
        </div>
        <Button
            icon="pi pi-ellipsis-v"
            class="!bg-gray-100 !text-gray-700 !border-none !p-2 !rounded-lg !shadow-none hover:!bg-gray-200 min-w-[36px] min-h-[36px] flex items-center justify-center"
            severity="secondary"
            @click="() => {}"
        />
      </div>
    </div>

    <!-- Deuxième section (description) -->
    <div class="mt-3">
      <p class="font-bold mb-1">Description</p>
      <p class="text-gray-700 text-sm line-clamp-2">{{ produit.description }}</p>
    </div>

    <!-- Troisième section (prix encadré) -->
    <div class="mt-3 border-1 border-gray-300 rounded-full px-5 py-2 flex justify-between items-center w-full">
      <span class="font-bold">Prix</span>
      <span class="font-normal">{{ produit.prix }} $ CAD</span>
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed } from "vue";
import Button from "primevue/button";
const props = defineProps<{ produit: any }>();

const imageUrl = computed(() => {
  return props.produit?.photoUrl?.[0]
      ? props.produit.photoUrl[0]
      : "https://i.ibb.co/8Lhg48TH/BBl-0001.png";
});
</script>