using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaMenu.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaMenu.Infrastructure.SchemaDefinitions
{
  public class IngredientEntitySchemaDefinition : IEntityTypeConfiguration<Ingredient>
  {
    public void Configure( EntityTypeBuilder<Ingredient> builder )
    {

      builder.ToTable( "Ingredient", PizzaMenuContext.DEFAULT_SCHEMA );
      builder.HasKey( k => k.Id );
      builder.Property( p => p.Name ).IsRequired().HasMaxLength( 64 );
      builder.Property( p => p.RowGuid ).HasDefaultValueSql( "NEWID()" );
    }
  }
}
