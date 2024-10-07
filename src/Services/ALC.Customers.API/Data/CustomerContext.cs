using Microsoft.EntityFrameworkCore;
using ALC.Customers.API.Models;
using ALC.Core.Data;
using ALC.Core.Mediator;
using ALC.Core.DomainObjects;

namespace ALC.Customers.API.Data;

public sealed class CustomerContext : DbContext,  IUnitOfWork
{
    private readonly IMediatorHandler _mediatorHandler;
    public CustomerContext(DbContextOptions<CustomerContext> options, IMediatorHandler mediatorHandler) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
        _mediatorHandler = mediatorHandler;
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach(var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");
        
        foreach(var relationship in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerContext).Assembly);
    }

    public async Task<bool> Commit()
    {
        var success = await base.SaveChangesAsync() > 0;
        if (success) await _mediatorHandler.PublishEvents(this);
        
        return success;
    }
}

public static class MediatorExtension
{
    public static async Task PublishEvents<T>(this IMediatorHandler mediator, T context) where T : DbContext
    {
        var domainEntitiies = context.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any());

        var domainEvents = domainEntitiies
            .SelectMany(x => x.Entity.Notifications)
            .ToList();

        domainEntitiies.ToList()
            .ForEach(entity => entity.Entity.ClearEvents());

        var tasks = domainEvents
            .Select(async (domainEvent) => 
            {
                await mediator.PublishEvent(domainEvent);
            });
        
        await Task.WhenAll(tasks);
    }
}

