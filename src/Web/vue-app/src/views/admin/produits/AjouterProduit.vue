<template>
  <div class="px-6 py-4 max-w-4xl">
    <Form>
      <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
        <!-- Colonne gauche -->
        <div class="w-full space-y-4">
          <div>
            <label for="categorie" class="block font-semibold text-gray-700 mb-1">Catégorie</label>
            <Dropdown
                id="categorie"
                v-model="produit.categorie"
                :options="categories"
                optionLabel="label"
                optionValue="value"
                placeholder="Sélectionner une catégorie"
                class="w-full"
            />
          </div>

          <div>
            <label for="format" class="block font-semibold text-gray-700 mb-1">Format</label>
            <Dropdown
                id="format"
                v-model="produit.format"
                :options="formats"
                optionLabel="label"
                optionValue="value"
                placeholder="Sélectionner un format"
                class="w-full"
            />
          </div>

          <div>
            <label for="couleur" class="block font-semibold text-gray-700 mb-1">Couleur</label>
            <Dropdown
                id="couleur"
                v-model="produit.couleur"
                :options="couleurs"
                optionLabel="label"
                optionValue="value"
                placeholder="Sélectionner ou entrer une couleur"
                :editable="true"
                class="w-full"
            />
          </div>

          <div>
            <label for="description" class="block font-semibold text-gray-700 mb-1">Description</label>
            <Textarea id="description" v-model="produit.description" class="w-full" />
          </div>

          <div class="flex flex-row gap-4">
            <div class="w-full">
              <label for="prix" class="block font-semibold text-gray-700 mb-1">Prix ($CAD)</label>
              <input
                  id="prix"
                  type="number"
                  min="0"
                  step="0.01"
                  v-model.number="produit.prix"
                  placeholder="0.00 $"
                  class="w-full px-4 py-2 border-2 border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-gray-900 focus:border-transparent"
              />
            </div>

            <div class="w-full">
              <label for="quantite" class="block font-semibold text-gray-700 mb-1">Quantité</label>
              <input
                  id="quantite"
                  type="number"
                  min="0"
                  step="1"
                  v-model.number="produit.quantite"
                  placeholder="0"
                  class="w-full px-4 py-2 border-2 border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-gray-900 focus:border-transparent"
              />
            </div>
          </div>
        </div>

        <!-- Colonne droite -->
        <div class="flex flex-col justify-start items-start w-full">
          <FileUpload
              name="PhotoUrl"
              accept="image/*"
              :multiple="true"
              @select="uploadImages"
              class="w-full"
          />
        </div>
        <div v-if="produit.PhotoUrl.length" class="grid grid-cols-3 gap-4 mt-4 w-full">
          <img v-for="(img, i) in produit.PhotoUrl" :key="i" :src="img" class="w-full h-28 object-cover rounded" />
        </div>
      </div>

      <div class="text-center mt-6">
        <Button
            type="button"
            label="Ajouter"
            icon="pi pi-check"
            :disabled="uploading"
            class="w-full bg-gray-900 border-2 border-gray-900 text-white hover:bg-white hover:text-gray-900"
            @click="ajouterProduit"
        />
      </div>
    </Form>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from "vue";
import { useProduitService } from "@/inversify.config";
import Dropdown from "primevue/dropdown";
import Textarea from "primevue/textarea";
import InputNumber from "primevue/inputnumber";
import { Form } from "@primevue/forms";
import FileUpload from "primevue/fileupload";
import Button from "primevue/button";
import axios from "axios";
import { ICreateProduitRequest } from "@/types/requests";
import { useToast } from "primevue/usetoast";

const produitService = useProduitService();
const toast = useToast();
const emit = defineEmits(["produitAjoute", "close"]);
const API_KEY = "ed2b0188fc04f797225f7ae1f12c4eff";
const uploading = ref(false);

const produit = ref<ICreateProduitRequest>({
  idProduit: "",
  description: "",
  prix: 0,
  categorie: "",
  format: "",
  couleur: "",
  quantite: 0,
  PhotoUrl: [] as string[],
});

const dernierId = ref(1);
const couleurs = ref<{ label: string; value: string }[]>([]);
const formats = ref([
  { label: "Bouton", value: "B" },
  { label: "Médium", value: "M" },
  { label: "Large", value: "L" },
  { label: "Très grosse", value: "XL" },
]);
const categories = ref([
  { label: "Boucles", value: "boucles" },
  { label: "Barrettes de cheveux", value: "C" },
  { label: "Colliers", value: "P" },
]);

onMounted(async () => {
  try {
    const couleursApi = await produitService.obtenirCouleurs();
    if (Array.isArray(couleursApi)) {
      couleurs.value = couleursApi.map(c => ({ label: c, value: c }));
    }
  } catch (error) {
    console.error("Erreur lors du chargement des couleurs:", error);
  }
});

const couleurMap: Record<string, string> = {
  noir: "N",
  gris: "G",
  blanc: "W",
  beige: "Be",
  bleu: "Bl",
  brun: "Br",
  vert: "V",
  jaune: "J",
  orange: "O",
  rouge: "R",
  rose: "P",
  mauve: "M",
  lilas: "L",
  turquoise: "T",
  fuschias: "F",
  or: "Or",
  mix: "X",
};

const genererIdProduit = () => {
  let couleurNom = produit.value.couleur?.toLowerCase().trim() || "";
  let couleurCode = couleurMap[couleurNom] || "";

  if (!couleurCode) {
    couleurCode = couleurNom.length > 2 ? couleurNom.substring(0, 2).toUpperCase() : couleurNom.toUpperCase();
    couleurMap[couleurNom] = couleurCode;
  }

  const formatCode = produit.value.format || "";
  const numero = String(dernierId.value).padStart(4, "0");
  dernierId.value++;

  return `${formatCode}${couleurCode}-${numero}`;
};

const uploadImages = async (event: { files: File[] }) => {
  const files = event.files;
  const uploadedUrls: string[] = [];
 
  uploading.value = true;
  produit.value.PhotoUrl = []; // reset


  for (let file of files) {
    const formData = new FormData();
    formData.append("image", file);
    formData.append("key", API_KEY);

    try {
      const response = await axios.post("https://api.imgbb.com/1/upload", formData);
      uploadedUrls.push(response.data.data.url);
    } catch (error) {
      console.error("Erreur lors de l'upload :", error);
    }
  }

  produit.value.PhotoUrl = uploadedUrls;
  uploading.value = false;
  console.log("🔍 Produit à envoyer :", produit.value);
  produit.value.PhotoUrl = uploadedUrls;
  if (!produit.value.PhotoUrl.length) {
    console.warn("⚠️ Aucune photo n'est attachée au produit !");
  }
  console.log("📸 URLs des images envoyées :", produit.value.PhotoUrl);
};

const ajouterProduit = async () => {
  try {
    if (!produit.value.categorie || !produit.value.format || !produit.value.couleur ||
        (produit.value.prix ?? 0) <= 0 || (produit.value.quantite ?? 0) <= 0) {
      toast.add({ severity: "warn", summary: "Champs manquants", detail: "Veuillez remplir tous les champs.", life: 3000 });
      return;
    }

    produit.value.idProduit = genererIdProduit();
    
    const response = await produitService.createProduit(produit.value);

    if (response.succeeded) {
      toast.add({ severity: "success", summary: "Produit ajouté", detail: `ID : ${produit.value.idProduit}`, life: 3000 });
      emit("produitAjoute");
      emit("close"); 
      produit.value = {
        idProduit: "",
        description: "",
        prix: 0,
        categorie: "",
        format: "",
        couleur: "",
        quantite: 0,
        PhotoUrl: [] as string[],
      };
    } else {
      toast.add({ severity: "error", summary: "Erreur", detail: "Une erreur est survenue.", life: 3000 });
    }
  } catch (error) {
    console.error("Erreur :", error);
    toast.add({ severity: "error", summary: "Erreur", detail: "Une erreur est survenue.", life: 3000 });
  }
};
</script>