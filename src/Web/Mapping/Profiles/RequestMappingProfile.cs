using Application.Services.Members.Dtos;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Entities.Books;
using Domain.Entities.Pages;
using Domain.Entities.Produits;
using Domain.Entities.Identity;
using Web.Dtos;
using Web.Features.Admins.Members.CreateMember;
using Web.Features.Admins.Members.UpdateMember;
using Web.Features.Members.Books.CreateBook;
using Web.Features.Admins.Pages.CreatePage;
using Web.Features.Admins.Produits.CreateProduit;
using Web.Features.Members.Books.EditBook;

namespace Web.Mapping.Profiles;

public class RequestMappingProfile : Profile
{
    public RequestMappingProfile()
    {
        CreateMap<TranslatableStringDto, TranslatableString>().ReverseMap();

        CreateMap<CreateBookRequest, Book>();
        
        CreateMap<CreateProduitRequest, Produit>()
            .ForMember(dest => dest.IdProduit, opt => opt.MapFrom(src => src.IdProduit))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Prix, opt => opt.MapFrom(src => src.Prix))
            .ForMember(dest => dest.Categorie, opt => opt.MapFrom(src => src.Categorie))
            .ForMember(dest => dest.Format, opt => opt.MapFrom(src => src.Format))
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.PhotoUrl)); 
    
        
        CreateMap<CreatePageRequest, Page>()
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Slug))
            .ForMember(dest => dest.Background, opt => opt.MapFrom(src => src.Background))
            .ForMember(dest => dest.Section1, opt => opt.MapFrom(src => src.Section1))
            .ForMember(dest => dest.Section2, opt => opt.MapFrom(src => src.Section2));

        CreateMap<EditBookRequest, Book>();

        CreateMap<CreateMemberRequest, MemberRegistrationDto>();

        CreateMap<UpdateMemberRequest, User>()
            .ForMember(user => user.Id, opt => opt.Ignore())
            .ForMember(user => user.Email, opt => opt.MapFrom(request => request.Email.ToLower()))
            .ForMember(user => user.UserName, opt => opt.MapFrom(request => request.Email.ToLower()))
            .ForMember(user => user.NormalizedEmail, opt => opt.MapFrom(request => request.Email.ToUpper()))
            .ForMember(user => user.NormalizedUserName, opt => opt.MapFrom(request => request.Email.ToUpper()));
        CreateMap<UpdateMemberRequest, Member>();
    }
}