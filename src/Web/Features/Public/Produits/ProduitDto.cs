namespace Web.Features.Public.Produits;

public class ProduitDto
{
    public  string IdProduit { get;  set; } = default!;
   
    public string Description { get;   set; } = default!;
   
    public decimal Prix { get;   set; } = default!;
   
    public string Categorie { get;   set; } = default!;
   
    public string Format { get;   set; } = default!;
   
    public string Couleur { get;  set; } = default!;
   
    public Byte Quantite { get;   set; } = default!;
   
    public string[] PhotoUrl { get;   set; } = [];
}