namespace Web.Features.Admins.Commandes;

public class LigneCommandeDto
{
    public Guid Id { get; set; }
    public Guid CommandeId { get; set; }
    public int BijouId { get; set; }
    public int Quantite { get; set; }
    public decimal PrixUnitaire { get; set; }
    public decimal TotalLigne { get; set; }
}