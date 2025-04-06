import i18n from "@/i18n";
import {Role} from "@/types/enums";
import {createRouter, createWebHistory} from "vue-router";

import Login from "@/views/Login.vue";
import TwoFactor from "@/views/TwoFactor.vue";
import ForgotPassword from "@/views/ForgotPassword.vue";
import ResetPassword from "@/views/ResetPassword.vue";
import Home from "@/views/Home.vue";
import Account from "@/views/shared/Account.vue";

import Admin from "../views/admin/Admin.vue";
import AdminMemberIndex from "@/views/admin/members/AdminMemberIndex.vue";
import AdminAddMemberForm from "@/views/admin/members/AdminAddMemberForm.vue";
import AdminEditMemberForm from "@/views/admin/members/AdminEditMemberForm.vue";
import AdminAddPage from "@/views/admin/AdminAddPage.vue";
import AjouterProduit from "@/views/admin/produits/AjouterProduit.vue";

import Books from "../views/member/Books.vue";
import BookIndex from "@/views/member/BookIndex.vue";
import AddBookForm from "@/views/member/AddBookForm.vue";
import EditBookForm from "@/views/member/EditBookForm.vue";

import {getLocalizedRoutes} from "@/locales/helpers";
import {useUserStore} from "@/stores/userStore";
import Commandes from "@/views/admin/commandes/Commandes.vue";
import Dashboard from "@/views/admin/Dashboard/Dashboard.vue";

import Boutique from "@/views/Boutique.vue";
import BoutiqueCategorie from "@/views/BoutiqueCategorie.vue";
import AdminProduits from "@/views/admin/produits/AdminProduits.vue";
import NewPage from "@/views/NewPage.vue";

const router = createRouter({
  // eslint-disable-next-line
  scrollBehavior(to, from, savedPosition) {
    // always scroll to top
    return {top: 0};
  },
  history: createWebHistory(),
  routes: [
    {
      path: i18n.t("routes.home.path"),
      alias: getLocalizedRoutes("routes.home.path"),
      name: "home",
      component: Home,
      meta: {
        title: "routes.home.name"
      },
    },
    {
      path:i18n.t("routes.categorie.path"),
      alias: getLocalizedRoutes("routes.categorie.path"),
      name:"categorie",
      component: BoutiqueCategorie,
      meta: {
        title: "routes.categorie.name"
      },
    },
    {
      path:i18n.t("routes.page.path"),
      alias: getLocalizedRoutes("routes.page.path"),
      name:"page",
      component: NewPage,
      meta: {
        title: "routes.page.name"
      },
    },
    {
      path:i18n.t("routes.produits.path"),
      alias: getLocalizedRoutes("routes.produits.path"),
      name:"Produits",
      component: Boutique,
    },
    {
      path: i18n.t("routes.login.path"),
      alias: getLocalizedRoutes("routes.login.path"),
      name: "login",
      component: Login,
      meta: {
        title: "routes.login.name"
      }
    },
    {
      path: i18n.t("routes.twoFactor.path"),
      alias: getLocalizedRoutes("routes.twoFactor.path"),
      name: "twoFactor",
      component: TwoFactor,
      meta: {
        title: "routes.twoFactor.name"
      }
    },
    {
      path: i18n.t("routes.forgotPassword.path"),
      alias: getLocalizedRoutes("routes.forgotPassword.path"),
      name: "forgotPassword",
      component: ForgotPassword,
      meta: {
        title: "routes.forgotPassword.name"
      }
    },
    {
      path: i18n.t("routes.resetPassword.path"),
      alias: getLocalizedRoutes("routes.resetPassword.path"),
      name: "resetPassword",
      component: ResetPassword,
      props: (route) => ({userId: route.query.userId, token: route.query.token}),
      meta: {
        title: "routes.resetPassword.name"
      }
    },
    {
      path: i18n.t("routes.account.path"),
      alias: getLocalizedRoutes("routes.account.path"),
      name: "account",
      component: Account,
      meta: {
        title: "routes.account.name"
      }
    },
    {
      path: i18n.t("routes.admin.path"),
      name: "admin",
      component: Admin,
      meta: {
        requiredRole: Role.Admin,
        noLinkInBreadcrumbs: true,
      },
      children: [
        {
          path: i18n.t("routes.admin.children.members.path"),
          name: "admin.children.members",
          component: Admin,
          children: [
            {
              path: "",
              name: "admin.children.members.index",
              component: AdminMemberIndex,
            },
            {
              path: i18n.t("routes.admin.children.members.add.path"),
              name: "admin.children.members.add",
              component: AdminAddMemberForm,
            },
            {
              path: i18n.t("routes.admin.children.members.edit.path"),
              alias: i18n.t("routes.admin.children.members.edit.path"),
              name: "admin.children.members.edit",
              component: AdminEditMemberForm,
              props: true
            },
          ],
        },
        {
          path: i18n.t("routes.admin.children.page.path"),
          name: "Page",
          component: AdminAddPage,
        },{
          path: i18n.t("routes.admin.children.dashboard.path"),
          name: "admin.dashboard",
          component: Dashboard
        },
        {
          path: i18n.t("routes.admin.children.commandes.path"),
          name: "admin.commandes",
          component: Commandes
        },
        {
          path: i18n.t("routes.admin.children.produits.path"),
          name: "admin.produits",
          component: AdminProduits
        },
        {
          path: i18n.t("routes.admin.children.ajouter-page.path"),
          name: "admin.ajouter-page",
          component: AdminAddPage
        },
        {
          path: i18n.t("routes.admin.children.ajouter-produit.path"),
          name: "admin.ajouter-produit",
          component: AjouterProduit
        },  
      ]
    },
    {
      path: i18n.t("routes.books.path"),
      alias: getLocalizedRoutes("routes.books.path"),
      name: "books",
      component: Books,
      meta: {
        requiredRole: Role.Member,
        title: "routes.books.name"
      },
      children: [
        {
          path: "",
          name: "books.index",
          component: BookIndex,
          meta: {
            title: "routes.books.name"
          }
        },
        {
          path: i18n.t("routes.books.children.add.path"),
          alias: getLocalizedRoutes("routes.books.children.add.path"),
          name: "books.children.add",
          component: AddBookForm,
          meta: {
            title: "routes.books.children.add.name"
          }
        },
        {
          path: i18n.t("routes.books.children.edit.path"),
          alias: getLocalizedRoutes("routes.books.children.edit.path"),
          name: "books.children.edit",
          component: EditBookForm,
          props: true,
          meta: {
            title: "routes.books.children.edit.name"
          }
        }
      ]
    },
  ]
});

// eslint-disable-next-line
router.beforeEach(async (to, from) => {
  const userStore = useUserStore();

  // 🔹 Vérifier si l'utilisateur est Admin
  const isAdmin = userStore.hasRole(Role.Admin);

  // 🔹 Rediriger un admin **obligatoirement** vers le Dashboard s'il n'est pas déjà sur une route admin
  if (isAdmin && !to.path.startsWith('/administration')) {
    return { name: 'admin.dashboard' };
  }

  // 🔹 Si la route n'a pas de restriction de rôle, on continue
  if (!to.meta.requiredRole) return;

  const isRoleArray = Array.isArray(to.meta.requiredRole);
  const doesNotHaveGivenRole = !isRoleArray && !userStore.hasRole(to.meta.requiredRole as Role);
  const hasNoRoleAmongRoleList = isRoleArray && !userStore.hasOneOfTheseRoles(to.meta.requiredRole as Role[]);

  // 🔹 Si l'utilisateur n'a pas le bon rôle, redirection vers son compte
  if (doesNotHaveGivenRole || hasNoRoleAmongRoleList) {
    return { name: "account" };
  }
});
console.log(i18n.t("routes.home.path"));  // "/"
console.log(i18n.t("routes.home.children.categorie.path"));  // "categorie"

export const Router = router;