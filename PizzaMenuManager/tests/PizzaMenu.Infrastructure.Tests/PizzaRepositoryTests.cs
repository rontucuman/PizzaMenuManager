using PizzaMenu.Domain.Entities;
using PizzaMenu.Fixtures;
using PizzaMenu.Infrastructure.Repositories;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PizzaMenu.Infrastructure.Tests
{
  public class PizzaRepositoryTests : IClassFixture<PizzaMenuContextFactory>
  {
    private readonly PizzaRepository _pizzaRepository;
    private readonly TestPizzaMenuContext _context;

    public PizzaRepositoryTests( PizzaMenuContextFactory pizzaMenuContextFactory )
    {
      _context = pizzaMenuContextFactory.ContextInstance;
      _pizzaRepository = new PizzaRepository( _context );
    }

    [Fact]
    public async Task Should_Get_DataAsync()
    {
      IEnumerable<Pizza> result = await _pizzaRepository.GetAsync();

      result.ShouldNotBeNull();
    }

    [Fact]
    public async Task Should_Returns_Null_With_Id_Not_Present()
    {
      Pizza result = await _pizzaRepository.GetAsync( 10 );

      result.ShouldBeNull();
    }

    [Theory]
    [InlineData(1)]
    public async Task Should_Return_Record_By_Id(int id)
    {
      Pizza result = await _pizzaRepository.GetAsync( id );

      result.Id.ShouldBe( 1 );
    }

    [Fact]
    public async Task Should_Add_New_Pizza()
    {
      var testPizza = new Pizza
      {
        Name = "Pizza Test",
      };

      _pizzaRepository.Add( testPizza );
      await _pizzaRepository.UnitOfWork.SaveEntitiesAsync();

      _context.Pizzas
        .FirstOrDefault( _ => _.Name == testPizza.Name )
        .ShouldNotBeNull();
    }

    [Fact]
    public async Task Should_Update_Item()
    {
      Pizza testPizza = new Pizza
      {
        Id = 1,
        Name = "Test Pizza"
      };

      _pizzaRepository.Update( testPizza );

      await _pizzaRepository.UnitOfWork.SaveEntitiesAsync();

      _context.Pizzas.FirstOrDefault( x => x.Id == testPizza.Id )
        ?.Name.ShouldBe( "Test Pizza" );
    }
  }
}
