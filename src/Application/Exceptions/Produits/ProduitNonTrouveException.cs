namespace Application.Exceptions.Produits;

public class ProduitNonTrouveException: Exception
{
    public ProduitNonTrouveException(string message): base(message) { }
}