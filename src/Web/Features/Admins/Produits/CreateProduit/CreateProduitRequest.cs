using Microsoft.EntityFrameworkCore;

namespace Web.Features.Admins.Produits.CreateProduit;

public class CreateProduitRequest
{
    public string IdProduit { get; set; } = default!; 

    public string Description { get; set; } = default!; 

    [Precision(18, 2)]
    public decimal Prix { get; set; } 

    public string Categorie { get; set; } = default!; 

    public string Format { get; set; } = default!;

    public string Couleur { get; set; } = default!; 

    public byte Quantite { get; set; } 

    public string[] PhotoUrl { get; set; } = Array.Empty<string>();
}