<template>
  <div class="card">
    <DataTable
        :value="commandes"
        tableStyle="min-width: 50rem"
        rowHover
        @row-click="onRowClick"
    >
      <!-- HEADER -->
      <template #header>
        <div class="flex flex-wrap items-center justify-between gap-2">
          <span class="text-xl font-bold">Tableau de bord</span>
        </div>
      </template>

      <!-- Colonnes -->
      <Column field="numeroCommande" header="Numéro de commande" />
      <Column header="Date">
        <template #body="slotProps">
          {{ formatDate(slotProps.data.dateCommande) }}
        </template>
      </Column>
      <Column field="emailClient" header="Client" />
      <Column header="Paiement">
        <template #body="slotProps">
          {{ paiementText(slotProps.data.moyenPaiement) }}
        </template>
      </Column>
      <Column header="Montant">
        <template #body="slotProps">
          {{ formatCurrency(slotProps.data.montantTotal) }}
        </template>
      </Column>
      <Column header="Livraison">
        <template #body="slotProps">
          <Tag
              :value="livraisonText(slotProps.data.optionLivraison)"
              :severity="getLivraisonSeverity(slotProps.data.optionLivraison)"
          />
        </template>
      </Column>

      <!-- FOOTER -->
      <template #footer>
        Au total vous avez {{ commandes?.length || 0 }} commandes.
      </template>
    </DataTable>

    <!-- Modal pour détails de commande (composant séparé) -->
    <CommandeDetailsModal
        v-model:visible="dialogVisible"
        :commande="selectedCommande"
    />
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted } from 'vue'
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Button from 'primevue/button'
import Tag from 'primevue/tag'
import CommandeDetailsModal from "@/views/admin/commandes/CommandeDetailsModal.vue";
import { useCommandeService } from '@/inversify.config'

interface Commande {
  id?: string;
  dateCommande?: string | Date;
  numeroCommande?: string;
  moyenPaiement?: "Paypal" | "Interact";
  optionLivraison?: "PosteCanada" | "RetraitChezLaVendeuse" | "LivraisonParLaVendeuse";
  numeroSuivi?: string;
  emailClient?: string;
  adresseLivraison?: string;
  montantTotal?: number;
  lignesCommande?: any[];
}

const commandes = ref<Commande[] | null>(null)
const loading = ref(false)
const dialogVisible = ref(false)
const selectedCommande = ref<Commande | null>(null)

const commandeService = useCommandeService()

onMounted(() => {
  reloadCommandes()
})

function reloadCommandes() {
  loading.value = true
  commandeService.LireTousCommandes().then((data) => {
    commandes.value = data ?? []
    loading.value = false
  })
}

function onRowClick(event: { data: Commande }) {
  selectedCommande.value = event.data
  dialogVisible.value = true
}

function formatDate(value?: string | Date): string {
  if (!value) return ''
  const d = typeof value === 'string' ? new Date(value) : value
  return d.toLocaleDateString('fr-CA')
}

function formatCurrency(value?: number): string {
  if (!value) return ''
  return value.toLocaleString('fr-CA', { style: 'currency', currency: 'CAD' })
}

function paiementText(value?: string): string {
  switch (value) {
    case 'Paypal': return 'PayPal'
    case 'Interact': return 'Interac'
    default: return ''
  }
}

function livraisonText(value?: string): string {
  switch (value) {
    case 'PosteCanada': return 'Poste Canada'
    case 'RetraitChezLaVendeuse': return 'Retrait chez la vendeuse'
    case 'LivraisonParLaVendeuse': return 'Livraison par la vendeuse'
    default: return ''
  }
}

function getLivraisonSeverity(value?: string): string | undefined {
  switch (value) {
    case 'PosteCanada': return 'success'
    case 'RetraitChezLaVendeuse': return 'info'
    case 'LivraisonParLaVendeuse': return 'warning'
    default: return undefined
  }
}
</script>

<style scoped>
.card {
  padding: 1rem;
}

/* Lignes du DataTable: hover visible & curseur pointer */
.p-datatable .p-datatable-tbody > tr.p-row-hover {
  cursor: pointer;
  transition: background-color 0.2s;
}
.p-datatable .p-datatable-tbody > tr.p-row-hover:hover {
  background-color: #f0f0f0;
}
</style>
