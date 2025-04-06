import {Couleur} from "@/types/entities/couleur";
import {Categorie} from "@/types/entities/categorie";
import {Format} from "@/types/entities/format";

export class Produit {
    idProduit?: string;
    description?: string;
    prix?: number;
    categorie?: string;
    format?: string;
    couleur?: string;
    quantite?: number;
    photosUrl?: string [];
}