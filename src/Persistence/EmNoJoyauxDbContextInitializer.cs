using Domain.Constants.User;
using Domain.Entities;
using Domain.Entities.Commandes;
using Domain.Entities.Identity;
using Domain.Entities.Produits;
using Domain.Entities.Pages;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Persistence;

public class EmNoJoyauxDbContextInitializer
{
    private const string MemberEmail = "member@gmail.com";
    private const string AdminEmail = "admin@gmail.com";
    private const string Password = "Qwerty123!";

    private readonly ILogger<EmNoJoyauxDbContextInitializer> _logger;
    private readonly EmNoJoyauxDbContext _context;
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;

    public EmNoJoyauxDbContextInitializer(ILogger<EmNoJoyauxDbContextInitializer> logger,
        EmNoJoyauxDbContext context,
        RoleManager<Role> roleManager,
        UserManager<User> userManager)
    {
        _logger = logger;
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await SeedRoles();
            await SeedAdmins();
            await SeedMembers();
            await SeedCommandes();
            await SeedProduits();
            await SeedPages();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }




    private async Task SeedProduits()
    {

        List<Produit> seedProduits = new List<Produit>();
        seedProduits.Add(new Produit
        {
            IdProduit = "BBl-0001",
            Description = "Fleur bleu pâle",
            Format = "bouton",
            Categorie = "boucle d'oreille",
            Couleur = "bleu",
            Prix = 7,
            Quantite = 1,
            PhotoUrl = ["https://i.ibb.co/8Lhg48TH/BBl-0001.png"]
        });
        seedProduits.Add(new Produit
        {
            IdProduit = "BG-0008",
            Description = "Triangle gris oréo",
            Format = "bouton",
            Categorie = "boucle d'oreille",
            Couleur = "gris",
            Prix = 7,
            Quantite = 1,
            PhotoUrl = ["https://i.ibb.co/HfQNBZn1/BG-0008.png"]
        });
        seedProduits.Add(new Produit
        {
            IdProduit = "CX-0001",
            Description = "Rectangle vert corail lilas",
            Format = "bouton",
            Categorie = "barrette",
            Couleur = "vert",
            Prix = 7,
            Quantite = 1,
            PhotoUrl = ["https://i.ibb.co/xbXcvZ1/CX-0001.png"]
        });
        seedProduits.Add(new Produit
        {
            IdProduit = "XG-0001",
            Description = "Triangle gris et mauve",
            Format = "Très Grosse",
            Categorie = "boucle d'oreille",
            Couleur = "mix",
            Prix = 7,
            Quantite = 1,
            PhotoUrl = ["https://i.ibb.co/v6LKC6qk/XG-0001.png"]
        });
        seedProduits.Add(new Produit
        {
            IdProduit = "XR-0001",
            Description = "Rond rouge ligne",
            Format = "Très Grosse",
            Categorie = "boucle d'oreille",
            Couleur = "rouge",
            Prix = 7,
            Quantite = 1,
            PhotoUrl = ["https://i.ibb.co/GQ1LWtZz/XR-0001.png"]
        });
        seedProduits.Add(new Produit
        {
            IdProduit = "XV-0003",
            Description = "Arche sauge",
            Format = "Très Grosse",
            Categorie = "boucle d'oreille",
            Couleur = "bleu",
            Prix = 7,
            Quantite = 1,
            PhotoUrl = ["https://i.ibb.co/wFrY8CJz/XV-0003.png"]
        });
        seedProduits.Add(new Produit
        {
            IdProduit = "LM-0003",
            Description = "Trapèze mix et rond mauve",
            Format = "Large",
            Categorie = "boucle d'oreille",
            Couleur = "mauve",
            Prix = 15,
            Quantite = 1,
            PhotoUrl = ["https://i.ibb.co/wFrY8CJz/LM-0003.png"]
        });
        seedProduits.Add(new Produit
        {
            IdProduit = "MF-0001",
            Description = "Trapèze fuschia fleur",
            Format = "Médium",
            Categorie = "boucle d'oreille",
            Couleur = "Fuschias",
            Prix = 10,
            Quantite = 1,
            PhotoUrl = ["https://i.ibb.co/wFrY8CJz/MF-0001.png"]
        });
        seedProduits.Add(new Produit
        {
            IdProduit = "PP-0002",
            Description = "rond vieux rose F",
            Format = "",
            Categorie = "Pendentif",
            Couleur = "rose",
            Prix = 15,
            Quantite = 1,
            PhotoUrl = ["https://i.ibb.co/wFrY8CJz/PP-0002.png"]
        });

        var couleurProduits = _context.Produits.IgnoreQueryFilters()
            .FirstOrDefault(x => x.IdProduit == seedProduits[0].IdProduit);
        if (couleurProduits != null)
            return;


        _context.Produits.AddRange(seedProduits);
        await _context.SaveChangesAsync();
    }

    private async Task SeedRoles()
    {
        if (!_roleManager.RoleExistsAsync(Roles.ADMINISTRATOR).Result)
            await _roleManager.CreateAsync(new Role { Name = Roles.ADMINISTRATOR, NormalizedName = Roles.ADMINISTRATOR.Normalize() });

        if (!_roleManager.RoleExistsAsync(Roles.MEMBER).Result)
            await _roleManager.CreateAsync(new Role { Name = Roles.MEMBER, NormalizedName = Roles.MEMBER.Normalize() });
    }

    private async Task SeedAdmins()
    {
        var user = await _userManager.FindByEmailAsync(AdminEmail);
        if (user != null)
            return;

        user = BuildUser(AdminEmail);
        var result = await _userManager.CreateAsync(user, Password);

        if (result.Succeeded)
            await _userManager.AddToRoleAsync(user, Roles.ADMINISTRATOR);
        else
            throw new Exception($"Could not seed/create {Roles.ADMINISTRATOR} user.");


        var admin = new Administrator("Super", "Admin");
        admin.SetUser(user);
        _context.Administrators.Add(admin);
        await _context.SaveChangesAsync();
    }

    private async Task SeedMembers()
    {
        var user = await _userManager.FindByEmailAsync(MemberEmail);
        if (user != null)
            return;

        user = BuildUser(MemberEmail);
        var result = await _userManager.CreateAsync(user, Password);

        if (result.Succeeded)
            await _userManager.AddToRoleAsync(user, Roles.MEMBER);
        else
            throw new Exception($"Could not seed/create {Roles.MEMBER} user.");

        var existingMember = _context.Members.IgnoreQueryFilters().FirstOrDefault(x => x.User.Id == user.Id);
        if (existingMember is { Active: true })
            return;

        if (existingMember == null)
        {
            var member = new Member("John", "Doe", 1, "123, my street", "Quebec", "A1A 1A1");
            member.SetUser(user);
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
        }
        else if (!existingMember.Active)
        {
            existingMember.Activate();
            _context.Members.Update(existingMember);
            await _context.SaveChangesAsync();
        }
    }
    
    private async Task SeedPages()
    {
        List<Page> seedPages = new List<Page>
        {
            new Page
            {
                Slug = "poulet",
                Background = "image_poulet.jpg",
                Section1 = "Description du poulet",
                Section2 = "Recettes à base de poulet"
            },
            new Page
            {
                Slug = "patate",
                Background = "image_patate.jpg",
                Section1 = "Description de la patate",
                Section2 = "Recettes à base de patate"
            },
            new Page
            {
                Slug = "pomme",
                Background = "image_pomme.jpg",
                Section1 = "Description de la pomme",
                Section2 = "Recettes à base de pomme"
            }
        };

        foreach (var page in seedPages)
        {
            var existingPage = await _context.Pages.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Slug == page.Slug);
            if (existingPage == null)
            {
                _context.Pages.Add(page);
            }
        }
    
        await _context.SaveChangesAsync();
    }


     
    
    private async Task SeedCommandes()
{
    if (_context.Commandes.Any())
        return;

    var random = new Random();
    var commandes = new List<Commande>();

    for (int i = 1; i <= 15; i++)
    {
        int nombreLignes = random.Next(1, 4); // 1 à 3 lignes par commande
        var commande = new Commande
        {
            DateCommande = DateTime.Now.AddDays(-i),
            NumeroCommande = $"CMD{i:000}",
            MoyenPaiement = (i % 2 == 0) ? MoyenPaiement.Interact : MoyenPaiement.Paypal,
            OptionLivraison = (i % 3 == 0)
                ? OptionLivraison.PosteCanada
                : ((i % 3 == 1) ? OptionLivraison.RetraitChezLaVendeuse : OptionLivraison.LivraisonParLaVendeuse),
            NumeroSuivi = (i % 3 == 0) ? $"SUIVI{i:000}" : "N/A",
            EmailClient = $"client{i}@exemple.com",
            AdresseLivraison = $"123 rue Exemple, Ville{i}",
            LignesCommande = new List<LigneCommande>()
        };

        for (int j = 1; j <= nombreLignes; j++)
        {
            int bijouId = random.Next(1, 5); // Exemple : bijouId entre 1 et 4
            int quantite = random.Next(1, 5); // Quantité entre 1 et 4
            decimal prixUnitaire = random.Next(50, 151); // Prix unitaire entre 50 et 150
            decimal totalLigne = quantite * prixUnitaire;

            // Ajoutez la ligne de commande sans renseigner explicitement la clé étrangère.
            // EF renseignera automatiquement CommandeId lors de l'insertion.
            commande.LignesCommande.Add(new LigneCommande
            {
                BijouId = bijouId,
                Quantite = quantite,
                PrixUnitaire = prixUnitaire,
                TotalLigne = totalLigne
            });
        }

        // Calcul du montant total en sommant le total de chaque ligne.
        commande.MontantTotal = commande.LignesCommande.Sum(lc => lc.TotalLigne);

        // Appliquer le nettoyage des chaînes, si nécessaire.
        commande.SanitizeForSaving();

        commandes.Add(commande);
    }

    _context.Commandes.AddRange(commandes);
    await _context.SaveChangesAsync();
}




    private User BuildUser(string email)
    {
        return new User
        {
            Email = email,
            UserName = email,
            NormalizedEmail = email.Normalize(),
            NormalizedUserName = email,
            PhoneNumber = "555-555-5555",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            TwoFactorEnabled = false
        };
    }
}