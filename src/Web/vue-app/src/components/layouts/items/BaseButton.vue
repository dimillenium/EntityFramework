<template>
  <button
      :type="type"
      class="flex  font-bold items-center gap-2 bg-[#141414] text-white px-4 py-2 border-2 border-gray-900 text-sm hover:bg-transparent hover:text-gray-900 rounded-none"
      @click="onClick"
  >
    <i :class="icon"></i>
    <span v-if="label" class="font-karla">{{ label }}</span>
  </button>
</template>

<script setup lang="ts">
defineProps<{
  icon: string
  label?: string
  onClick?: () => void
  type?: 'button' | 'submit' | 'reset'
}>()
</script>