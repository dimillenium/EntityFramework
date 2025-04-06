<template>
  <div class="content-grid">
    <div class="content-grid__header">
      <h1>{{ t('pages.admin.createPage') }}</h1>
    </div>

    <div class="form-container">
      <div class="form-group">
        <label for="title">Titre</label>
        <InputText id="Slug" type="text" v-model="page.Slug" placeholder="Titre de la page" />
      </div>

      <label>Image en fond d'écran</label>
      <input type="file" ref="fileInput" @change="handleFileSelect">
      <p v-if="page.background">
        <a :href="page.background" target="_blank">{{ page.background }}</a>
      </p>

      <div class="form-group">
        <label for="section_1">Section 1</label>
        <Textarea id="section_1" v-model="page.section1" rows="5" cols="30" style="resize: none" />
      </div>

      <div class="form-group">
        <label for="section_2">Section 2</label>
        <Textarea id="section_2" v-model="page.section2" rows="5" cols="30" style="resize: none" />
      </div>

      <Button label="Submit" @click="submitForm" />

      <p v-if="message" class="message">{{ message }}</p>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref } from "vue";
import { useI18n } from "vue3-i18n";
import { useRouter } from "vue-router";
import Textarea from 'primevue/textarea';
import InputText from 'primevue/inputtext';
import Button from "primevue/button";
import axios from "axios";
import {ICreatePageRequest} from "@/types/requests/createPageRequest";
import {usePageService} from "@/inversify.config";

const { t } = useI18n();
const router = useRouter();
const pageService = usePageService();

const page = ref({
  Slug: "",
  background: "",
  section1: "",
  section2: "",
});

const message = ref("");
const selectedFile = ref<File | null>(null);
const fileInput = ref<HTMLInputElement | null>(null); // Référence à l'input file
const API_KEY = "ed2b0188fc04f797225f7ae1f12c4eff";


const handleFileSelect = (event: Event) => {
  const target = event.target as HTMLInputElement;
  selectedFile.value = target.files?.[0] || null;
};


const uploadImage = async () => {
  if (!selectedFile.value) return null;

  const formData = new FormData();
  formData.append("image", selectedFile.value);
  formData.append("key", API_KEY);

  try {
    const response = await axios.post("https://api.imgbb.com/1/upload", formData);
    return response.data.data.url;
  } catch (error) {
    console.error("Erreur lors de l'upload de l'image :", error);
    return null;
  }
};

// Fonction pour réinitialiser le formulaire
const resetForm = () => {
  page.value = { Slug: "", background: "", section1: "", section2: "" };
  selectedFile.value = null;
  if (fileInput.value) fileInput.value.value = ""; 
};


const submitForm = async () => {
  message.value = "Envoi en cours...";

  
  if (selectedFile.value) {
    const uploadedImageUrl = await uploadImage();
    if (uploadedImageUrl) {
      page.value.background = uploadedImageUrl;
    } else {
      message.value = "Erreur lors du téléchargement de l'image.";
      return;
    }
  }

  try {
    // Envoi des données au backend
    let response = await pageService.createPage(page.value as ICreatePageRequest);

    if (response.succeeded) {
      message.value = "La page a été enregistrée avec succès !";
      resetForm(); // Réinitialiser le formulaire après soumission
    } else {
      // Traitement des erreurs côté client
      message.value = "Une erreur est survenue lors de l'enregistrement.";
    }
  } catch (error: unknown) {
    console.error("Erreur lors de l'envoi des données :", error);

    // Vérifier si l'erreur est une instance de Error
    if (error instanceof Error) {
      message.value = error.message;  // Utilisation du message d'erreur
    } else {
      message.value = "Erreur lors de l'enregistrement."; // Message générique en cas d'erreur non gérée
    }
  }
};


</script>

<style scoped>
.form-container {
  max-width: 600px;
  margin: auto;
  display: flex;
  flex-direction: column;
  gap: 15px;
}

.form-group {
  display: flex;
  flex-direction: column;
}

.form-group label {
  font-weight: bold;
  margin-bottom: 5px;
}

input, textarea {
  width: 100%;
  padding: 8px;
  border: 1px solid #ccc;
  border-radius: 5px;
}

.message {
  margin-top: 10px;
  padding: 10px;
  background-color: #d4edda;
  color: #155724;
  border: 1px solid #c3e6cb;
  border-radius: 5px;
  text-align: center;
}

button {
  background-color: #28a745;
  color: white;
  padding: 10px;
  border: none;
  cursor: pointer;
  border-radius: 5px;
}

@media (min-width: 768px) {
  .form-group {
    display: flex;
    flex-direction: row;
    align-items: center;
    gap: 10px;
  }

  .form-group label {
    min-width: 150px;
  }

  input, textarea {
    flex-grow: 1;
  }
}
</style>
