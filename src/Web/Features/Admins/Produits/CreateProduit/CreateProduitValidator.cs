using FastEndpoints;
using FluentValidation;
using Web.Extensions;

namespace Web.Features.Admins.Produits.CreateProduit;

public class CreateProduitValidator:  Validator<CreateProduitRequest>
{
    public CreateProduitValidator()
    {
        RuleFor(x => x.IdProduit)
            .NotNull()
            .NotEmpty()
            .WithErrorCode("InvalidIdProduit")
            .WithMessage("Le numéro de modèle du bijou (IdProduit) ne doit pas être vide.");

        RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty()
            .WithErrorCode("InvalidDescription")
            .WithMessage("La description du produit ne doit pas être vide.");

        RuleFor(x => x.Prix)
            .NotNull()
            .GreaterThan(0)
            .WithErrorCode("InvalidPrix")
            .WithMessage("Le prix du produit doit être un nombre positif.");

        RuleFor(x => x.Quantite)
            .NotNull()
            .GreaterThanOrEqualTo((byte)1)
            .WithErrorCode("InvalidQuantite")
            .WithMessage("La quantité doit être au moins 1.");

        RuleFor(x => x.Categorie)
            .NotNull()
            .NotEmpty()
            .WithErrorCode("InvalidCategorie")
            .WithMessage("La catégorie du produit ne doit pas être vide.");

        RuleFor(x => x.Format)
            .NotNull()
            .NotEmpty()
            .WithErrorCode("InvalidFormat")
            .WithMessage("Le format du produit ne doit pas être vide.");

        RuleFor(x => x.Couleur)
            .NotNull()
            .NotEmpty()
            .WithErrorCode("InvalidCouleur")
            .WithMessage("La couleur du produit ne doit pas être vide.");

        RuleFor(x => x.PhotoUrl)
            .Must(photoUrls => photoUrls == null || photoUrls.All(url => Uri.IsWellFormedUriString(url.ToString(), UriKind.RelativeOrAbsolute)))
            .WithErrorCode("InvalidPhotoUrl")
            .WithMessage("Chaque URL de photo doit être un lien valide.");
    }
}