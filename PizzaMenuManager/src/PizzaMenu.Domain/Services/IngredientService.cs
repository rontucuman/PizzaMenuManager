using PizzaMenu.Domain.Entities;
using PizzaMenu.Domain.Mappers;
using PizzaMenu.Domain.Repositories;
using PizzaMenu.Domain.Requests.Ingredient;
using PizzaMenu.Domain.Responses.Ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMenu.Domain.Services
{
  public class IngredientService : IIngredientService
  {
    private readonly IIngredientRepository _ingredientRepository;
    private readonly IIngredientMappper _ingredientMapper;

    public IngredientService( IIngredientRepository repository, IIngredientMappper mapper )
    {
      _ingredientRepository = repository ?? throw new ArgumentNullException( nameof( repository ) );
      _ingredientMapper = mapper ?? throw new ArgumentNullException( nameof( mapper ) );
    }
    public async Task<IngredientResponse> AddIngredientAsync( AddIngredientRequest request )
    {
      if ( string.IsNullOrWhiteSpace( request?.Name ) )
      {
        throw new ArgumentNullException();
      }

      Ingredient ingredient = _ingredientMapper.Map( request );
      Ingredient result = _ingredientRepository.Add( ingredient );

      await _ingredientRepository.UnitOfWork.SaveEntitiesAsync();

      return _ingredientMapper.Map( result );
    }

    public async Task<IngredientResponse> GetIngredientAsync( GetIngredientRequest request )
    {
      if ( request?.Guid == null )
      {
        throw new ArgumentNullException();
      }

      Ingredient ingredient = await _ingredientRepository.GetAsync( request.Guid );

      return _ingredientMapper.Map( ingredient );
    }

    public async Task<IEnumerable<IngredientResponse>> GetIngredientsAsync()
    {
      IEnumerable<Ingredient> result = await _ingredientRepository.GetAsync();
      return result.Select( x => _ingredientMapper.Map( x ) );
    }
  }
}
