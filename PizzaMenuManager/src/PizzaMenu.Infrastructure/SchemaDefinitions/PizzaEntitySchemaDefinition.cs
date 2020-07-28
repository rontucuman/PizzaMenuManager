using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaMenu.Domain.Entities;
using System;

namespace PizzaMenu.Infrastructure.SchemaDefinitions
{
  public class PizzaEntitySchemaDefinition : IEntityTypeConfiguration<Pizza>
  {
    public void Configure( EntityTypeBuilder<Pizza> builder )
    {
      builder.ToTable( "Pizza", PizzaMenuContext.DEFAULT_SCHEMA );
      builder.HasKey( k => k.Id );
      builder.Property( p => p.Name ).IsRequired().HasMaxLength( 64 );
      builder.Property( p => p.RowGuid ).HasDefaultValueSql( "NEWID()" );
    }
  }
}
