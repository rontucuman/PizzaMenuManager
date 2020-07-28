using Microsoft.EntityFrameworkCore;
using PizzaMenu.Domain.Entities;
using PizzaMenu.Fixtures.Extensions;
using PizzaMenu.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaMenu.Fixtures
{
  public class TestPizzaMenuContext : PizzaMenuContext
  {
    public TestPizzaMenuContext( DbContextOptions<PizzaMenuContext> options ) : base( options )
    {
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
      base.OnModelCreating( modelBuilder );
      modelBuilder.Seed<Pizza>( "./Data/Pizza.json" );
      modelBuilder.Seed<Ingredient>( "./Data/Ingredient.json" );
      modelBuilder.Seed<PizzaIngredient>( "./Data/PizzaIngredient.json" );
    }
  }
}
