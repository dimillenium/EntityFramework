<template>
  <div>
    <BaseButton
        label="Importer des produits"
        icon="pi pi-upload"
        @click="dialogVisible = true"
    />

    <Dialog v-model:visible="dialogVisible" header="Importer des bijoux (XLSX)" :modal="true" :style="{ width: '1200px' }">
      <div class="flex flex-col items-center gap-6">
        <FileUpload
            mode="basic"
            accept=".xlsx"
            chooseLabel="Sélectionner un fichier Excel (.xlsx)"
            :auto="false"
            class="bg-gray-900 border-2 border-gray-900 rounded-none"
            @select="onFileSelected"
        />

        <Message v-if="errorMessage" severity="error">{{ errorMessage }}</Message>

        <ProgressSpinner v-if="loading" />

        <DataTable
            v-if="excelData.length && !loading"
            :value="excelData"
            class="w-full"
            scrollable
            scrollHeight="600px"
        >
          <Column field="idProduit" header="Numéro de modèle" />
          <Column field="quantite" header="Quantité" />
          <Column field="prix" header="Prix" />
          <Column field="description" header="Description" />
        </DataTable>

        <BaseButton
            v-if="excelData.length > 0 && !loading"
            label="Importer"
            icon="pi pi-check"
            class="rounded-none border border-black bg-black text-white hover:bg-transparent hover:text-black px-6 py-2"
            @click="uploadFile"
        />
      </div>
    </Dialog>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { useToast } from "primevue/usetoast";
import Dialog from "primevue/dialog";
import FileUpload from "primevue/fileupload";
import DataTable from "primevue/datatable";
import Column from "primevue/column";
import ProgressSpinner from "primevue/progressspinner";
import Message from "primevue/message";
import axios from "axios";
import BaseButton from "@/components/layouts/items/BaseButton.vue";
import { useProduitService } from "@/inversify.config";
import ExcelJS from "exceljs";

const API_KEY = "ed2b0188fc04f797225f7ae1f12c4eff";

const toast = useToast();
const dialogVisible = ref(false);
const file = ref<File | null>(null);
const excelData = ref<any[]>([]);
const loading = ref(false);
const errorMessage = ref<string | null>(null);
const produitService = useProduitService();

async function onFileSelected(event: any) {
  const selectedFile = event.files[0];
  if (!selectedFile) return;
  console.log("📄 Fichier sélectionné :", selectedFile.name);

  file.value = selectedFile;
  errorMessage.value = null;

  try {
    const parsed = await parseXlsxFile(selectedFile);
    console.log("✅ Données extraites :", parsed);
    excelData.value = parsed;
  } catch (err) {
    console.error("❌ Erreur parsing Excel:", err);
    errorMessage.value = "Erreur de lecture du fichier Excel.";
  }
}

async function parseXlsxFile(file: File) {
  const workbook = new ExcelJS.Workbook();
  const buffer = await file.arrayBuffer();
  await workbook.xlsx.load(buffer);

  const worksheet = workbook.worksheets[0];
  console.log("📊 Lecture de la feuille :", worksheet.name);

  const products: any[] = [];
  const imagesMap = new Map<number, Uint8Array>();

  const media = (workbook as any).model.media;
  worksheet.getImages().forEach((image: any, index: number) => {
    const { range, imageId } = image;
    const excelImage = media?.find((m: any) => m.id === imageId);

    const row = range.tl.nativeRow + 1;
    const col = range.tl.nativeCol + 1;

    console.log(`🖼️ Image détectée à ligne ${row}, colonne ${col} → ID ${imageId}`);

    if (col === 5 && excelImage?.buffer) {
      imagesMap.set(row, new Uint8Array(excelImage.buffer));
    }
  });

  worksheet.eachRow((row, rowNumber) => {
    if (rowNumber === 1) return;

    const idProduit = row.getCell(1).text.trim();
    const quantite = parseInt(row.getCell(2).value?.toString() || "1");
    const prix = parseFloat(row.getCell(3).value?.toString() || "0");
    const description = row.getCell(4).text.trim();
    const imageBuffer = imagesMap.get(rowNumber);

    console.log(`🧾 Ligne ${rowNumber} → ID: ${idProduit}, Qté: ${quantite}, Prix: ${prix}, Desc: ${description}, Image?: ${!!imageBuffer}`);

    products.push({ idProduit, quantite, prix, description, imageBuffer });
  });

  console.log("✅ Données extraites :", products);
  return products;
}

async function uploadFile() {
  loading.value = true;
  errorMessage.value = null;

  try {
    for (const produit of excelData.value) {
      let photoUrl = "";

      if (produit.imageBuffer) {
        try {
          console.log("⬆️ Upload image pour :", produit.idProduit);
          photoUrl = await uploadImageToImgBB(produit.imageBuffer);
          console.log("✅ Image uploadée :", photoUrl);
        } catch (e) {
          console.warn("❌ Échec de l'upload pour", produit.idProduit);
        }
      }

      const produitToCreate = {
        idProduit: produit.idProduit,
        description: produit.description,
        prix: produit.prix,
        quantite: produit.quantite,
        categorie: "Bijoux",
        format: "Standard",
        couleur: "Inconnue",
        PhotoUrl: photoUrl ? [photoUrl] : []
      };

      console.log("📦 Produit à créer :", produitToCreate);

      const response = await produitService.createProduit(produitToCreate);
      if (!response.succeeded) {
        console.warn(`⚠️ Produit non ajouté : ${produit.idProduit}`);
      }
    }

    toast.add({ severity: "success", summary: "Succès", detail: "Importation réussie", life: 3000 });
    excelData.value = [];
    dialogVisible.value = false;
  } catch (error) {
    console.error("❌ Échec de l'importation :", error);
    errorMessage.value = "Échec de l'importation. Vérifiez le format du fichier.";
  } finally {
    loading.value = false;
  }
}

async function uploadImageToImgBB(buffer: Uint8Array) {
  const blob = new Blob([buffer.buffer], { type: "image/png" });
  const formData = new FormData();
  formData.append("image", blob);
  formData.append("key", API_KEY);

  const res = await axios.post("https://api.imgbb.com/1/upload", formData);
  return res.data.data.url as string;
}
</script>

<style scoped>
</style>
