
import {LigneCommande} from "@/types/entities/ligneCommande";

export class Commande {
    id?: string;
    dateCommande?: Date;
    numeroCommande?: string;
    moyenPaiement?: "Paypal" | "Interact";
    optionLivraison?: "PosteCanada" | "RetraitChezLaVendeuse" | "LivraisonParLaVendeuse";
    numeroSuivi?: string;
    emailClient?: string;
    adresseLivraison?: string;
    montantTotal?: number;
    lignesCommande?: LigneCommande[];
}