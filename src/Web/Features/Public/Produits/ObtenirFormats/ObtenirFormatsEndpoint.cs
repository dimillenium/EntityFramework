using FastEndpoints;
using Domain.Repositories;
namespace Web.Features.Public.Produits.ObtenirFormats;


public class ObtenirFormatsEndpoint : EndpointWithoutRequest
{
    private readonly IProduitRepository _produitRepository;

    public ObtenirFormatsEndpoint( IProduitRepository produitRepository)
    {
        _produitRepository = produitRepository;
    }

    public override void Configure()
    {
        DontCatchExceptions();
        Get("/produits/formats");
        AllowAnonymous();
   
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        // var formats = _produitRepository.ObtenirFormats();
        // await SendOkAsync(formats, ct);
    }
}