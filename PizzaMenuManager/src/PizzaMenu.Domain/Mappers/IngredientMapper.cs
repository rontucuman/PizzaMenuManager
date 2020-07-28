using PizzaMenu.Domain.Entities;
using PizzaMenu.Domain.Requests.Ingredient;
using PizzaMenu.Domain.Responses.Ingredient;

namespace PizzaMenu.Domain.Mappers
{
  public class IngredientMapper : IIngredientMappper
  {
    public Ingredient Map( AddIngredientRequest request )
    {
      if ( request == null )
      {
        return null;
      }

      Ingredient ingredient = new Ingredient
      {
        Name = request.Name
      };

      return ingredient;
    }

    public Ingredient Map( EditIngredientRequest request )
    {
      if ( request == null )
      {
        return null;
      }

      Ingredient ingredient = new Ingredient
      {
        RowGuid = request.Guid,
        Name = request.Name
      };

      return ingredient;
    }

    public IngredientResponse Map( Ingredient ingredient )
    {
      if ( ingredient == null )
      {
        return null;
      }

      IngredientResponse response = new IngredientResponse
      {
        Guid = ingredient.RowGuid,
        Name = ingredient.Name
      };

      return response;
    }
  }
}
