using Application.Interfaces.Services.Produits;
using Domain.Common;
using Domain.Entities.Produits;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using IMapper = AutoMapper.IMapper;

namespace Web.Features.Admins.Produits.CreateProduit;

public class CreateProduitEndpoint: Endpoint<CreateProduitRequest, SucceededOrNotResponse>
{
    private readonly IMapper _mapper;
    private readonly IProduitCreationService _produitCreationService;

    public CreateProduitEndpoint(IMapper mapper, IProduitCreationService produitCreationService)
    {
        _mapper = mapper;
        _produitCreationService = produitCreationService;
    }

    public override void Configure()
    {
        AllowFileUploads();
        DontCatchExceptions();

        Post("produits");
        Roles(Domain.Constants.User.Roles.ADMINISTRATOR);
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
    }

    public override async Task HandleAsync(CreateProduitRequest req, CancellationToken ct)
    {
         Console.WriteLine("📩 Requête reçue pour la création de produit");
         Console.WriteLine($"ID Produit : {req.IdProduit}");
         Console.WriteLine($"Description : {req.Description}");
         Console.WriteLine($"Prix : {req.Prix}");
         Console.WriteLine($"Quantité : {req.Quantite}");
         Console.WriteLine($"Nombre de photos : {(req.PhotoUrl != null ? req.PhotoUrl.Length : 0)}");
        
        var produit = _mapper.Map<Produit>(req);
        Console.WriteLine($"✅ Produit mappé : {produit.IdProduit} avec {produit.PhotoUrl.Length} photos.");
        
        await _produitCreationService.CreateProduit(produit);
        await SendOkAsync(new SucceededOrNotResponse(true), ct);
    }
}