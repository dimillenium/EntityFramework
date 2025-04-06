using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Entities.Books;
using Domain.Entities.Pages;
using Domain.Entities.Commandes;
using Domain.Entities.Identity;
using Domain.Entities.Produits;
using Microsoft.AspNetCore.Identity;
using Web.Dtos;
using Web.Features.Members.Books;
using Web.Features.Admins.Pages;
using Web.Features.Admins.Commandes;
using GetMeMemberResponse = Web.Features.Members.Me.GetMe.GetMeResponse;
using GetMeAdminResponse = Web.Features.Admins.Me.GetMe.GetMeResponse;
using ProduitDto = Web.Features.Public.Produits.ProduitDto;

namespace Web.Mapping.Profiles;

public class ResponseMappingProfile : Profile
{
    public ResponseMappingProfile()
    {
        CreateMap<IdentityResult, SucceededOrNotResponse>();

        CreateMap<IdentityError, Error>()
            .ForMember(error => error.ErrorType, opt => opt.MapFrom(identity => identity.Code))
            .ForMember(error => error.ErrorMessage, opt => opt.MapFrom(identity => identity.Description));

        CreateMap<Member, GetMeMemberResponse>()
            .ForMember(x => x.Roles, opt => opt.MapFrom(x => x.User.RoleNames))
            .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(x => x.GetPhoneNumber()))
            .ForMember(x => x.PhoneExtension, opt => opt.MapFrom(x => x.GetPhoneExtension()));

        CreateMap<Administrator, GetMeAdminResponse>();

        CreateMap<Book, BookDto>()
            .ForMember(bookDto => bookDto.Created, opt => opt.MapFrom(book => book.Created.ToDateTimeUtc()))
            .ForMember(bookDto => bookDto.NameFr, opt => opt.MapFrom(book => book.NameFr))
            .ForMember(bookDto => bookDto.NameEn, opt => opt.MapFrom(book => book.NameEn))
            .ForMember(bookDto => bookDto.DescriptionFr, opt => opt.MapFrom(book => book.DescriptionFr))
            .ForMember(bookDto => bookDto.DescriptionEn, opt => opt.MapFrom(book => book.DescriptionEn));

        CreateMap<Produit, ProduitDto>()
            .ForMember(produitDto => produitDto.IdProduit, opt => opt.MapFrom(produit => produit.IdProduit))
            .ForMember(produitDto => produitDto.Description, opt => opt.MapFrom(produit => produit.Description))
            .ForMember(produitDto => produitDto.Prix, opt => opt.MapFrom(produit => produit.Prix))
            .ForMember(produitDto => produitDto.Categorie, opt => opt.MapFrom(produit => produit.Categorie))
            .ForMember(produitDto => produitDto.Format, opt => opt.MapFrom(produit => produit.Format))
            .ForMember(produitDto => produitDto.Couleur, opt => opt.MapFrom(produit => produit.Couleur))
            .ForMember(produitDto => produitDto.Quantite, opt => opt.MapFrom(produit => produit.Quantite))
            .ForMember(produitDto => produitDto.PhotoUrl, opt => opt.MapFrom(produit => produit.PhotoUrl));


        
        CreateMap<Page, PageDto>()
            .ForMember(PageDto  => PageDto.Slug, opt => opt.MapFrom(page => page.Slug))
            .ForMember(PageDto => PageDto.Background, opt => opt.MapFrom(page => page.Background))
            .ForMember(PageDto => PageDto.Section1, opt => opt.MapFrom(page => page.Section1))
            .ForMember(PageDto => PageDto.Section2, opt => opt.MapFrom(page => page.Section2));
          
        CreateMap<User, UserDto>()
            .ForMember(x => x.Roles, opt => opt.MapFrom(x => x.RoleNames))
            .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(x => x.PhoneNumber))
            .ForMember(x => x.PhoneExtension, opt => opt.MapFrom(x => x.PhoneExtension));

        CreateMap<Member, MemberDto>()
            .ForMember(x => x.Roles, opt => opt.MapFrom(x => x.User.RoleNames))
            .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(x => x.PhoneNumber!.Number))
            .ForMember(x => x.PhoneExtension, opt => opt.MapFrom(x => x.PhoneNumber!.Extension));

        CreateMap<Commande, CommandeDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.DateCommande, opt => opt.MapFrom(src => src.DateCommande))
            .ForMember(dest => dest.NumeroCommande, opt => opt.MapFrom(src => src.NumeroCommande))
            .ForMember(dest => dest.MoyenPaiement, opt => opt.MapFrom(src => src.MoyenPaiement.ToString()))
            .ForMember(dest => dest.OptionLivraison, opt => opt.MapFrom(src => src.OptionLivraison.ToString()))
            .ForMember(dest => dest.NumeroSuivi, opt => opt.MapFrom(src => src.NumeroSuivi))
            .ForMember(dest => dest.EmailClient, opt => opt.MapFrom(src => src.EmailClient))
            .ForMember(dest => dest.AdresseLivraison, opt => opt.MapFrom(src => src.AdresseLivraison))
            .ForMember(dest => dest.MontantTotal, opt => opt.MapFrom(src => src.MontantTotal))
            .ForMember(dest => dest.LignesCommande, opt => opt.MapFrom(src => src.LignesCommande));

        CreateMap<LigneCommande, LigneCommandeDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CommandeId, opt => opt.MapFrom(src => src.CommandeId))
            .ForMember(dest => dest.BijouId, opt => opt.MapFrom(src => src.BijouId))
            .ForMember(dest => dest.Quantite, opt => opt.MapFrom(src => src.Quantite))
            .ForMember(dest => dest.PrixUnitaire, opt => opt.MapFrom(src => src.PrixUnitaire))
            .ForMember(dest => dest.TotalLigne, opt => opt.MapFrom(src => src.TotalLigne));
    }
}