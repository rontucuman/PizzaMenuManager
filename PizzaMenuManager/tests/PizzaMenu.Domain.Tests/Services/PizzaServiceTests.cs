using PizzaMenu.Domain.Mappers;
using PizzaMenu.Domain.Repositories;
using PizzaMenu.Domain.Requests.Pizza;
using PizzaMenu.Domain.Responses.Pizza;
using PizzaMenu.Domain.Services;
using PizzaMenu.Fixtures;
using PizzaMenu.Infrastructure.Repositories;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PizzaMenu.Domain.Tests.Services
{
  public class PizzaServiceTests : IClassFixture<PizzaMenuContextFactory>
  {
    private readonly IPizzaRepository _pizzaRepository;
    private readonly IPizzaMapper _mapper;

    public PizzaServiceTests( PizzaMenuContextFactory pizzaMenuContextFactory )
    {
      _pizzaRepository = new PizzaRepository( pizzaMenuContextFactory.ContextInstance );
      _mapper = pizzaMenuContextFactory.PizzaMapper;
    }

    [Fact]
    public async Task GetPizzas_Should_Return_Right_Data()
    {
      IPizzaService service = new PizzaService( _pizzaRepository, _mapper );
      IEnumerable<PizzaResponse> result = await service.GetPizzasAsync();
      result.ShouldNotBeNull();
    }

    [Theory]
    [InlineData( "3eb00b42-a9f0-4012-841d-70ebf3ab7474" )]
    public async Task GetPizza_Should_Return_Right_Data( string guid )
    {
      IPizzaService service = new PizzaService( _pizzaRepository, _mapper );
      PizzaResponse result = await service.GetPizzaAsync( new GetPizzaRequest { Guid = new Guid( guid ) } );
      result.Guid.ShouldBe( new Guid( guid ) );
    }

    [Fact]
    public void GetPizza_Should_Thrown_Exception_With_Null_Id()
    {
      IPizzaService service = new PizzaService( _pizzaRepository, _mapper );
      service.GetPizzaAsync( null ).ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public async Task AddPizza_Should_Add_Right_Entity()
    {
      AddPizzaRequest testRequest = new AddPizzaRequest
      {
        Name = "Test Pizza 101"
      };

      IPizzaService service = new PizzaService( _pizzaRepository, _mapper );
      PizzaResponse result = await service.AddPizzaAsync( testRequest );
      result.Name.ShouldBe( testRequest.Name );
    }

    [Fact]
    public async Task EditPizza_Should_Edit_Right_Entity()
    {
      EditPizzaRequest request = new EditPizzaRequest
      {
        Guid = new Guid("3eb00b42-a9f0-4012-841d-70ebf3ab7474"),
        Name = "Test Pizza 101 Edited"
      };

      IPizzaService service = new PizzaService( _pizzaRepository, _mapper );
      PizzaResponse result = await service.EditPizzaAsync( request );
      result.Name.ShouldBe( "Test Pizza 101 Edited" );
    }
  }
}
