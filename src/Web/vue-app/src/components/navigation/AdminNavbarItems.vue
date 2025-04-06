<template>
  <ul class="navbar-admin__nav">
    <!-- Lien vers "Dashboard" -->
    <li>
      <router-link
          :to="{ name: 'admin.dashboard' }"
          class="navbar-admin__navlink"
      >
        <img
            src='@/assets/icons/dashboard.png'
            :alt="t('routes.admin.children.dashboard.name')"
            class="navbar-admin__icon"
        />
        <img
            src='@/assets/icons/dashboard-white.png'
            :alt="t('routes.admin.children.dashboard.name')"
            class="navbar-admin__icon-white"
        />
        {{ t('routes.admin.children.dashboard.name') }}
      </router-link>
    </li>

    <!-- Lien vers "Produits" -->
    <li>
      <router-link
          :to="{ name: 'admin.produits' }"
          class="navbar-admin__navlink"
      >
        <img
            src='@/assets/icons/box.png'
            :alt="t('routes.admin.children.dashboard.name')"
            class="navbar-admin__icon"
        />
        {{ t('routes.admin.children.produits.name') }}
      </router-link>
    </li>

    <!-- Lien vers "Commandes" -->
    <li>
      <router-link
          :to="{ name: 'admin.commandes' }"
          class="navbar-admin__navlink"
      >
        <img
            src="@/assets/icons/clipboard.png"
            :alt="t('routes.admin.children.commandes.name')"
            class="navbar-admin__icon"
        />
        {{ t('routes.admin.children.commandes.name') }}
      </router-link>
    </li>

    <!-- Lien vers "Ajouter une page" -->
    <li>
      <router-link
          :to="{ name: 'admin.ajouter-page' }"
          class="navbar-admin__navlink"
      >
        <img
            src='@/assets/icons/browser.png'
            :alt="t('routes.admin.children.dashboard.name')"
            class="navbar-admin__icon"
        />
        {{ t('routes.admin.children.ajouter-page.name') }}
      </router-link>
    </li>
  </ul>
</template>

<script lang="ts" setup>
import { useI18n } from "vue3-i18n"
import {useRoute} from "vue-router";

const { t } = useI18n()
const route = useRoute()

// Vérifie si la route actuelle correspond au lien
const isActiveRoute = (routeName: string): boolean => {
  return route.name === routeName
}

// Retourne l'icône appropriée (blanche si actif, normale sinon)
const getIcon = (iconName: string, routeName: string): string => {
  const isActive = isActiveRoute(routeName)
  return new URL(`/src/assets/icons/${iconName}${isActive ? '-white' : ''}.png`, import.meta.url).href
}
</script>

<style scoped lang="scss">
.navbar-admin__nav {
  display: flex;
  flex-direction: column;
  list-style: none;
  margin: 0;
  padding: 0;
  gap: 1rem; /* Espace entre chaque lien */
}

.navbar-admin__navlink {
  display: flex;
  align-items: center;
  gap: 0.5rem; /* Espace entre l'icône et le texte */
  text-decoration: none;
  color: #000; /* Noir par défaut */
  font-weight: 500;
  padding: 0.75rem 1rem;
  border-radius: 8px;
  transition: background 0.3s ease-in-out;
  position: relative;
}

/* Changement de style pour la route active */
.navbar-admin__navlink.active {
  background-color: #141414; /* Fond noir */
  color: #fff; /* Texte blanc */
}

/* Taille des icônes */
.navbar-admin__icon,
.navbar-admin__icon-white {
  width: 20px;
  height: 20px;
}

/* Par défaut, cacher l'icône blanche */
.navbar-admin__icon-white {
  display: none;
}

/* Quand le lien est actif, afficher l'icône blanche et cacher l'icône normale */
.navbar-admin__navlink.router-link-active.active .navbar-admin__icon {
  display: none;
}

.navbar-admin__navlink.router-link-active.active .navbar-admin__icon-white {
  display: block;
}
</style>

