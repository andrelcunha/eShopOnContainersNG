using Microsoft.EntityFrameworkCore;
using ALC.Clients.API.Models;
using ALC.Core.Data;

namespace ALC.Clients.API.Data;

public sealed class ClientContext : DbContext,  IUnitOfWork
{
    public ClientContext(DbContextOptions<ClientContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach(var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");
        
        foreach(var relationship in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientContext).Assembly);
    }

    public async Task<bool> Commit()
    {
        var sucesso = await base.SaveChangesAsync() > 0;
        
        return sucesso;
    }
}
