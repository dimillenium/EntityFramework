<template>
  <div class="relative inline-block">
    <Button
        icon="pi pi-user"
        label="Admin"
        @click="toggleMenu"
        class="p-button-text text-gray-900 hover:bg-black-alpha-10 border-2 border-gray-900 rounded-md"
    />

    <Transition name="fade">
      <div v-if="isOpen" class="dropdown-menu p-2 rounded-lg border-2 border-gray-200 mt-2">
        <ul>
          <li class="rounded-lg w-full flex justify-between items-center p-3" @click="goToAccount">
            <p class="font-karla font-bold">Mon compte</p>
            <i class="pi pi-user mr-2"></i>
          </li>
          <li class="rounded-lg">
            <LogoutButton class="logoutBtn" />
          </li>
        </ul>
      </div>
    </Transition>
  </div>
</template>

<script lang="ts" setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import Button from 'primevue/button';
import LogoutPopup from "@/components/layouts/items/LogoutPopup.vue";
import LogoutButton from "@/components/navigation/LogoutButton.vue";

const router = useRouter();
const isOpen = ref(false);

const toggleMenu = () => {
  isOpen.value = !isOpen.value;
};

const goToAccount = () => {
  router.push('/mon-compte'); // Redirection vers la page Mon Compte
  isOpen.value = false;
};
</script>

<style scoped>
.dropdown-menu {
  position: absolute;
  top: 2.5rem;
  right: 0;
  background: white;
  z-index: 10;
}

.dropdown-menu ul {
  list-style: none;
  padding: 0;
  margin: 0;
}

.dropdown-menu li {
  padding: 10px;
  cursor: pointer;
  display: flex;
  gap: 1rem;
  align-items: center;
  transition: background 0.2s ease;
}

.dropdown-menu li:hover {
  background: #f4f4f4;
  cursor: pointer;
}

.logoutBtn {
  display: flex;
  gap: 2rem;
  align-items: center;
}

.fade-enter-active, .fade-leave-active {
  transition: opacity 0.2s;
}

.fade-enter-from, .fade-leave-to {
  opacity: 0;
}
</style>
