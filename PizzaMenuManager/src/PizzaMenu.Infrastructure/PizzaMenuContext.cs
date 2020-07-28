using Microsoft.EntityFrameworkCore;
using PizzaMenu.Domain.Entities;
using PizzaMenu.Domain.Repositories;
using PizzaMenu.Infrastructure.SchemaDefinitions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PizzaMenu.Infrastructure
{
  public class PizzaMenuContext : DbContext, IUnitOfWork
  {
    public const string DEFAULT_SCHEMA = "menu";

    public DbSet<Pizza> Pizzas { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<PizzaIngredient> PizzaIngredients { get; set; }

    public PizzaMenuContext( DbContextOptions<PizzaMenuContext> options ) : base( options )
    {
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
      modelBuilder.ApplyConfiguration( new PizzaEntitySchemaDefinition() );
      modelBuilder.ApplyConfiguration( new IngredientEntitySchemaDefinition() );
      modelBuilder.ApplyConfiguration( new PizzaIngredientEntitySchemaDefinition() );
      base.OnModelCreating( modelBuilder );
    }

    public async Task<bool> SaveEntitiesAsync( CancellationToken cancellationToken = default( CancellationToken ) )
    {
      await SaveChangesAsync( cancellationToken );
      return true;
    }
  }
}
