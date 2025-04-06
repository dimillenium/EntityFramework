<script setup lang="ts">
import { ref } from 'vue';
import {RouterLink} from "vue-router";

const columns = ["Nous joindre", "A propos", "Magasin", "Coordonnées de contact"];
const rows = [
  ["Contact", "À propos", "Boutique", "Tél:+1(418)000-0000"],
  ["Suivre ma commande", "", "Colliers", "Courriel:info@les projetsdem.ca"],
  ["FAQ (Question & Politique)", "", "Boucles d'oreilles", ""],
  ["", "", "Bracelets", ""],
  [" ", "", "Bagues", ""],
];


const links = [
  ["/contact", "/apropos", "/magasin", "tel:+141800000000"],
  ["/suivre-commande", "", "/colliers", "mailto:info@lesprojetsdem.ca"],
  ["/faq", "", "/boucles-oreilles", ""],
  ["", "", "/bracelets", ""],
  ["", "", "/bagues", ""],
];
</script>

<template>
  <div class="grid__row container" style="padding: 2rem">
    <div class="table grid__col--lg-8">

      <div class="row header">
        <div class="cell" v-for="(column, index) in columns" :key="'header-' + index">{{ column }}</div>
      </div>

      <div class="row" v-for="(row, rowIndex) in rows" :key="'row-' + rowIndex">
        <div class="cell" v-for="(cell, colIndex) in row" :key="'cell-' + rowIndex + '-' + colIndex">

          <router-link v-if="links[rowIndex][colIndex] && !links[rowIndex][colIndex].startsWith('tel:') && !links[rowIndex][colIndex].startsWith('mailto:')" :to="links[rowIndex][colIndex]">
            {{ cell }}
          </router-link>

          <a v-else-if="links[rowIndex][colIndex]" :href="links[rowIndex][colIndex]" target="_blank">
            {{ cell }}
          </a>

          <span v-else>{{ cell }}</span>
        </div>
      </div>
    </div>
    <div class="grid__col--lg-4">
      <router-link to="/">
        <img class="logo" src="@/assets/icons/logo-transparent-png.png" alt="iconSite" />
      </router-link>
      <p ><b>Beauté Éthique, Bijoux Durable</b> </p>
      <p>Nous privilégions des matériaux resposable et un savoir-faire artisanal respectueux de l'environnement.</p>
      <div>
        <router-link to="/">
          <i class="fab fa-instagram" style="margin-right: 3px"></i>
        </router-link>
        <router-link to="/">
          <i class="fab fa-facebook" style="margin-left: 3px"></i>
        </router-link>
      </div>
    </div>
    <div> <p>Les projets D'EM. Tous droits réservés</p></div>
  </div>
</template>

<style scoped lang="scss">
.table {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  width: 100%;
}

.row {
  display: contents;
}

.cell {
  padding: 4px;
  text-align: start;
}

.header .cell {
  font-weight: bold;
  color: #E5A97D; /* Changer la couleur du texte de la première ligne */
}

a, router-link {
  
  text-decoration: none;
}

a:hover, router-link:hover {
  text-decoration: underline; /* Style au survol du lien */
}
.logo {
  width: 150px; /* Redimensionner le logo */
  height: auto;
  display: block;
  margin: 0 auto;
}
</style>
