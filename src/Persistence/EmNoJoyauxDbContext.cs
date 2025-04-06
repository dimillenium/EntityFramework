using System.Reflection;
using Domain.Common;
using Domain.Entities;
using Domain.Entities.Authentication;
using Domain.Entities.Books;
using Domain.Entities.Commandes;
using Domain.Entities.Identity;
using Domain.Entities.Produits;
using Domain.Entities.Pages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Extensions;
using Persistence.Interceptors;

namespace Persistence;

public class EmNoJoyauxDbContext : IdentityDbContext<User, Role, Guid,
    IdentityUserClaim<Guid>, UserRole,
    IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    private readonly AuditableAndSoftDeletableEntitySaveChangesInterceptor
        _auditableAndSoftDeletableEntitySaveChangesInterceptor = null!;

    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor = null!;
    private readonly UserSaveChangesInterceptor _userSaveChangesInterceptor = null!;
    private readonly EntitySaveChangesInterceptor _entitySaveChangesInterceptor = null!;

    public EmNoJoyauxDbContext(
        DbContextOptions<EmNoJoyauxDbContext> options,
        AuditableAndSoftDeletableEntitySaveChangesInterceptor auditableAndSoftDeletableEntitySaveChangesInterceptor,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor,
        UserSaveChangesInterceptor userSaveChangesInterceptor,
        EntitySaveChangesInterceptor entitySaveChangesInterceptor)
        : base(options)
    {
        _auditableAndSoftDeletableEntitySaveChangesInterceptor = auditableAndSoftDeletableEntitySaveChangesInterceptor;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        _userSaveChangesInterceptor = userSaveChangesInterceptor;
        _entitySaveChangesInterceptor = entitySaveChangesInterceptor;
    }

    public DbSet<Administrator> Administrators { get; set; } = null!;
    public DbSet<Member> Members { get; set; } = null!;
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Produit> Produits {get; set;  }  = null!;

    public DbSet<Page> Pages { get; set; } = null!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
    
    public DbSet<Commande> Commandes { get; set; } = null!;
    public DbSet<LigneCommande> LignesCommande { get; set; } = null!;

    public EmNoJoyauxDbContext()
    {
    }

    public EmNoJoyauxDbContext(DbContextOptions<EmNoJoyauxDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Global query to prevent loading soft-deleted entities
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (!typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
                continue;

            if (entityType.ClrType == typeof(User))
                continue;

            entityType.AddSoftDeleteQueryFilter();
        }

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(
            _auditableAndSoftDeletableEntitySaveChangesInterceptor,
            _auditableEntitySaveChangesInterceptor,
            _userSaveChangesInterceptor,
            _entitySaveChangesInterceptor);
    }

    public async Task<int> SaveChangesAsync(CancellationToken? cancellationToken = null)
    {
        return await base.SaveChangesAsync(cancellationToken ?? CancellationToken.None);
    }
}