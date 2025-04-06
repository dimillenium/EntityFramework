// eslint-disable-next-line @typescript-eslint/no-empty-interface
import {
    ICreateBookRequest,
    IEditBookRequest,
    IForgotPasswordRequest,
    ILoginRequest,
    IResetPasswordRequest,
    ITwoFactorRequest,
    ICreatePageRequest,
    ICreateProduitRequest,
} from "@/types/requests"
import {PaginatedResponse, SucceededOrNotResponse} from "@/types/responses"
import {Administrator, Book, Member, User, Commande, Page, Produit} from "@/types/entities"
import {Guid} from "@/types";

export interface IApiService {
    headersWithJsonContentType(): any

    headersWithFormDataContentType(): any

    buildEmptyBody(): string
}

export interface IAdministratorService {
    getAuthenticated(): Promise<Administrator | undefined>
}


export interface IAuthenticationService {
    login(request: ILoginRequest): Promise<SucceededOrNotResponse>

    twoFactor(request: ITwoFactorRequest): Promise<SucceededOrNotResponse>

    forgotPassword(request: IForgotPasswordRequest): Promise<SucceededOrNotResponse>

    resetPassword(request: IResetPasswordRequest): Promise<SucceededOrNotResponse>

    logout(): Promise<SucceededOrNotResponse>
}

export interface IMemberService {

    getAuthenticated(): Promise<Member | undefined>

    search(pageIndex: number, pageSize: number, searchValue: string): Promise<PaginatedResponse<Member>>

    getMember(id: string): Promise<Member>

    createMember(member: Member): Promise<SucceededOrNotResponse>

    updateMember(member: Member): Promise<SucceededOrNotResponse>

    deleteMember(id: Guid): Promise<SucceededOrNotResponse>
}

export interface IProduitService {
    obtenirTousLesProduits(): Promise<Produit[]>

    obtenirProduit(idProduit: string): Promise<Produit>
    
    createProduit(request: ICreateProduitRequest): Promise<SucceededOrNotResponse>

    obtenirTousLesProduits(qt: number): Promise<Produit[]>

    obtenirCouleurs(): Promise< string[]>
}

export interface IBookService {
    getAllBooks(): Promise<Book[]>

    getBook(bookId: string): Promise<Book>

    deleteBook(bookId: string): Promise<SucceededOrNotResponse>

    createBook(request: ICreateBookRequest): Promise<SucceededOrNotResponse>

    editBook(request: IEditBookRequest): Promise<SucceededOrNotResponse>
}

export interface IPageService {
    getAllPages(): Promise<Page[]>

    getPage(pageId: string): Promise<Page>

  deletePage(pageId: string): Promise<SucceededOrNotResponse>

  createPage(request: ICreatePageRequest): Promise<SucceededOrNotResponse>

  // editPage(request: IEditPageRequest): Promise<SucceededOrNotResponse>
}

export interface IUserService {
    getCurrentUser(): Promise<User>
}

//-----------------------------------
// Nouveau service pour Commande
//-----------------------------------
export interface ICommandeService {
  // Récupère toutes les commandes (optionnel : paginé ou non)
  LireTousCommandes(): Promise<Commande[]>
}
