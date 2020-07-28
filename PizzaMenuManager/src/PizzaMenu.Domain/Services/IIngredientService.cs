using PizzaMenu.Domain.Entities;
using PizzaMenu.Domain.Requests.Ingredient;
using PizzaMenu.Domain.Responses.Ingredient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMenu.Domain.Services
{
  public interface IIngredientService
  {
    Task<IEnumerable<IngredientResponse>> GetIngredientsAsync();
    Task<IngredientResponse> GetIngredientAsync( GetIngredientRequest request );
    Task<IngredientResponse> AddIngredientAsync( AddIngredientRequest request );
  }
}
