import {IBookService} from "@/injection/interfaces";
import {injectable} from "inversify";
import {ApiService} from "./apiService";
import {SucceededOrNotResponse} from "@/types/responses";
import {AxiosError, AxiosResponse} from "axios";
import {ICreateBookRequest} from "@/types/requests/createBookRequest";
import {Book} from "@/types/entities";
import {IEditBookRequest} from "@/types/requests/editBookRequest";

@injectable()
export class BookService extends ApiService implements IBookService {
  public async getAllBooks(): Promise<Book[]> {
    const response = await this
      ._httpClient
      .get<AxiosResponse<Book[]>>(`${process.env.VUE_APP_API_BASE_URL}/api/books`)
      .catch(function (error: AxiosError): AxiosResponse<Book[]> {
        return error.response as AxiosResponse<Book[]>
      })
    return response.data as Book[]
  }

  public async getBook(bookId: string): Promise<Book> {
    const response = await this
      ._httpClient
      .get<AxiosResponse<Book>>(`${process.env.VUE_APP_API_BASE_URL}/api/books/${bookId}`)
      .catch(function (error: AxiosError): AxiosResponse<Book> {
        return error.response as AxiosResponse<Book>;
      });
    return response.data as Book
  }

  public async deleteBook(bookId: string): Promise<SucceededOrNotResponse> {
    const response = await this
      ._httpClient
      .delete<AxiosResponse<any>>(`${process.env.VUE_APP_API_BASE_URL}/api/books/${bookId}`)
      .catch(function (error: AxiosError): AxiosResponse<SucceededOrNotResponse> {
        return error.response as AxiosResponse<SucceededOrNotResponse>
      })
    return new SucceededOrNotResponse(response.status === 204)
  }

  public async createBook(request: ICreateBookRequest): Promise<SucceededOrNotResponse> {
    const response = await this
      ._httpClient
      .post<ICreateBookRequest, AxiosResponse<any>>(
        `${process.env.VUE_APP_API_BASE_URL}/api/books`,
        request,
        this.headersWithFormDataContentType())
      .catch(function (error: AxiosError): AxiosResponse<SucceededOrNotResponse> {
        return error.response as AxiosResponse<SucceededOrNotResponse>
      })
    const succeededOrNotResponse = response.data as SucceededOrNotResponse
    return new SucceededOrNotResponse(succeededOrNotResponse.succeeded, succeededOrNotResponse.errors)
  }

  public async editBook(request: IEditBookRequest): Promise<SucceededOrNotResponse> {
    const response = await this
      ._httpClient
      .put<ICreateBookRequest, AxiosResponse<any>>(
        `${process.env.VUE_APP_API_BASE_URL}/api/books/${request.id}`,
        request,
        this.headersWithFormDataContentType())
      .catch(function (error: AxiosError): AxiosResponse<SucceededOrNotResponse> {
        return error.response as AxiosResponse<SucceededOrNotResponse>
      })
    const succeededOrNotResponse = response.data as SucceededOrNotResponse
    return new SucceededOrNotResponse(succeededOrNotResponse.succeeded, succeededOrNotResponse.errors)
  }
}