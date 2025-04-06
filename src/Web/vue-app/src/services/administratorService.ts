import {AxiosResponse} from "axios"
import {injectable} from "inversify"

import "@/extensions/date.extensions"
import {ApiService} from "@/services/apiService"
import {IAdministratorService} from "@/injection/interfaces"
import {Administrator} from "@/types/entities";

@injectable()
export class AdministratorService extends ApiService implements IAdministratorService {
  public async getAuthenticated(): Promise<Administrator | undefined> {
    try {
      const response = await this
        ._httpClient
        .get<Administrator, AxiosResponse<Administrator>>(`${process.env.VUE_APP_API_BASE_URL}/admins/me`)
      return response.data
    } catch (error) {
      return Promise.reject(error)
    }
  }
}