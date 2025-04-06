using Domain.Entities.Produits;

namespace Domain.Repositories;

public interface IProduitRepository
{
    List<Produit> ObtenirTous();
    Produit TrouverParId(string id);
    List<Produit> ObtenirParCategorie(string nom);
    List<String> ObtenirFormats();
    List<String> ObtenirCouleurs();
    List<String> ObtenirCategories();
    Task CreateProduit(Produit produit);

}