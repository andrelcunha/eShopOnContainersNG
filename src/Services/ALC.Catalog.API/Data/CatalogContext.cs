using FluentValidation.Results;
using ALC.Catalog.API.Models;
using ALC.Core.Data;
using Microsoft.EntityFrameworkCore;
using ALC.Core.Messages;

namespace ALC.Catalog.API.Data
{
    public class CatalogContext : DbContext, IUnitOfWork
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options) { }
            public DbSet<Product> Products {get;set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            foreach(var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");
        
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
