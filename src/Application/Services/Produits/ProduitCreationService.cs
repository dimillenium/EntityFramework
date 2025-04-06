using Application.Interfaces.FileStorage;
using Application.Interfaces.Services.Produits;
using Domain.Entities.Produits;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Produits;

public class ProduitCreationService : IProduitCreationService
{  
    private readonly IProduitRepository _produitRepository;
    private readonly IFileStorageApiConsumer _fileStorageApiConsumer;

    public ProduitCreationService(
        IProduitRepository produitRepository,
        IFileStorageApiConsumer fileStorageApiConsumer)
    {
        _produitRepository = produitRepository;
        _fileStorageApiConsumer = fileStorageApiConsumer;
    }
    public async Task CreateProduit(Produit produit)
    {
        await _produitRepository.CreateProduit(produit);
    }
}