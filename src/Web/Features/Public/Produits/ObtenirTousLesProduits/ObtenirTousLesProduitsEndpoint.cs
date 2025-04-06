using Domain.Repositories;
using FastEndpoints;
using Web.Dtos;

namespace Web.Features.Public.Produits.ObtenirTousLesProduits;

using IMapper = AutoMapper.IMapper;


public class ObtenirTousLesProduitsEndpoint : EndpointWithoutRequest<List<ProduitDto>>
{
    private readonly IMapper _mapper;
    private readonly IProduitRepository _produitRepository;

    public ObtenirTousLesProduitsEndpoint(IMapper mapper, IProduitRepository produitRepository)
    {
        _mapper = mapper;
        _produitRepository = produitRepository;
    }

    public override void Configure()
    {
        DontCatchExceptions();
        
        Get("produits");
        AllowAnonymous();
        
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var lesProduits = _produitRepository.ObtenirTous();
        
        await SendOkAsync(_mapper.Map<List<ProduitDto>>(lesProduits), ct);
    }
}