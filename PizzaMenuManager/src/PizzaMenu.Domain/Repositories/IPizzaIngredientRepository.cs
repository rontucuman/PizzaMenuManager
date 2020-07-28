using PizzaMenu.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMenu.Domain.Repositories
{
  public interface IPizzaIngredientRepository : IRepository
  {
    Task<IEnumerable<PizzaIngredient>> GetAsync();
    Task<PizzaIngredient> GetAsync( int id );
    Task<IEnumerable<PizzaIngredient>> GetByPizzaId( int pizzaId );
    Task<IEnumerable<PizzaIngredient>> GetByIngredientId( int ingredientId );
    PizzaIngredient Add( PizzaIngredient pizzaIngredient );
  }
}
