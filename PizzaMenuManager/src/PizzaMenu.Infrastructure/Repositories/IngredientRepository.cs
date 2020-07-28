using Microsoft.EntityFrameworkCore;
using PizzaMenu.Domain.Entities;
using PizzaMenu.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMenu.Infrastructure.Repositories
{
  public class IngredientRepository : IIngredientRepository
  {
    private readonly PizzaMenuContext _context;
    public IUnitOfWork UnitOfWork => _context;

    public IngredientRepository(PizzaMenuContext context)
    {
      _context = context ?? throw new ArgumentNullException( nameof( context ) );
    }

    public Ingredient Add( Ingredient ingredient )
    {
      return _context.Ingredients.Add( ingredient ).Entity;
    }

    public async Task<IEnumerable<Ingredient>> GetAsync()
    {
      return await _context.Ingredients.AsNoTracking().ToListAsync();
    }

    public async Task<Ingredient> GetAsync( int id )
    {
      Ingredient ingredient = await _context.Ingredients.AsNoTracking().Where( p => p.Id == id ).FirstOrDefaultAsync();

      if ( ingredient == null )
      {
        return null;
      }

      _context.Entry( ingredient ).State = EntityState.Detached;

      return ingredient;
    }

    public async Task<Ingredient> GetAsync( Guid guid )
    {
      Ingredient ingredient = await _context.Ingredients.AsNoTracking().Where( p => p.RowGuid == guid ).FirstOrDefaultAsync();

      if ( ingredient == null )
      {
        return null;
      }

      _context.Entry( ingredient ).State = EntityState.Detached;

      return ingredient;
    }

    public Ingredient Update( Ingredient ingredient )
    {
      throw new NotImplementedException();
    }

    public async Task<IEnumerable<Ingredient>> GetIngredientsByPizzaId( int id )
    {
      return await _context.Ingredients
        .Join<Ingredient, PizzaIngredient, int, Ingredient>( 
        _context.PizzaIngredients, ing => ing.Id, pIng => pIng.IngredientId, ( ing, pIng ) => ing )
        .ToListAsync();
    }
  }
}
