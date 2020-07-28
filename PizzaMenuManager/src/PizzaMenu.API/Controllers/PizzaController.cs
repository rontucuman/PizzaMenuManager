using Microsoft.AspNetCore.Mvc;
using PizzaMenu.API.Filters;
using PizzaMenu.API.ResponseModels;
using PizzaMenu.Domain.Requests.Pizza;
using PizzaMenu.Domain.Responses.Pizza;
using PizzaMenu.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaMenu.API.Controllers
{
  [Route( "api/pizzas" )]
  [ApiController]
  [JsonException]
  public class PizzaController : ControllerBase
  {
    private readonly IPizzaService _pizzaService;

    public PizzaController( IPizzaService pizzaService )
    {
      _pizzaService = pizzaService ?? throw new ArgumentNullException( nameof( pizzaService ) );
    }

    [HttpGet]
    public async Task<IActionResult> Get( int pageSize = 10, int pageIndex = 0 )
    {
      IEnumerable<PizzaResponse> result = await _pizzaService.GetPizzasAsync();
      int totalPizzas = result.Count();

      IEnumerable<PizzaResponse> pizzasOnPage = result.OrderBy( c => c.Name )
        .Skip( pageSize * pageIndex )
        .Take( pageSize );

      PaginatedPizzaResponseModel<PizzaResponse> model = new PaginatedPizzaResponseModel<PizzaResponse>(
        pageIndex, pageSize, totalPizzas, pizzasOnPage );

      return Ok( model );
    }

    [HttpGet( "{guid:guid}" )]
    [PizzaExists]
    public async Task<IActionResult> GetByGuid( Guid guid )
    {
      PizzaResponse result = await _pizzaService.GetPizzaAsync( new GetPizzaRequest
      {
        Guid = guid
      } );

      return Ok( result );
    }

    [HttpPost]
    public async Task<IActionResult> Post( AddPizzaRequest request )
    {
      PizzaResponse result = await _pizzaService.AddPizzaAsync( request );
      return CreatedAtAction( nameof( GetByGuid ), new { guid = result.Guid }, null );
    }

    [HttpPut( "{guid:guid}" )]
    [PizzaExists]
    public async Task<IActionResult> Put( Guid guid, EditPizzaRequest request )
    {
      request.Guid = guid;
      PizzaResponse result = await _pizzaService.EditPizzaAsync( request );

      return Ok( result );
    }

    [HttpDelete("{guid:guid}")]
    [PizzaExists]
    public async Task<IActionResult> Delete(Guid guid )
    {
      var request = new DeletePizzaRequest { Guid = guid };
      await _pizzaService.DeletePizzaAsync( request );

      return NoContent();
    }
  }
}
