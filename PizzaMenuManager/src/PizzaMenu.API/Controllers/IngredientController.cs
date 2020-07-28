using Microsoft.AspNetCore.Mvc;
using PizzaMenu.API.Filters;
using PizzaMenu.API.ResponseModels;
using PizzaMenu.Domain.Requests.Ingredient;
using PizzaMenu.Domain.Responses.Ingredient;
using PizzaMenu.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaMenu.API.Controllers
{
  [Route("api/ingredient")]
  [ApiController]
  [JsonException]
  public class IngredientController : ControllerBase
  {
    private readonly IIngredientService _ingredientService;

    public IngredientController(IIngredientService service)
    {
      _ingredientService = service ?? throw new ArgumentNullException( nameof( service ) );
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0 )
    {
      IEnumerable<IngredientResponse> result = await _ingredientService.GetIngredientsAsync();
      int totalItems = result.ToList().Count;
      IEnumerable<IngredientResponse> itemsOnPage = result.OrderBy( c => c.Name ).Skip( pageSize * pageIndex ).Take( pageSize );
      PaginatedPizzaResponseModel<IngredientResponse> model = new PaginatedPizzaResponseModel<IngredientResponse>( pageIndex, pageSize, totalItems, itemsOnPage );

      return Ok( model );
    }

    [HttpGet("guid:guid")]
    public async Task<IActionResult> GetByGuid(Guid guid)
    {
      IngredientResponse result = await _ingredientService.GetIngredientAsync( new GetIngredientRequest { Guid = guid } );

      return Ok( result );
    }

    [HttpPost]
    public async Task<IActionResult> Post(AddIngredientRequest request )
    {
      IngredientResponse result = await _ingredientService.AddIngredientAsync( request );

      return CreatedAtAction( nameof( GetByGuid ), new { Guid = result.Guid }, null );
    }
  }
}
