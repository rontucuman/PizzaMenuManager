using Microsoft.AspNetCore.Mvc;
using PizzaMenu.API.Filters;
using PizzaMenu.API.ResponseModels;
using PizzaMenu.Domain.Requests.Pizza;
using PizzaMenu.Domain.Responses.Pizza;
using PizzaMenu.Domain.Services;
using RiskFirst.Hateoas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaMenu.API.Controllers
{
  [Route( "api/hateoas/pizzas" )]
  [ApiController]
  [JsonException]
  public class PizzaHateoasController : ControllerBase
  {
    private readonly ILinksService _linksService;
    private readonly IPizzaService _pizzaService;

    public PizzaHateoasController( ILinksService linksService, IPizzaService pizzaService )
    {
      _linksService = linksService ?? throw new ArgumentNullException( nameof( linksService ) );
      _pizzaService = pizzaService ?? throw new ArgumentNullException( nameof( pizzaService ) );
    }

    [HttpGet( Name = nameof( Get ) )]
    public async Task<IActionResult> Get( [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0 )
    {
      IEnumerable<PizzaResponse> result = await _pizzaService.GetPizzasAsync();
      int totalPizzas = result.Count();

      IEnumerable<PizzaResponse> pizzasOnPage = result.OrderBy( p => p.Name )
        .Skip( pageSize * pageIndex )
        .Take( pageSize );

      List<PizzaHateoasResponse> hateoasResults = new List<PizzaHateoasResponse>();

      foreach ( PizzaResponse pizzaResponse in pizzasOnPage )
      {
        PizzaHateoasResponse hateoasResult = new PizzaHateoasResponse { Data = pizzaResponse };
        await _linksService.AddLinksAsync( hateoasResult );

        hateoasResults.Add( hateoasResult );
      }

      PaginatedPizzaResponseModel<PizzaHateoasResponse> model = new PaginatedPizzaResponseModel<PizzaHateoasResponse>(
        pageIndex, pageSize, totalPizzas, hateoasResults );

      return Ok( model );
    }

    [HttpGet( "{guid:guid}", Name = nameof( GetById ) )]
    [PizzaExists]
    public async Task<IActionResult> GetById( Guid guid )
    {
      PizzaResponse result = await _pizzaService.GetPizzaAsync( new GetPizzaRequest { Guid = guid } );
      PizzaHateoasResponse hateoasResult = new PizzaHateoasResponse { Data = result };
      await _linksService.AddLinksAsync( hateoasResult );

      return Ok( hateoasResult );
    }

    [HttpPost( Name = nameof( Post ) )]
    public async Task<IActionResult> Post( AddPizzaRequest request )
    {
      PizzaResponse result = await _pizzaService.AddPizzaAsync( request );
      return CreatedAtAction( nameof( GetById ), new { guid = result.Guid }, null );
    }

    [HttpPut( "{guid:guid}", Name = nameof( Put ) )]
    [PizzaExists]
    public async Task<IActionResult> Put( Guid guid, EditPizzaRequest request )
    {
      request.Guid = guid;
      PizzaResponse result = await _pizzaService.EditPizzaAsync( request );

      PizzaHateoasResponse hateoasResult = new PizzaHateoasResponse { Data = result };
      await _linksService.AddLinksAsync( hateoasResult );

      return Ok( hateoasResult );
    }

    [HttpDelete( "{guid:guid}", Name = nameof( Delete ) )]
    [PizzaExists]
    public async Task<IActionResult> Delete( Guid guid )
    {
      DeletePizzaRequest request = new DeletePizzaRequest { Guid = guid };
      await _pizzaService.DeletePizzaAsync( request );
      return NoContent();
    }
  }
}
