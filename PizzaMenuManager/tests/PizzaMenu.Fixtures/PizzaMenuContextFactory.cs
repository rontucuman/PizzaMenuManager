using Microsoft.EntityFrameworkCore;
using PizzaMenu.Domain.Mappers;
using PizzaMenu.Infrastructure;
using System;

namespace PizzaMenu.Fixtures
{
  public class PizzaMenuContextFactory
  {
    public readonly TestPizzaMenuContext ContextInstance;
    public readonly IPizzaMapper PizzaMapper;

    public PizzaMenuContextFactory()
    {
      DbContextOptions<PizzaMenuContext> contextOptions = new DbContextOptionsBuilder<PizzaMenuContext>()
        .UseInMemoryDatabase( Guid.NewGuid().ToString() )
        .EnableSensitiveDataLogging()
        .Options;

      EnsureCreation( contextOptions );
      ContextInstance = new TestPizzaMenuContext( contextOptions );

      PizzaMapper = new PizzaMapper();
    }

    private void EnsureCreation( DbContextOptions<PizzaMenuContext> contextOptions )
    {
      using TestPizzaMenuContext context = new TestPizzaMenuContext( contextOptions );
      context.Database.EnsureCreated();
    }
  }
}
