<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useProduitService } from "@/inversify.config";
import { Produit } from "@/types/entities";
import LogoSVG from '@/assets/icons/logo-noir.svg';
import Carousel from 'primevue/carousel';
import ProduitApercu from "@/components/produits/ProduitApercu.vue";

const produitService = useProduitService();

const produitsRecents = ref<Produit[]>([]);
const categorie =ref<Produit[]>([]);
const enChargement = ref(false);

onMounted(async () => {
  await obtenirProduitRecent();
  await RecuperCategorieProduits();
  

});

async function obtenirProduitRecent() {
  enChargement.value = true;
  
  let produits = await produitService.obtenirTousLesProduits();

  
  if (Array.isArray(produits) && produits.length > 0) {
    
    produitsRecents.value = produits.slice(0, 7);
    console.log(produitsRecents.value[0].idProduit);
  }

  enChargement.value = false;
}



async function RecuperCategorieProduits() {
  let produits = await produitService.obtenirTousLesProduits();
  if (produits && produits.length > 0) {

    const categoriesRencontrees = new Set<string>();
    const produitsUniquesParCategorie: Produit[] = [];

    produits.forEach((produit) => {
      const categorie = produit.categorie?.toLowerCase();
      
      if (categorie) {
        const categorieFormat = categorie;
        
        if (!categoriesRencontrees.has(categorieFormat)) {
          produitsUniquesParCategorie.push(produit);
          categoriesRencontrees.add(categorieFormat);
        }
      }
    });

    categorie.value = produitsUniquesParCategorie;
    console.log(categorie.value);
  }
}


const responsiveOptions = ref([
  {
    breakpoint: '1500px',
    numVisible: 5,
    numScroll: 1
  },
  {
    breakpoint: '1199px',
    numVisible: 4,
    numScroll: 1
  },
  {
    breakpoint: '767px',
    numVisible: 3,
    numScroll: 1
  },
  {
    breakpoint: '575px',
    numVisible: 1,
    numScroll: 1
  }
]);
</script>

<template>
  <!-- Section Hero-->
  <section class="hero-bg flex justify-center items-center">
    <div class="container">
      <div class="hero-content">
        <p class="hero-title">Bijoux. Unique. Faits Main.</p>
        <p class="description ">
          Découvrez des pièces uniques,<br>façonnées avec passion et savoir-faire.
        </p>
        <router-link to="/boutique" class="popup__btn">Magasiner</router-link>
      </div>
    </div>

  </section>
  <!-- Section Recommendations -->
  <section class="recommendations h-screen flex justify-center items-center">
    <div class="container w-full">
      <!--product heading-->
      <div class="recommendations-heading mb-6">
        <p class="font-cormorant font-thin text-6xl">Magasinez par catégories</p>
        <p class="subheading mt-4">Intéréssés par un type de produit en particulier?</p>
      </div>
      <!-- Carousel -->
      <Carousel :value="categorie" :numVisible="5" :numScroll="1" >
        <template #item="item">
          <div class="carousel-item p-2">
            <router-link :to="`/categorie/${item.data.categorie}`" aria-hidden="true" class="w-full">
              <img  :src="`${item.data.photoUrl}`" alt="" class="category-image " />
              <p class="category-name">{{ item.data.categorie}}</p>
            </router-link>
          </div>
        </template>
      </Carousel>
      <!-- Fin Caroususel -->
    </div>
  </section>
  <!-- Section Qui suis-je -->
  <section class="h-screen">
    <div class="about-content h-full">
      <img src="@/assets/img/20240117_170131.jpg" alt="Créatrice" class="about-image h-full">
      <div class="about-text">
        <p class="section-title">Qui suis-je ?</p>
        <p class="description">
          Bienvenue dans l’univers d’Émilie Noël, une créatrice passionnée qui donne vie à chaque bijou de ses propres mains. Chaque pièce est façonnée avec soin dans son atelier, où l’amour du détail et la beauté du fait main prennent tout leur sens. Inspirée par la nature, les émotions et les petites choses du quotidien, Émilie transforme des matériaux simples comme les perles, les pierres fines et les métaux doux en créations uniques qui racontent une histoire.
        </p>
        <p class="description">
          Son approche est guidée par l’authenticité, la délicatesse et le désir de créer des bijoux qui résonnent avec la personne qui les porte. Que ce soit pour offrir, marquer un moment spécial ou simplement se faire plaisir, chaque bijou signé Émilie Noël est une invitation à porter un peu d’art et de poésie au quotidien. Plongez dans son univers sensible et découvrez le charme de l’artisanat local, pensé et créé avec cœur.
        </p>
        <button class="popup__btn">À propos</button>
      </div>
    </div>
  </section>
  <!-- Section Matières -->
  <section class="h-screen">
    <div class="product-content h-full">
      <div class="product-text">
        <p class="subtitle">Matière du produit</p>
        <p class="description">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Metus vulputate eu scelerisque felis imperdiet proin fermentum.
        </p>
        <router-link to="/boutique" class="popup__btn">Magasiner</router-link>
      </div>
      <img src="@/assets/img/IMG_8877.jpg" alt="Matière du produit" class="product-image h-full">
    </div>
  </section>
  <!-- Section Recommendations -->
  <section class="h-screen flex items-center justify-center">
    <div class="container w-full">
      <!--product heading-->
      <div class="mb-6">
        <p class="mb-4 font-cormorant font-thin text-6xl">Recommendations</p>
        <p class="subheading">Vous aimerez sûrement ces produits</p>
      </div>
      <!-- Carousel -->
      <Carousel :value="produitsRecents"
                :numVisible="5"
                :numScroll="1"
                :responsiveOptions="responsiveOptions"
                containerClass="flex justify-start w-full pr-4">
        <template #item="{ data }">
          <ProduitApercu :produit="data" class=""/>
        </template>
      </Carousel>
      <!-- Fin Caroususeel -->
    </div>
  </section>
</template>

<style scoped lang="scss">
.carousel-item {
  width: 100%;
  object-fit: cover;
}
.carousel-control-prev-icon {
  display: block;
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
  padding-bottom: 20px;
  padding-top: 20px;
}

.subheading{
  font-weight: lighter;
  color: grey;
}

.base-container {
  width: 100%;
}

//p,div{
//  margin: 20px 0;
//}

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
  @apply font-cormorant;
  font-size: 2rem;
  font-weight: bold;
}
.title {
  @apply font-cormorant;
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


