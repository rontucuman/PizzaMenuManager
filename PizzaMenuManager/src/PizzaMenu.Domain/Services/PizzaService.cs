using PizzaMenu.Domain.Entities;
using PizzaMenu.Domain.Mappers;
using PizzaMenu.Domain.Repositories;
using PizzaMenu.Domain.Requests.Pizza;
using PizzaMenu.Domain.Responses.Ingredient;
using PizzaMenu.Domain.Responses.Pizza;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMenu.Domain.Services
{
  public class PizzaService : IPizzaService
  {
    private readonly IPizzaRepository _pizzaRepository;
    private readonly IPizzaMapper _pizzaMapper;

    public PizzaService( IPizzaRepository pizzaRepository, IPizzaMapper pizzaMapper )
    {
      _pizzaRepository = pizzaRepository ?? throw new ArgumentNullException( nameof( pizzaRepository ) );
      _pizzaMapper = pizzaMapper ?? throw new ArgumentNullException( nameof( pizzaMapper ) );
    }
    public async Task<PizzaResponse> AddPizzaAsync( AddPizzaRequest request )
    {
      if ( string.IsNullOrWhiteSpace( request?.Name ) )
      {
        throw new ArgumentNullException();
      }

      Pizza pizza = _pizzaMapper.Map( request );
      Pizza result = _pizzaRepository.Add( pizza );

      await _pizzaRepository.UnitOfWork.SaveEntitiesAsync();

      return _pizzaMapper.Map( result );
    }

    public async Task<PizzaResponse> DeletePizzaAsync( DeletePizzaRequest request )
    {
      if ( request?.Guid == null )
      {
        throw new ArgumentNullException( nameof( request ) );
      }

      Pizza result = await _pizzaRepository.GetAsync( request.Guid );
      result.IsInactive = true;

      _pizzaRepository.Update( result );
      await _pizzaRepository.UnitOfWork.SaveEntitiesAsync();

      return _pizzaMapper.Map( result );
    }

    public async Task<PizzaResponse> EditPizzaAsync( EditPizzaRequest request )
    {
      if ( request?.Guid == null )
      {
        throw new ArgumentNullException();
      }

      Pizza existingPizza = await _pizzaRepository.GetAsync( request.Guid );

      if ( existingPizza == null )
      {
        throw new ArgumentNullException( "No valid record found." );
      }

      Pizza entity = _pizzaMapper.Map( request );

      existingPizza.Name = entity.Name;

      Pizza result = _pizzaRepository.Update( existingPizza );
      await _pizzaRepository.UnitOfWork.SaveEntitiesAsync();

      return _pizzaMapper.Map( result );
    }

    public Task<IEnumerable<IngredientResponse>> GetIngredientsByPizzaAsync( GetPizzaRequest request )
    {
      throw new NotImplementedException();
    }

    public async Task<PizzaResponse> GetPizzaAsync( GetPizzaRequest request )
    {
      if ( request?.Guid == null )
      {
        throw new ArgumentNullException();
      }

      Pizza entity = await _pizzaRepository.GetAsync( request.Guid );

      return _pizzaMapper.Map( entity );
    }

    public async Task<IEnumerable<PizzaResponse>> GetPizzasAsync()
    {
      IEnumerable<Pizza> result = await _pizzaRepository.GetAsync();
      return result.Select( x => _pizzaMapper.Map( x ) );
    }
  }
}
