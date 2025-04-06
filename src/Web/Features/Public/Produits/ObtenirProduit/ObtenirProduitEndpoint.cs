using Domain.Repositories;
using FastEndpoints;
namespace Web.Features.Public.Produits.ObtenirProduit;

using IMapper = AutoMapper.IMapper;


public class ObtenirProduitEndpoint : Endpoint<ObtenirProduitRequest, ProduitDto>
{
    private readonly IMapper _mapper;
    private readonly IProduitRepository _produitRepository;

    public ObtenirProduitEndpoint(IMapper mapper, IProduitRepository produitRepository)
    {
        _mapper = mapper;
        _produitRepository = produitRepository;
    }

    public override void Configure()
    {
        DontCatchExceptions();
        Get("produit/{idProduit}");
        AllowAnonymous();
   
    }

    public override async Task HandleAsync(ObtenirProduitRequest request, CancellationToken ct)
    {
        var produit = _produitRepository.TrouverParId(request.IdProduit);
        await SendOkAsync(_mapper.Map<ProduitDto>(produit), ct);
        
    }
}