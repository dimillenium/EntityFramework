import {injectable} from "inversify";
import {ApiService} from "./apiService";
import {ICommandeService} from "@/injection/interfaces";
import {Commande} from "@/types/entities";
import {SucceededOrNotResponse} from "@/types/responses";
import {AxiosError, AxiosResponse} from "axios";
import {Guid} from "@/types";

@injectable()
export class CommandeService extends ApiService implements ICommandeService {

    public async LireTousCommandes(): Promise<Commande[]> {
        const response = await this
            ._httpClient
            .get<AxiosResponse<Commande[]>>(`${process.env.VUE_APP_API_BASE_URL}/admin/commandes`)
            .catch((error: AxiosError): AxiosResponse<Commande[]> => {
                return error.response as AxiosResponse<Commande[]>;
            });
        return response.data as Commande[];
    }
}
