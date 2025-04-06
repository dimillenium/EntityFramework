<template>
  <div class="admin-produits">
    <!-- Section 1: Barre d'actions -->
    <div class="header">
      <h2 class="title">Tous les produits</h2>
      <div class="actions">
        <BaseButton
            icon="pi pi-plus"
            label="Ajouter un produit"
            :onClick="ajouterProduit"
        />
        <!-- Modal d'importation -->
        <ImporterProduits v-model:visible="importModalVisible" @importSuccess="onImportSuccess"/>
      </div>
    </div>

    <!-- Section 2: Grille de produits -->
    <TransitionGroup
        name="slide-stagger"
        tag="div"
        class="produits-grid"
        appear
    >
      <ProduitCard
          v-for="(produit, index) in paginatedProduits"
          :key="produit.idProduit"
          :produit="produit"
          :style="{ transitionDelay: `${index * 100}ms` }"
      />
    </TransitionGroup>

    <!-- Pagination -->
    <Paginator
        v-if="produits.length > produitsParPage"
        :rows="produitsParPage"
        :totalRecords="produits.length"
        :page="page"
        @page="onPageChange"
    />
  </div>

  <!-- Import du modal -->
  <AjoutProduitModal
      v-model:visible="ajoutModalVisible"
      @produitAjoute="onProduitAjoute"
  />
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from "vue";
import Button from "primevue/button";
import Paginator from "primevue/paginator";
  import { useRouter } from "vue-router"; 
 import { useI18n } from "vue3-i18n";
import ProduitCard from "@/views/admin/produits/ProduitCard.vue";
import ImporterProduits from "@/views/admin/produits/ImporterProduits.vue";
import { useProduitService } from "@/inversify.config";
import BaseButton from "@/components/layouts/items/BaseButton.vue";
import AjoutProduitModal from "@/views/admin/produits/AjoutProduitModal.vue";

const onProduitAjoute = async () => {
  produits.value = await produitService.obtenirTousLesProduits();
  page.value = 0;
  ajoutModalVisible.value = false; // ferme le modal proprement
};
// Variables et état
const produitService = useProduitService();
const produits = ref<any[]>([]);
const page = ref(0);
const produitsParPage = 16;
 const router = useRouter();
 const { t } = useI18n();
const importModalVisible = ref(false); // État du modal d'importation

// Charger les produits au montage
onMounted(async () => {
  produits.value = await produitService.obtenirTousLesProduits()
  console.log(produits.value)
});

const ajoutModalVisible = ref(false);

const ajouterProduit = () => {
  ajoutModalVisible.value = true;
};

// Produits paginés
const paginatedProduits = computed(() => {
  const start = page.value * produitsParPage;
  return produits.value.slice(start, start + produitsParPage);
});

// Changement de page
const onPageChange = (event: any) => {
  page.value = event.page;
};

// // Actions
// const ajouterProduit = () => {
//   console.log(t("routes.admin.children.ajouter-produit.path"));
//    router.push(t("routes.admin.children.ajouter-produit.path"));
//   console.log("Ouvrir le formulaire d'ajout de produit");
// };

const ouvrirImportation = () => {
  importModalVisible.value = true;
};

// Rafraîchir les produits après un import réussi
const onImportSuccess = async () => {
  produits.value = await produitService.obtenirTousLesProduits()
};
</script>

<style scoped>
.admin-produits {
  padding: 2rem;
}

/* HEADER */
.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 1.5rem;
}

.title {
  font-size: 1.5rem;
  font-weight: bold;
}

.actions {
  display: flex;
  gap: 1rem;
}

.slide-stagger-enter-from {
  opacity: 0;
  transform: translateY(20px);
}

.slide-stagger-enter-active {
  transition: all 0.5s ease-out;
}

.slide-stagger-leave-to {
  opacity: 0;
  transform: translateY(-10px);
}

.slide-stagger-leave-active {
  transition: all 0.3s ease-in;
}

/* GRILLE DES PRODUITS */
.produits-grid {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 1.5rem;
  overflow-y: auto;
  max-height: 600px;
  padding-bottom: 1rem;
}

/* RESPONSIVE */
@media (max-width: 1024px) {
  .produits-grid {
    grid-template-columns: repeat(2, 1fr);
  }
}

@media (max-width: 768px) {
  .produits-grid {
    grid-template-columns: repeat(1, 1fr);
  }
}
</style>
