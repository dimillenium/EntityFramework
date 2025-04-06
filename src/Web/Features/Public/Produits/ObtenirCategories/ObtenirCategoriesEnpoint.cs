using Domain.Repositories;
using FastEndpoints;

namespace Web.Features.Public.Produits.ObtenirCategories;

public class ObtenirCategoriesEnpoint : EndpointWithoutRequest
{
    private readonly IProduitRepository _produitRepository;

    public ObtenirCategoriesEnpoint( IProduitRepository produitRepository)
    {
        _produitRepository = produitRepository;
    }

    public override void Configure()
    {
        DontCatchExceptions();
        Get("/produits/categories");
        AllowAnonymous();
   
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        // var categories = _produitRepository.ObtenirCategories();
        // await SendOkAsync(categories, ct);
    }
}