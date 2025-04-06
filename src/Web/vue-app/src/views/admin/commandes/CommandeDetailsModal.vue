<template>
  <Dialog
      :visible="internalVisible"
      @update:visible="updateVisible"
      header="Détails de la commande"
      :modal="true"
      :style="{ width: '800px' }"
      class="p-fluid commande-details-modal"
  >
    <!-- SECTION 1: Détails généraux avec 3 cards côte à côte -->
    <div class="section-1 p-4" style="min-height: 200px;">
      <div class="cards-container flex flex-row justify-content-between gap-3">
        <!-- Card 1: Infos Client -->
        <div class="card flex-1 p-3">
          <h3 class="text-lg font-bold mb-2">Infos Client</h3>
          <p><strong>Nom complet :</strong> Jean Dupont</p>
          <p><strong>Email :</strong> jean.dupont@example.com</p>
          <p><strong>Téléphone :</strong> (555) 123-4567</p>
        </div>
        <!-- Card 2: Livraison & Paiement -->
        <div class="card flex-1 p-3">
          <h3 class="text-lg font-bold mb-2">Livraison & Paiement</h3>
          <p><strong>Livraison :</strong> {{ livraisonText(commande?.optionLivraison) }}</p>
          <p><strong>Paiement :</strong> {{ paiementText(commande?.moyenPaiement) }}</p>
        </div>
        <!-- Card 3: Adresse -->
        <div class="card flex-1 p-3">
          <h3 class="text-lg font-bold mb-2">Adresse</h3>
          <p>{{ commande?.adresseLivraison }}</p>
        </div>
      </div>
    </div>

    <!-- SECTION 2: Tableau des lignes de commande et résumé -->
    <div class="section-2 p-4" style="min-height: 400px;">
      <h3 class="text-lg font-bold mb-4">Articles de la commande</h3>
      <div class="overflow-x-auto">
        <table class="w-full table-auto text-left border-collapse">
          <thead>
          <tr class="bg-gray-100 text-gray-600">
            <th class="p-2 border-b">Modèle</th>
            <th class="p-2 border-b">Quantité</th>
            <th class="p-2 border-b">Type de matières</th>
            <th class="p-2 border-b">Prix Unitaire</th>
          </tr>
          </thead>
          <tbody>
          <tr
              v-for="(ligne, idx) in commande?.lignesCommande"
              :key="idx"
              class="hover:bg-gray-50"
          >
            <td class="p-2 border-b flex items-center gap-2">
              <img
                  src="https://primefaces.org/cdn/primevue/images/product/bamboo-watch.jpg"
                  alt="Produit"
                  style="width: 40px; height: 40px;"
                  class="object-cover rounded"
              />
              <span>Bamboo Watch - #2023</span>
            </td>
            <td class="p-2 border-b">{{ ligne.quantite }}</td>
            <td class="p-2 border-b">Bois & Métal</td>
            <td class="p-2 border-b">{{ ligne.prixUnitaire?.toFixed(2) }} $</td>
          </tr>
          </tbody>
        </table>
      </div>
      <!-- Résumé -->
      <div class="summary mt-4 flex flex-col items-end gap-1">
        <p><strong>Frais de livraison :</strong> 5.00 $</p>
        <p><strong>Rabais :</strong> 0.00 $</p>
        <p><strong>Sous-total :</strong> {{ commande ? commande.montantTotal.toFixed(2) : '0.00' }} $</p>
        <p class="text-lg mt-2">
          <strong>Total :</strong>
          {{ commande ? (commande.montantTotal + 5).toFixed(2) : '5.00' }} $
        </p>
      </div>
    </div>

    <!-- Footer -->
    <template #footer>
      <Button
          label="Fermer"
          icon="pi pi-times"
          class="p-button-outlined"
          @click="updateVisible(false)"
      />
    </template>
  </Dialog>
</template>

<script lang="ts">
import { defineComponent, ref, watch } from 'vue'
import Dialog from 'primevue/dialog'
import Button from 'primevue/button'

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
  lignesCommande?: Array<any>;
}

export default defineComponent({
  name: 'CommandeDetailsModal',
  components: {
    Dialog,
    Button
  },
  props: {
    visible: {
      type: Boolean,
      required: true
    },
    commande: {
      type: Object as () => Commande | null,
      default: null
    }
  },
  emits: ['update:visible'],
  setup(props, { emit }) {
    const internalVisible = ref<boolean>(props.visible)
    watch(() => props.visible, (newVal: boolean) => {
      internalVisible.value = newVal
    })

    function updateVisible(val: boolean) {
      internalVisible.value = val
      emit('update:visible', val)
    }

    function livraisonText(value?: string): string {
      switch (value) {
        case 'PosteCanada':
          return 'Poste Canada'
        case 'RetraitChezLaVendeuse':
          return 'Retrait chez la vendeuse'
        case 'LivraisonParLaVendeuse':
          return 'Livraison par la vendeuse'
        default:
          return '---'
      }
    }

    function paiementText(value?: string): string {
      switch (value) {
        case 'Paypal':
          return 'PayPal'
        case 'Interact':
          return 'Interac'
        default:
          return '---'
      }
    }

    return {
      internalVisible,
      updateVisible,
      livraisonText,
      paiementText
    }
  }
})
</script>

<style scoped>
.commande-details-modal .section-1 {
  border-bottom: 1px solid #ddd;
}
.commande-details-modal .section-2 {
  min-height: 400px;
}

/* Styles pour les cards */
.card {
  background-color: #f9f9f9;
  border: 1px solid #eee;
  border-radius: 8px;
  padding: 1rem;
}

.cards-container {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  gap: 1rem;
}
</style>
