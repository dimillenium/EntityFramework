<template>
  <nav class="navbara container">
    <!-- Liens de navigation -->
    <div>
      <!-- Menu Burger -->
      <div class="menu-burger" @click="toggleMenu">
        <i class="fas fa-bars"></i>
      </div>

      <div class="nav-links" :class="{ open: menuOpen }">
        <router-link to="/maps">
          <i class="fas fa-map-marker-alt"></i>
        </router-link>
        <RouterLink :to="t('routes.produits.path')">
          {{ t('routes.produits.name') }}
        </RouterLink>
        <router-link to="/a-propos">À propos</router-link>
        <router-link to="/contact">Nous Joindre</router-link>

        <!-- Menubar avec sous-menu global -->
        <div class="submenu-container">
          <!-- Icône pour ouvrir le sous-menu global -->
          <a @click="toggleSubmenu">
            <i class="fa fa-plus" aria-hidden="true"></i>
          </a>

          <!-- Sous-menu global -->
          <div v-show="submenuOpen" class="submenu">
            <ul>
              <li v-for="page in pages" :key="page.slug">
                <router-link :to="`/page/${page.slug}`">{{ page.slug }}</router-link>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </div>

    <!-- Logo au centre -->
    <div class="logo-container">
      <router-link to="/">
        <LogoSVG style="width: 120px" />
      </router-link>
    </div>

    <!-- Icônes de droite -->
    <div class="nav-icons">
      <div class="search-container nav-links">
        <i class="fas fa-search search-icon"></i>
        <input
            type="text"
            class="border-none rounded-none border-bottom-1 focus:outline-none focus:shadow-1"
            v-model="motcle"
            placeholder="Rechercher"
            @input="recherche"
        />

        <!-- Affichage des résultats -->
        <div v-if="resultatsRecherche.length" class="submenu rounded-md">
          <ul>
            <li v-for="item in resultatsRecherche" :key="typeof item === 'string' ? item : item.id">
              <router-link v-if="typeof item === 'string'" class="flex items-center justify-start"
                           :to="`/categorie/${item}`">
                {{ item }}
              </router-link>
              <router-link v-else class="flex items-center justify-start"
                           :to="`/produit/${item.idProduit}`">
                {{ item.idProduit }}
              </router-link>
            </li>
          </ul>
        </div>

        <i class="fas fa-search search-icon"></i>
      </div>


      <div class="search-icon-mobile">
        <svg width="17" height="17" viewBox="0 0 17 17" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path d="M15.1008 14.186L10.7894 9.87461C11.4584 9.00967 11.8203 7.95215 11.8203 6.83984C11.8203 5.5084 11.3007 4.25996 10.361 3.31865C9.42138 2.37734 8.16963 1.85938 6.83984 1.85938C5.51006 1.85938 4.2583 2.379 3.31865 3.31865C2.37734 4.2583 1.85938 5.5084 1.85938 6.83984C1.85938 8.16963 2.379 9.42139 3.31865 10.361C4.2583 11.3023 5.5084 11.8203 6.83984 11.8203C7.95215 11.8203 9.00801 11.4584 9.87295 10.791L14.1844 15.1008C14.197 15.1134 14.212 15.1235 14.2285 15.1303C14.2451 15.1372 14.2628 15.1407 14.2807 15.1407C14.2985 15.1407 14.3163 15.1372 14.3328 15.1303C14.3493 15.1235 14.3643 15.1134 14.377 15.1008L15.1008 14.3786C15.1134 14.366 15.1235 14.351 15.1303 14.3344C15.1372 14.3179 15.1407 14.3002 15.1407 14.2823C15.1407 14.2644 15.1372 14.2467 15.1303 14.2302C15.1235 14.2137 15.1134 14.1987 15.1008 14.186ZM9.46953 9.46953C8.76562 10.1718 7.83262 10.5586 6.83984 10.5586C5.84707 10.5586 4.91406 10.1718 4.21016 9.46953C3.50791 8.76563 3.12109 7.83262 3.12109 6.83984C3.12109 5.84707 3.50791 4.9124 4.21016 4.21016C4.91406 3.50791 5.84707 3.12109 6.83984 3.12109C7.83262 3.12109 8.76728 3.50625 9.46953 4.21016C10.1718 4.91406 10.5586 5.84707 10.5586 6.83984C10.5586 7.83262 10.1718 8.76729 9.46953 9.46953Z" fill="#705B4F"/>
        </svg>
      </div>

      <router-link to="/panier">
        <svg width="21" height="21" viewBox="0 0 21 21" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path d="M14.4375 6.5625V8.53125C14.4375 8.7053 14.3684 8.87222 14.2453 8.99529C14.1222 9.11836 13.9553 9.1875 13.7812 9.1875H13.125V6.5625H7.875V9.1875H7.21875C7.0447 9.1875 6.87778 9.11836 6.75471 8.99529C6.63164 8.87222 6.5625 8.7053 6.5625 8.53125V6.5625H3.9375V18.375H17.0625V6.5625H14.4375ZM6.5625 5.25C6.5625 4.20571 6.97734 3.20419 7.71577 2.46577C8.45419 1.72734 9.45571 1.3125 10.5 1.3125C11.5443 1.3125 12.5458 1.72734 13.2842 2.46577C14.0227 3.20419 14.4375 4.20571 14.4375 5.25H17.7188C17.8928 5.25 18.0597 5.31914 18.1828 5.44221C18.3059 5.56528 18.375 5.7322 18.375 5.90625V19.0312C18.375 19.2053 18.3059 19.3722 18.1828 19.4953C18.0597 19.6184 17.8928 19.6875 17.7188 19.6875H3.28125C3.1072 19.6875 2.94028 19.6184 2.81721 19.4953C2.69414 19.3722 2.625 19.2053 2.625 19.0312V5.90625C2.625 5.7322 2.69414 5.56528 2.81721 5.44221C2.94028 5.31914 3.1072 5.25 3.28125 5.25H6.5625ZM7.875 5.25H13.125C13.125 4.55381 12.8484 3.88613 12.3562 3.39384C11.8639 2.90156 11.1962 2.625 10.5 2.625C9.80381 2.625 9.13613 2.90156 8.64384 3.39384C8.15156 3.88613 7.875 4.55381 7.875 5.25Z" fill="#705B4F"/>
          <path d="M3.9375 14.4375H17.0625V15.75H3.9375V14.4375Z" fill="#705B4F"/>
        </svg>
      </router-link>
      <router-link to="/like" class="nav-links">
        <svg width="22" height="22" viewBox="0 0 22 22" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path d="M19.8301 6.09298C19.5421 5.42619 19.1269 4.82194 18.6076 4.31408C18.088 3.8047 17.4753 3.3999 16.8029 3.12169C16.1057 2.83207 15.3579 2.68382 14.6029 2.68556C13.5438 2.68556 12.5104 2.9756 11.6123 3.52345C11.3975 3.65451 11.1934 3.79845 11 3.95529C10.8066 3.79845 10.6025 3.65451 10.3877 3.52345C9.48965 2.9756 8.45625 2.68556 7.39707 2.68556C6.63438 2.68556 5.89531 2.83166 5.19707 3.12169C4.52246 3.40099 3.91445 3.80275 3.39238 4.31408C2.87245 4.82137 2.45712 5.42576 2.16992 6.09298C1.87129 6.78693 1.71875 7.52384 1.71875 8.28224C1.71875 8.99767 1.86484 9.74318 2.15488 10.5016C2.39766 11.1354 2.7457 11.7928 3.19043 12.4567C3.89512 13.5072 4.86406 14.6029 6.06719 15.7137C8.06094 17.5549 10.0354 18.8268 10.1191 18.8783L10.6283 19.2049C10.8539 19.3488 11.1439 19.3488 11.3695 19.2049L11.8787 18.8783C11.9625 18.8246 13.9348 17.5549 15.9307 15.7137C17.1338 14.6029 18.1027 13.5072 18.8074 12.4567C19.2522 11.7928 19.6023 11.1354 19.843 10.5016C20.133 9.74318 20.2791 8.99767 20.2791 8.28224C20.2813 7.52384 20.1287 6.78693 19.8301 6.09298ZM11 17.5055C11 17.5055 3.35156 12.6049 3.35156 8.28224C3.35156 6.09298 5.1627 4.31837 7.39707 4.31837C8.96758 4.31837 10.3297 5.19494 11 6.47541C11.6703 5.19494 13.0324 4.31837 14.6029 4.31837C16.8373 4.31837 18.6484 6.09298 18.6484 8.28224C18.6484 12.6049 11 17.5055 11 17.5055Z" fill="#705B4F"/>
        </svg>
      </router-link>
      <router-link to="/login" class="nav-links">
        <svg width="22" height="24" viewBox="0 0 22 24" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path d="M11 12C12.094 12 13.1432 11.5259 13.9168 10.682C14.6904 9.83807 15.125 8.69347 15.125 7.5C15.125 6.30653 14.6904 5.16193 13.9168 4.31802C13.1432 3.47411 12.094 3 11 3C9.90598 3 8.85677 3.47411 8.08318 4.31802C7.3096 5.16193 6.875 6.30653 6.875 7.5C6.875 8.69347 7.3096 9.83807 8.08318 10.682C8.85677 11.5259 9.90598 12 11 12ZM13.75 7.5C13.75 8.29565 13.4603 9.05871 12.9445 9.62132C12.4288 10.1839 11.7293 10.5 11 10.5C10.2707 10.5 9.57118 10.1839 9.05546 9.62132C8.53973 9.05871 8.25 8.29565 8.25 7.5C8.25 6.70435 8.53973 5.94129 9.05546 5.37868C9.57118 4.81607 10.2707 4.5 11 4.5C11.7293 4.5 12.4288 4.81607 12.9445 5.37868C13.4603 5.94129 13.75 6.70435 13.75 7.5ZM19.25 19.5C19.25 21 17.875 21 17.875 21H4.125C4.125 21 2.75 21 2.75 19.5C2.75 18 4.125 13.5 11 13.5C17.875 13.5 19.25 18 19.25 19.5ZM17.875 19.494C17.8736 19.125 17.6632 18.015 16.731 16.998C15.8345 16.02 14.1474 15 11 15C7.85125 15 6.1655 16.02 5.269 16.998C4.33675 18.015 4.12775 19.125 4.125 19.494H17.875Z" fill="#705B4F"/>
        </svg>
      </router-link>
    </div>
  </nav>
</template>

<script lang="ts" setup>
import { onMounted, ref } from "vue";
import LogoSVG from "@/assets/icons/logo-noir.svg";
import { useI18n } from "vue3-i18n";
import {Page, Produit} from "@/types/entities";
import { usePageService,useProduitService } from "@/inversify.config";
import {Categorie} from "@/types/entities/categorie";
const pageService = usePageService();
const produitService = useProduitService();
const pages = ref<Page[]>([]);
const { t } = useI18n();

const menuOpen = ref(false);

const toggleMenu = () => {
  menuOpen.value = !menuOpen.value;
};

const motcle = ref("");
const resultatsRecherche = ref<(Produit | string)[]>([]);
async function recherche() {
  

  if (!motcle.value.trim()) {
    resultatsRecherche.value = []; 
    return;
  }

  try {
    
    const [produits, categories] = await Promise.all([
      RecuperProduits(motcle.value),
      RecuperCategorieProduits(motcle.value)
    ]);
    
    if (Array.isArray(produits) && Array.isArray(categories)) {
      resultatsRecherche.value = [...produits, ...categories.map(categorie => ( categorie ))];
      console.log("afficher les resultats :", resultatsRecherche.value);
    } else {
      console.error("Les résultats de la recherche ne sont pas des tableaux.");
    }
  } catch (error) {
    console.error("Erreur lors de la récupération des recherches :", error);
  }
}
async function RecuperCategorieProduits(motCle: string): Promise<string[]> {
  let produits = await produitService.obtenirTousLesProduits();

  if (produits && produits.length > 0) {
    const categoriesRencontrees = new Set<string>();

    produits.forEach((produit) => {
      const categorie = produit.categorie?.toLowerCase();
      if (categorie && categorie.includes(motCle.toLowerCase())) {
        categoriesRencontrees.add(categorie); 
      }
    });

    return Array.from(categoriesRencontrees);
  }

  return [];
}
async function RecuperProduits(motCle: string): Promise<Produit[]> {
  let produits = await produitService.obtenirTousLesProduits();

  if (produits && produits.length > 0) {
    return produits
        .filter((p) => p.idProduit?.toLowerCase().includes(motCle.toLowerCase())); 
  }

  return [];
}



onMounted(async () => {
  await RecuperPage()
  await recherche()
});

const submenuOpen = ref(false);

const toggleSubmenu = () => {
  submenuOpen.value = !submenuOpen.value; 
};

async function RecuperPage() {
  try {
    const response = await pageService.getAllPages();
    console.log("Réponse récupérée de getAllPages:", response);
    
    if (Array.isArray(response)) {
      pages.value = response; 
    } else {
      console.error("La réponse de getAllPages n'est pas un tableau.");
    }

    console.log("Pages récupérées:", pages.value);
  } catch (error) {
    console.error("Erreur lors de la récupération des pages:", error);
  }
}
</script>

<style lang="scss" scoped>

.search-results {
  margin-top: 10px;
  padding: 10px;
  border: 1px solid #ddd;
  border-radius: 5px;
}

.search-results ul {
  list-style: none;
  padding: 0;
}

.search-results li {
  padding: 8px;
  border-bottom: 1px solid #eee;
}

.search-results li:last-child {
  border-bottom: none;
}

.no-results {
  margin-top: 10px;
  color: red;
  font-weight: bold;
}

.navbara {
  display: flex;
  justify-content: space-between;
  align-items: center;
  width: 100%;
  height: 15vh;
  text-align: center;
  padding: 10px 20px;
  white-space: nowrap;
  //background-color: #eee;
  //position:fixed;
  //top: 0;
  //right: 0;
  //z-index: 1000;
  //margin-bottom: 15vh;
}

.menu-burger {
  display: none;
  cursor: pointer;
}

.menu-burger i {
  font-size: 24px;
}

.nav-links {
  display: flex;
  align-items: center;
  transition: transform 0.3s ease-in-out;
}

.nav-links a {
  text-decoration: none;
  color: #333;
  font-weight: 500;
  margin: 0 15px;
}

.nav-links a:hover {
  color: #007bff;
}

.nav-links.open {
  display: flex;
}

.submenu-container {
  position: relative;
}

.submenu {
  //background-color: #f9f9f9;
  background-color: white;
  position: absolute;
  top: 100%;
  left: 0;
  width: 200px;
  border: 1px solid #ddd;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  z-index: 10;
}

.submenu ul {
  list-style: none;
  padding: 0;
  margin: 0;
}

.submenu li {
  padding: 10px;
}

.submenu li:hover {
  background-color: #f0f0f0;
}

@media (max-width: 900px) {
  .nav-links {
    display: none;
    width: 100%;
    flex-direction: column;
    align-items: flex-start;
  }

  .nav-links.open {
    display: flex;
  }

  .menu-burger {
    display: block;
    margin-bottom: 10px;
  }

  .search-icon-mobile {
    display: block;
    font-size: 20px;
    margin-right: 10px;
  }

  .search-container {
    display: none;
  }

  .nav-icons a i {
    font-size: 20px;
  }

  .nav-links a i {
    font-size: 18px;
  }
}

.logo-container {
  width: 120px;
  height: 100px;
  justify-content: center;
  display: flex;
}

.logo {
  width: 100%;
  height: auto;
  object-fit: contain;
}

.nav-icons {
  justify-content: flex-end;
  display: flex;
  align-items: center;
}

.nav-icons a {
  text-decoration: none;
  color: #333;
  margin: 0 15px;
}

.nav-icons a i {
  font-size: 24px;
}

.nav-icons a:hover {
  color: #007bff;
}

.search-container {
  position: relative;
}

.search-container input {
  padding: 8px 15px 8px 35px;
  width: 200px;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 14px;
}

.search-icon {
  position: absolute;
  top: 50%;
  left: 10px;
  transform: translateY(-50%);
  font-size: 16px;
  color: #aaa;
}

.search-icon-mobile {
  display: none;
}

@media (max-width: 900px) {
  .nav-links {
    display: none;
    width: 100%;
    flex-direction: column;
    align-items: flex-start;
  }

  .nav-links.open {
    display: flex;
  }

  .menu-burger {
    display: block;
    margin-bottom: 10px;
  }

  .search-icon-mobile {
    display: block;
    font-size: 20px;
    margin-right: 10px;
  }

  .search-container {
    display: none;
  }

  .nav-icons a i {
    font-size: 20px;
  }

  .nav-links a i {
    font-size: 18px;
  }
}

@media (min-width: 900px) {
  .menu-burger {
    display: none;
  }

  .search-container {
    display: block;
  }

  .search-icon-mobile {
    display: none;
  }

  .nav-links a {
    display: block;
  }

  .nav-links a i {
    font-size: 24px;
  }
}
</style>
