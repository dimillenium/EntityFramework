namespace Web.Features.Admins.Commandes;

public class CommandeDto
{
    public Guid Id { get; set; }
    public DateTime DateCommande { get; set; }
    public string NumeroCommande { get; set; } = default!;
    public string MoyenPaiement { get; set; } = default!;
    public string OptionLivraison { get; set; } = default!;
    public string NumeroSuivi { get; set; } = default!;
    public string EmailClient { get; set; } = default!;
    public string AdresseLivraison { get; set; } = default!;
    public decimal MontantTotal { get; set; }
    public List<LigneCommandeDto> LignesCommande { get; set; } = new();
}