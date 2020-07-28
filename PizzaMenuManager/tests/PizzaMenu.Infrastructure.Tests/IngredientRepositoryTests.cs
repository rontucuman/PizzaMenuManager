using PizzaMenu.Domain.Entities;
using PizzaMenu.Domain.Repositories;
using PizzaMenu.Fixtures;
using PizzaMenu.Infrastructure.Repositories;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PizzaMenu.Infrastructure.Tests
{
  public class IngredientRepositoryTests : IClassFixture<PizzaMenuContextFactory>
  {
    private readonly PizzaMenuContextFactory _factory;

    public IngredientRepositoryTests( PizzaMenuContextFactory factory )
    {
      _factory = factory;
    }

    [Theory]
    [LoadData( "ingredient" )]
    public async Task Should_Return_Record_By_Id( Ingredient ingredient )
    {
      IIngredientRepository repo = new IngredientRepository( _factory.ContextInstance );
      Ingredient result = await repo.GetAsync( ingredient.Id );

      result.Id.ShouldBe( ingredient.Id );
      result.Name.ShouldBe( ingredient.Name );
      result.RowGuid.ShouldBe( ingredient.RowGuid );
    }

    [Theory]
    [LoadData( "ingredient" )]
    public async Task Should_Add_New_Ingredient( Ingredient ingredient )
    {
      ingredient.RowGuid = Guid.NewGuid();
      IIngredientRepository repo = new IngredientRepository( _factory.ContextInstance );

      repo.Add( ingredient );
      await repo.UnitOfWork.SaveEntitiesAsync();

      _factory.ContextInstance.Ingredients.FirstOrDefault( p => p.Id == ingredient.Id ).ShouldNotBeNull();
    }
  }
}
