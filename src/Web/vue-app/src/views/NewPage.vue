<script setup lang="ts">
import { ref, onMounted,watch } from 'vue';
import {usePageService, useProduitService} from "@/inversify.config";
import {Page, Produit} from "@/types/entities";
import Carousel  from "primevue/carousel";
import LogoSVG from '@/assets/icons/logo-noir.svg';
import { useRoute } from 'vue-router';
import ProduitApercu from "@/components/produits/ProduitApercu.vue";
const produitService = useProduitService();

const pageService = usePageService();
const pageData = ref<Page>();  

const route = useRoute();
const pageSlug = route.params.slug as string;
const enChargement = ref(false);
const produitsRecents = ref<Produit[]>([]);
onMounted(async () => {
  await recupererPage(pageSlug);
  await obtenirProduitRecent();
});
watch(() => route.params.slug, async (newSlug) => {
 
  const slug = Array.isArray(newSlug) ? newSlug[0] : newSlug as string;
  await recupererPage(slug);  
});
async function recupererPage(slug: string) {
  try {
    const page = await pageService.getPage(slug); 
    console.log(page.section1);
    if (page) {
      pageData.value = page;  
    } else {
      console.error('Page non trouvée');
    }
  } catch (error) {
    console.error('Erreur lors de la récupération des données de la page:', error);
  }
}
async function obtenirProduitRecent() {
  enChargement.value = true;

  let produits = await produitService.obtenirTousLesProduits();


  if (Array.isArray(produits) && produits.length > 0) {

    produitsRecents.value = produits.slice(0, 7);
    console.log(produitsRecents.value[0].idProduit);
  }

  enChargement.value = false;
}

</script>

<template>
  <!-- Section Hero-->
  <section class="hero-bg" :style="{ backgroundImage: 'url(' + (pageData?.background || '') + ')' }">
    <div class="base-container">
      <div class="hero-content">
        <p class="title">{{ pageData?.slug || 'Bijoux. Unique. Faits Main.' }}</p>
        <p class="description">
          Découvrez des pièces uniques,<br>façonnées avec passion et savoir-faire.
        </p>
        <button class="popup__btn">Magasiner</button>
      </div>
    </div>
  </section>


  <section v-if="pageData" class="about-content">
    <img :src="pageData.background" alt="Matière du produit" class="product-image">
    <div class="about-text">
      <p class="subtitle">{{ pageData.slug }}</p>
      <p class="description">
        {{ pageData.section1 }}
      </p>
    </div>
  </section>

  <section v-if="pageData" class="product-content">
    <div class="product-text">
      <!-- Idem pour slug et section1 -->
      <p class="subtitle">{{ pageData.slug }}</p>
      <p class="description">
        {{ pageData.section1 }}
      </p>
    </div>
    <img :src="pageData.background" alt="Matière du produit" class="product-image">
  </section>

  <!-- Section Recommendations -->
  <section class="recommendations">
    <div class="recommendations-container">
      <!--product heading-->
      <div class="recommendations-heading">
        <p class="subtitle">Recommendations</p>
        <p class="subheading">Vous aimerez sûrement ces produits</p>
      </div>
      <!-- Carousel -->
      <Carousel :value="produitsRecents" :numVisible="5" :numScroll="1" :responsiveOptions="responsiveOptions" >
        <template #item="item" >
          <div style="margin: 20px;">
            <ProduitApercu :produit="item.data" class="flex flex-col sm:flex-row sm:items-center p-6 gap-6"/>
          </div>
        </template>
      </Carousel>
      <!-- Fin Caroususeel -->

    </div>
  </section>
</template>

<style scoped lang="scss">
section{
  height: 85dvh;
}
.hero-bg {
  background-image: url("@/assets/img/IMG_8627.jpg");
  background-size: cover;
  background-position: center;
  color: white;
  text-align: start;
  display: flex;
  align-items: center;
  height: 85vh;
}

.hero-bg::after {
  content: "";
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: linear-gradient(to right, rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0));
  z-index: 1;
}

.hero-content {
  z-index: 2;
  width: fit-content;
  display: flex;
  flex-direction: column;
  gap: 20px;
  justify-content: start;
  align-content: start;
}

.section-content {
  padding: 20px;
  background-color: #f9f9f9;
}

.subtitle {
  font-size: 2rem;
  font-weight: bold;
}

.description {
  font-size: 1.2rem;
}
.carousel-item {
  padding: 10px;
  object-fit: cover;
}
.carousel-control-prev-icon {
  display: block;
}

.custom-carousel {
  height: 50vh; 
  overflow: hidden; 
}

.base-container {
  padding-right: 5rem;
  padding-left: 5rem;
  padding-bottom: 80px;
  padding-top: 80px;
}

.recommendations-container{
  padding-right: 7rem;
  padding-left: 7rem;
  padding-bottom: 60px;
  padding-top: 60px;
}

.recommendations-heading{

}

.subheading{
  font-weight: lighter;
  color: grey;
}

.base-container {
  width: 100%;
}

p,div{
  margin: 20px 0;
}

.hero-bg {
  background-image: url("@/assets/img/IMG_8627.jpg");
  background-size: cover;
  background-position: center;
  color: white;
  text-align:start;
  display: flex;
  align-items: center;
  height: 85vh;
}

.hero-bg::after {
  content: "";
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: linear-gradient(to right, rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0));
  z-index: 1;
}

.hero-content {
  z-index: 2;
  width: fit-content;
  display: flex;
  flex-direction: column;
  gap: 20px;
  justify-content: start;
  align-content: start;
}


.row {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(250px, 1fr));
  gap: 40px;
}


.category-item {
  text-align: center;
  overflow: hidden;
}


.category-image {
  height: 40vh;
  object-fit: cover;
}


.category-name {
  margin-top: 10px;
  font-size: 16px;
  color: #333;
  font-weight: bold;
}




.about-content {
  display: flex;
  color: white;
  background-color: #111;
  position: relative;
}

.about-image {
  width: 33%;
  min-height: 85vh;
  object-fit: cover;
  position: relative;
}

.about-text {
  max-width: 600px;
  margin: 0 100px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  gap: 20px;
}

.subtitle {
  font-size: 2rem;
  font-weight: bold;
}
.title {
  font-size: 3rem;

  text-transform: none;
}

.description {
  font-size: 1.2rem;
}

.popup__btn {
  width: fit-content;
  padding: 10px 20px;
  border: 1px solid white;
  border-radius: 0;
  transition: background-color 0.3s ease;
  cursor: pointer;
}

.popup__btn:hover {
  background-color: #141414;
  color: white;
  border: 1px solid black;
}


.product-section {
  background-color: white;
  color: black;
  padding: 50px 10px;
}

.product-content {
  display: flex;
  justify-content: space-between;
  position: relative;
  background-color: #FAF0E7;
}

.product-text {
  max-width: 600px;
  margin: 0 100px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  gap: 20px;
  z-index: 10;
}

.product-image {
  width: 33%;
  min-height: 85vh;
  object-fit: cover;
  position: relative;
}




@media (max-width: 768px) {
  .row {
    grid-template-columns: repeat(auto-fill, minmax(100px, 1fr));
  }
  .section.about {
    position: relative;
    width: 100%;

  }


  .about-content, .product-content {
    flex-direction: column;
  }

  .about-image, .product-image {
    width: 100%;
    margin-bottom: 10px;
  }

  .about-text, .product-text {
    max-width: 100%;
    padding: 0 10px;
    background-color: rgba(0, 0, 0, 0.5);

  }


  .about-content {
    height: 500px;
  }

  .about-text, .product-text {
    position: absolute;
    top: 60%;
    left: 30%;
    transform: translate(-50%, -50%);
    text-align: center;
    width: 80%;
  }




  .subtitle {
    font-size: 1.8rem;
  }

  .description {
    font-size: 1rem;
  }
}




</style>
