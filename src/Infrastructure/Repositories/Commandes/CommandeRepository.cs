using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Slugify;

namespace Infrastructure.Repositories.Commandes;

public class CommandeRepository : ICommandeRepository
{
    private readonly ISlugHelper _slugHelper;
    private readonly EmNoJoyauxDbContext _context;

    public CommandeRepository(EmNoJoyauxDbContext context, ISlugHelper slugHelper)
    {
        _context = context;
        _slugHelper = slugHelper;
    }
    
    public List<Commande> LireTous()
    {
        return _context.Commandes
            .Include(c => c.LignesCommande)
            .AsNoTracking()
            .ToList();
    }
}