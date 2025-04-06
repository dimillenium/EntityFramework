using Domain.Entities.Produits;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Services.Produits;

public interface IProduitCreationService
{
    Task CreateProduit(Produit produit);
}