using Application.Exceptions.Produits;
using Domain.Repositories;
using Domain.Entities.Produits;
using Domain.Repositories;
using Infrastructure.Repositories.Produits;
using Microsoft.EntityFrameworkCore;
using Persistence;

    

namespace Infrastructure.Repositories.Produits;

public class ProduitRepository : IProduitRepository
{
    private readonly EmNoJoyauxDbContext _context;

    public ProduitRepository(EmNoJoyauxDbContext context)
    {
        _context = context;
    }
    
    public List<Produit> ObtenirTous() => _context.Produits.ToList();

    public Produit TrouverParId(String id)
    {
        var produit = _context.Produits
            .AsNoTracking()
            .FirstOrDefault(x => x.IdProduit == id);
        if (produit == null)
            throw new ProduitNonTrouveException($"Le produit {id} n'existe pas");
        return produit;
    }
    
    public async Task CreateProduit(Produit produit)
    {
        _context.Produits.Add(produit);
        await _context.SaveChangesAsync();
    }

    public List<Produit> ObtenirParCategorie(string categorie)
    {
        List<Produit> produits = _context.Produits.AsNoTracking().ToList().Where(x => x.Categorie.Equals(categorie)).ToList();
        return produits;
    }
 
    public List<String> ObtenirFormats() => _context.Produits.Select(p => p.Couleur).Distinct().ToList();
   
   
    public List<String> ObtenirCategories() => _context.Produits.Select(p => p.Categorie).Distinct().ToList();


    public List<String> ObtenirCouleurs() => _context.Produits.Select(p => p.Couleur).Distinct().ToList();
    
}