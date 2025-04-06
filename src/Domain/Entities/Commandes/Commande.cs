using System;
using System.Collections.Generic;
using Domain.Common;
using Domain.Entities;
using Domain.Entities.Commandes;

public enum MoyenPaiement
{
    Paypal,
    Interact
}

public enum OptionLivraison
{
    PosteCanada,
    RetraitChezLaVendeuse,
    LivraisonParLaVendeuse
}

public class Commande : AuditableAndSoftDeletableEntity, ISanitizable
{
    public DateTime DateCommande { get; set; } = DateTime.Now;
    public string NumeroCommande { get; set; }
    public MoyenPaiement MoyenPaiement { get; set; }
    public OptionLivraison OptionLivraison { get; set; }
    public string NumeroSuivi { get; set; }
    public string EmailClient { get; set; }
    public string AdresseLivraison { get; set; }
    public decimal MontantTotal { get; set; }
    public List<LigneCommande> LignesCommande { get; set; } = new List<LigneCommande>();

    public void SanitizeForSaving()
    {
        NumeroCommande = NumeroCommande?.Trim();
        EmailClient = EmailClient?.Trim();
        NumeroSuivi = NumeroSuivi?.Trim();
        AdresseLivraison = AdresseLivraison?.Trim();
    }
}