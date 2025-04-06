using Domain.Repositories;
using FastEndpoints;

namespace Web.Features.Public.Produits.ObtenirCouleurs;

public class ObtenirCouleursEndpoint : EndpointWithoutRequest
{
    private readonly IProduitRepository _produitRepository;

    public ObtenirCouleursEndpoint( IProduitRepository produitRepository)
    {
        _produitRepository = produitRepository;
    }

    public override void Configure()
    {
        DontCatchExceptions();
        Get("/produits/couleurs");
        AllowAnonymous();
   
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var couleurs = _produitRepository.ObtenirCouleurs();

         await SendOkAsync(couleurs, ct);

        await SendOkAsync(couleurs, ct);

    } 
}