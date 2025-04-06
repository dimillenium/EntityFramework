<template>
  <DataTable
      :value="data"
      tableStyle="min-width: 50rem"
      rowHover
      :sortField="sortField"
      :sortOrder="sortOrder"
      @row-click="onRowClick"
      @sort="onSort"
  >
    <!-- HEADER -->
    <template #header>
      <div class="flex flex-wrap items-center justify-between gap-2">
        <span class="text-xl font-bold">{{ title }}</span>
      </div>
    </template>

    <!-- Colonnes dynamiques -->
    <Column
        v-for="col in columns"
        :key="col.field"
        :field="col.field"
        :header="col.header"
        :sortable="col.sortable"
    >
      <template v-if="col.slot" #body="slotProps">
        {{ col.slot(slotProps.data[col.field]) }}
      </template>
    </Column>

    <!-- FOOTER -->
    <template #footer>
      {{ data?.length || 0 }} {{ footerText }}
    </template>
  </DataTable>
</template>

<script lang="ts" setup>
import { ref, defineProps, defineEmits } from 'vue'
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'

interface ColumnConfig {
  field: string;
  header: string;
  sortable?: boolean;
  slot?: (value: any) => string;
}

const props = defineProps<{
  data: any[];
  columns: ColumnConfig[];
  title: string;
  footerText: string;
}>()

const emit = defineEmits(["rowClick"])

// Gestion du tri
const sortField = ref<string>(props.columns[0]?.field || "")
const sortOrder = ref<number>(-1) // Descendant par défaut

function onRowClick(event: { data: any }) {
  emit("rowClick", event.data)
}

function onSort(event: { sortField: string; sortOrder: number }) {
  sortField.value = event.sortField
  sortOrder.value = event.sortOrder
}
</script>

<style scoped>
.card {
  padding: 1rem;
}
.p-datatable .p-sortable-column {
  cursor: pointer;
}
.p-datatable .p-sortable-column:hover {
  background-color: #f5f5f5;
}
</style>
