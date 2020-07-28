using PizzaMenu.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMenu.Domain.Repositories
{
  public interface IPizzaRepository : IRepository
  {
    Task<IEnumerable<Pizza>> GetAsync();
    Task<Pizza> GetAsync( int id );
    Task<Pizza> GetAsync( Guid guid );
    Pizza Add( Pizza pizza );
    Pizza Update( Pizza pizza );
  }
}
