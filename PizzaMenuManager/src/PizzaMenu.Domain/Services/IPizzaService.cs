using PizzaMenu.Domain.Requests.Pizza;
using PizzaMenu.Domain.Responses.Ingredient;
using PizzaMenu.Domain.Responses.Pizza;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMenu.Domain.Services
{
  public interface IPizzaService
  {
    Task<IEnumerable<PizzaResponse>> GetPizzasAsync();
    Task<PizzaResponse> GetPizzaAsync(GetPizzaRequest request);
    Task<PizzaResponse> AddPizzaAsync( AddPizzaRequest request );
    Task<PizzaResponse> EditPizzaAsync( EditPizzaRequest request );
    Task<PizzaResponse> DeletePizzaAsync( DeletePizzaRequest request );
    Task<IEnumerable<IngredientResponse>> GetIngredientsByPizzaAsync( GetPizzaRequest request );
  }
}
