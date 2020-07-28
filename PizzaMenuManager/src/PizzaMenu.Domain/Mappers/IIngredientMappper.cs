using PizzaMenu.Domain.Entities;
using PizzaMenu.Domain.Requests.Ingredient;
using PizzaMenu.Domain.Responses.Ingredient;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaMenu.Domain.Mappers
{
  public interface IIngredientMappper
  {
    Ingredient Map( AddIngredientRequest request );
    Ingredient Map( EditIngredientRequest request );
    IngredientResponse Map( Ingredient ingredient );
  }
}
