using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

// FRENCH
[Route("/connexion")]
[Route("/authentification-a-deux-facteurs")]
[Route("/mot-de-passe-oublie")]
[Route("/reinitialiser-mot-de-passe")]
[Route("/mon-compte")]
[Route("/administration/membres")]
[Route("/administration/membres/ajouter")]
[Route("/livres")]
[Route("/livres/ajouter")]
[Route("/boutique")]
[Route("/creer_page")]
[Route("/ajouter-produit")]
[Route("/categorie/:categorie")]
// ENGLISH
[Route("/login")]
// ENGLISH
[Route("/two-factor-authentication")]
[Route("/forgot-password")]
[Route("/reset-password")]
[Route("/my-account")]
[Route("/administration/members")]
[Route("/administration/members/add")]
[Route("/books")]
[Route("/books/add")]
[Route("/addpage")]
[Route("/shop")]
[Route("/add-product")]
[Route("/category/:category")]
public class IntranetController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}