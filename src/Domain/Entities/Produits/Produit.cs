using Domain.Common;

namespace Domain.Entities.Produits;

public class Produit : AuditableAndSoftDeletableEntity ,ISanitizable
{
   public  string IdProduit { get;  set; } = default!;
   
   public string Description { get;   set; } = default!;
   
   public decimal Prix { get;   set; } = default!;
   
   public string Categorie { get;   set; } = default!;
   
   public string Format { get;   set; } = default!;
   
   public string Couleur { get;  set; } = default!;
   
   public Byte Quantite { get;   set; } = default!;
   
   public string[] PhotoUrl { get;   set; } = [];
   

   public void SetPhotoUrl(string[] photoUrl)
   {
      PhotoUrl = photoUrl;
   }
   
   public void SanitizeForSaving()
   {
      IdProduit = IdProduit.Trim();
      Description = Description.Trim();
      
   }
}