using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaMenu.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaMenu.Infrastructure.SchemaDefinitions
{
  public class PizzaIngredientEntitySchemaDefinition : IEntityTypeConfiguration<PizzaIngredient>
  {
    public void Configure( EntityTypeBuilder<PizzaIngredient> builder )
    {
      builder.ToTable( "PizzaIngredient", PizzaMenuContext.DEFAULT_SCHEMA );
      builder.HasKey( k => k.Id );
      builder.HasOne<Pizza>( p => p.Pizza )
        .WithMany( p => p.PizzaIngredients )
        .HasForeignKey( p => p.PizzaId );
      builder.HasOne<Ingredient>( p => p.Ingredient )
        .WithMany( p => p.PizzaIngredients )
        .HasForeignKey( p => p.IngredientId );
    }
  }
}
