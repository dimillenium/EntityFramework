<template>
  <Dialog v-model:visible="visible" modal :style="{ width: '900px' }" header="Ajouter un produit" :closable="true">
    <AjouterProduit @close="visible = false" @produitAjoute="$emit('produitAjoute')" />
  </Dialog>
</template>

<script setup lang="ts">
import {defineProps, defineEmits, ref, watch} from 'vue';
import Dialog from 'primevue/dialog';
import AjouterProduit from '@/views/admin/produits/AjouterProduit.vue';

const props = defineProps<{ visible: boolean }>();
const emit = defineEmits(['update:visible']);

const visible = ref(props.visible);

watch(() => props.visible, (val) => visible.value = val);
watch(visible, (val) => emit('update:visible', val));
</script>
