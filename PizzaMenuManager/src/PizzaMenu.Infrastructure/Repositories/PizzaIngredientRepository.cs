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
  public class PizzaIngredientRepository : IPizzaIngredientRepository
  {
    private readonly PizzaMenuContext _context;
    public IUnitOfWork UnitOfWork => _context;

    public PizzaIngredientRepository( PizzaMenuContext context)
    {
      _context = context ?? throw new ArgumentNullException( nameof( context ) );
    }
    public PizzaIngredient Add( PizzaIngredient pizzaIngredient )
    {
      throw new NotImplementedException();
    }

    public async Task<IEnumerable<PizzaIngredient>> GetAsync()
    {
      return await _context.PizzaIngredients.AsNoTracking().ToListAsync();
    }

    public async Task<PizzaIngredient> GetAsync( int id )
    {
      PizzaIngredient result = await _context.PizzaIngredients.AsNoTracking().Where( p => p.Id == id ).FirstOrDefaultAsync();

      if ( result == null )
      {
        return null;
      }

      _context.Entry( result ).State = EntityState.Detached;

      return result;
    }

    public async Task<IEnumerable<PizzaIngredient>> GetByIngredientId( int ingredientId )
    {
      return await _context.PizzaIngredients.AsNoTracking().Where( p => p.IngredientId == ingredientId ).ToListAsync();
    }

    public async Task<IEnumerable<PizzaIngredient>> GetByPizzaId( int pizzaId )
    {
      return await _context.PizzaIngredients.AsNoTracking().Where( p => p.PizzaId == pizzaId ).ToListAsync();
    }
  }
}
