import {IPageService} from "@/injection/interfaces";
import {injectable} from "inversify";
import {ApiService} from "./apiService";
import {SucceededOrNotResponse} from "@/types/responses";
import {AxiosError, AxiosResponse} from "axios";
import {ICreatePageRequest} from "@/types/requests/createPageRequest";
import {Page} from "@/types/entities";
// import {IEditPageRequest} from "@/types/requests/editPageRequest";

@injectable()
export class PageService extends ApiService implements IPageService {
    public async getAllPages(): Promise<Page[]> {
        const response = await this
            ._httpClient
            .get<AxiosResponse<Page[]>>(`${process.env.VUE_APP_API_BASE_URL}/pages`)
            .catch(function (error: AxiosError): AxiosResponse<Page[]> {
                return error.response as AxiosResponse<Page[]>
            })
        return response.data as Page[]
    }

    public async getPage(PageId: string): Promise<Page> {
        const response = await this
            ._httpClient
            .get<AxiosResponse<Page>>(`${process.env.VUE_APP_API_BASE_URL}/Page/${PageId}`)
            .catch(function (error: AxiosError): AxiosResponse<Page> {
                return error.response as AxiosResponse<Page>;
            });
        return response.data as Page
    }

    public async deletePage(PageId: string): Promise<SucceededOrNotResponse> {
        const response = await this
            ._httpClient
            .delete<AxiosResponse<any>>(`${process.env.VUE_APP_API_BASE_URL}/api/Pages/${PageId}`)
            .catch(function (error: AxiosError): AxiosResponse<SucceededOrNotResponse> {
                return error.response as AxiosResponse<SucceededOrNotResponse>
            })
        return new SucceededOrNotResponse(response.status === 204)
    }

    public async createPage(request: ICreatePageRequest): Promise<SucceededOrNotResponse> {
        const response = await this
            ._httpClient
            .post<ICreatePageRequest, AxiosResponse<any>>(
                `${process.env.VUE_APP_API_BASE_URL}/page/create`,
                request,
                this.headersWithFormDataContentType())
            .catch(function (error: AxiosError): AxiosResponse<SucceededOrNotResponse> {
                return error.response as AxiosResponse<SucceededOrNotResponse>
            })
       
        console.log("Page Data to be sent:", request);
        const succeededOrNotResponse = response.data as SucceededOrNotResponse
        return new SucceededOrNotResponse(succeededOrNotResponse.succeeded, succeededOrNotResponse.errors)
    }

    // public async editPage(request: IEditPageRequest): Promise<SucceededOrNotResponse> {
    //     const response = await this
    //         ///._httpClient
    //         .put<ICreatePageRequest, AxiosResponse<any>>(
    //             `${process.env.VUE_APP_API_BASE_URL}/api/Pages/${request.id}`,
    //             request,
    //             this.headersWithFormDataContentType())
    //         .catch(function (error: AxiosError): AxiosResponse<SucceededOrNotResponse> {
    //             return error.response as AxiosResponse<SucceededOrNotResponse>
    //         })
    //     const succeededOrNotResponse = response.data as SucceededOrNotResponse
    //     return new SucceededOrNotResponse(succeededOrNotResponse.succeeded, succeededOrNotResponse.errors)
    // }
}