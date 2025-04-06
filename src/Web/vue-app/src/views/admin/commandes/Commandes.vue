<template>
  <div class="card">
    <BaseDataTable
        :data="commandes"
        :columns="columns"
        title="Liste des Commandes"
        footerText="commandes"
        @rowClick="onRowClick"
    />

    <!-- Modal pour détails de commande -->
    <CommandeDetailsModal
        v-model:visible="dialogVisible"
        :commande="selectedCommande"
    />
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted } from 'vue'
import BaseDataTable from "@/components/tableau/BaseDataTable.vue";
import CommandeDetailsModal from "@/views/admin/commandes/CommandeDetailsModal.vue"
import { useCommandeService } from '@/inversify.config'

// Interface des commandes
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

// État des commandes
const commandes = ref<Commande[] | null>(null)
const loading = ref(false)
const dialogVisible = ref(false)
const selectedCommande = ref<Commande | null>(null)

const commandeService = useCommandeService()

onMounted(() => {
  reloadCommandes()
})

// Récupération des commandes
function reloadCommandes() {
  loading.value = true
  commandeService.LireTousCommandes().then((data) => {
    commandes.value = data ?? []
    loading.value = false
  })
}

// Gestion du clic sur une ligne
function onRowClick(event: { data: Commande }) {
  selectedCommande.value = event.data
  dialogVisible.value = true
}

// Configuration des colonnes du tableau
const columns = [
  { field: "numeroCommande", header: "Numéro de commande", sortable: true },
  { field: "dateCommande", header: "Date", sortable: true, slot: formatDate },
  { field: "emailClient", header: "Client", sortable: true },
  { field: "moyenPaiement", header: "Paiement", sortable: true, slot: paiementText },
  { field: "montantTotal", header: "Montant", sortable: true, slot: formatCurrency },
  { field: "optionLivraison", header: "Livraison", sortable: true, slot: livraisonText },
]

// Fonctions utilitaires
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
</script>

<style scoped>
.card {
  padding: 1rem;
}
</style>
