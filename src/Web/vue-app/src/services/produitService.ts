import {IProduitService} from "@/injection/interfaces";
import {injectable} from "inversify";
import {ApiService} from "./apiService";
import {AxiosError, AxiosResponse} from "axios";
import {Produit} from "@/types/entities/produit";
import {ICreateProduitRequest} from "@/types/requests";
import {SucceededOrNotResponse} from "@/types/responses";

@injectable()
export class ProduitService extends ApiService implements IProduitService {
    public async obtenirTousLesProduits(): Promise<Produit[]> {
        const response = await this
            ._httpClient
            .get<AxiosResponse<Produit[]>>(`${process.env.VUE_APP_API_BASE_URL}/produits`)
            .catch(function (error: AxiosError): AxiosResponse<Produit[]> {
                return error.response as AxiosResponse<Produit[]>
            })
        return response.data as Produit[]
    }

    public async obtenirParCategorie(categorie: string): Promise<Produit[]> {
        const response = await this
            ._httpClient
            .get<AxiosResponse<Produit[]>>(`${process.env.VUE_APP_API_BASE_URL}/produits/${categorie}`)
            .catch(function (error: AxiosError): AxiosResponse<Produit[]> {
                return error.response as AxiosResponse<Produit[]>
            })
        console.log(response.data as Produit[])
        return response.data as Produit[]
    }

    public async obtenirProduit(idProduit: string): Promise<Produit> {
        const response = await this
            ._httpClient
            .get<AxiosResponse<Produit>>(`${process.env.VUE_APP_API_BASE_URL}/produit/${idProduit}`)
            .catch(function (error: AxiosError): AxiosResponse<Produit> {
                return error.response as AxiosResponse<Produit>;
            });
        return response.data as Produit
    }

    public async obtenirFormats(): Promise<string[]> {
        const response = await this
            ._httpClient
            .get<AxiosResponse<string[]>>(`${process.env.VUE_APP_API_BASE_URL}/produits/formats`)
            .catch(function (error: AxiosError): AxiosResponse<string[]> {
                return error.response as AxiosResponse<string[]>
            })
        return response.data as string[]
    }

    public async obtenirCategories(): Promise<string[]> {
        const response = await this
            ._httpClient
            .get<AxiosResponse<string[]>>(`${process.env.VUE_APP_API_BASE_URL}/produits/categories`)
            .catch(function (error: AxiosError): AxiosResponse<string[]> {
                return error.response as AxiosResponse<string[]>
            })
        return response.data as string[]
    }
    public async obtenirCouleurs(): Promise<string[]> {
        const response = await this
            ._httpClient
            .get<AxiosResponse<string[]>>(`${process.env.VUE_APP_API_BASE_URL}/produits/couleurs`)
            .catch(function (error: AxiosError): AxiosResponse<string[]> {
                return error.response as AxiosResponse<string[]>
            })
        return response.data as string[]
    }
    public async createProduit(request: ICreateProduitRequest): Promise<SucceededOrNotResponse> {
        const formData = new FormData();

        formData.append("IdProduit", request.idProduit ?? "");
        formData.append("Description", request.description ?? "");
        formData.append("Prix", request.prix?.toString() ?? "0");
        formData.append("Categorie", request.categorie ?? "");
        formData.append("Format", request.format ?? "");
        formData.append("Couleur", request.couleur ?? "");
        formData.append("Quantite", request.quantite?.toString() ?? "0");

        // 🟢 Important : ajouter chaque URL de photo individuellement avec le même nom
        request.PhotoUrl?.forEach(url => {
            formData.append("PhotoUrl", url);
        });

        const response = await this._httpClient.post(
            `${process.env.VUE_APP_API_BASE_URL}/produits`,
            formData,
            {
                headers: {
                    "Content-Type": "multipart/form-data"
                }
            }
        );

        console.log("✅ Produit Data envoyé :", request);
        console.log("📸 Réponse du backend :", response.data);

        return new SucceededOrNotResponse(response.data.succeeded, response.data.errors);
    }
    // public async createProduit(request: ICreateProduitRequest): Promise<SucceededOrNotResponse> {
    //     const response = await this._httpClient.post(
    //         `${process.env.VUE_APP_API_BASE_URL}/produits`,
    //         request, // Ici c'est un objet JSON
    //         this.headersWithJsonContentType() // Content-Type: application/json
    //     );
    //     // const response = await this
    //     //     ._httpClient
    //     //     .post<ICreateProduitRequest, AxiosResponse<any>>(
    //     //         `${process.env.VUE_APP_API_BASE_URL}/produits`,
    //     //         request,
    //     //         this.headersWithFormDataContentType())
    //     //     .catch(function (error: AxiosError): AxiosResponse<SucceededOrNotResponse> {
    //     //         console.error("Erreur lors de la requête:", error);
    //     //         return error.response as AxiosResponse<SucceededOrNotResponse>
    //     //     })
    //     console.log("Produit Data to be sent:", request);
    //     console.log("Photo URLs to be sent:", response.data);
    //     const succeededOrNotResponse = response.data as SucceededOrNotResponse
    //     return new SucceededOrNotResponse(succeededOrNotResponse.succeeded, succeededOrNotResponse.errors)
    // }
}