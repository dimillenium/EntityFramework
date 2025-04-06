using Domain.Common;

namespace Domain.Entities.Commandes;

public class LigneCommande : AuditableAndSoftDeletableEntity, ISanitizable
{
    public Guid CommandeId { get; set; }
    public Commande Commande { get; set; }
    public int BijouId { get; set; }
    public int Quantite { get; set; }
    public decimal PrixUnitaire { get; set; }
    public decimal TotalLigne { get; set; }

    public void SanitizeForSaving()
    {
        // Aucun nettoyage nécessaire pour l'instant.
    }
}

