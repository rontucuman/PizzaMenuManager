using PizzaMenu.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMenu.Domain.Repositories
{
  public interface IIngredientRepository : IRepository
  {
    Task<IEnumerable<Ingredient>> GetAsync();
    Task<Ingredient> GetAsync( int id );
    Task<Ingredient> GetAsync( Guid guid );
    Task<IEnumerable<Ingredient>> GetIngredientsByPizzaId( int id );
    Ingredient Add( Ingredient ingredient );
    Ingredient Update( Ingredient ingredient );
  }
}
