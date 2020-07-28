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
  public class PizzaRepository : IPizzaRepository
  {
    private readonly PizzaMenuContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public PizzaRepository( PizzaMenuContext context )
    {
      _context = context ?? throw new ArgumentNullException( nameof( context ) );
    }

    public Pizza Add( Pizza pizza )
    {
      return _context.Pizzas.Add( pizza ).Entity;
    }

    public async Task<IEnumerable<Pizza>> GetAsync()
    {
      return await _context.Pizzas.Where(x => !x.IsInactive).AsNoTracking().ToListAsync();
    }

    public async Task<Pizza> GetAsync( int id )
    {
      Pizza pizza = await _context.Pizzas.AsNoTracking().Where( x => x.Id == id ).FirstOrDefaultAsync();
      return pizza;
    }

    public async Task<Pizza> GetAsync( Guid guid )
    {
      Pizza pizza = await _context.Pizzas.AsNoTracking().Where( x => x.RowGuid == guid ).FirstOrDefaultAsync();
      return pizza;
    }

    public Pizza Update( Pizza pizza )
    {
      _context.Entry( pizza ).State = EntityState.Modified;
      return pizza;
    }
  }
}
