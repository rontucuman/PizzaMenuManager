using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PizzaMenu.Domain.Requests.Pizza;
using PizzaMenu.Domain.Responses.Pizza;
using PizzaMenu.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaMenu.API.Filters
{
  public class PizzaExistsAttribute : TypeFilterAttribute
  {
    public PizzaExistsAttribute() : base( typeof( PizzaExistsFilterImpl ) )
    {
    }

    public class PizzaExistsFilterImpl : IAsyncActionFilter
    {
      private readonly IPizzaService _pizzaService;

      public PizzaExistsFilterImpl( IPizzaService pizzaService )
      {
        _pizzaService = pizzaService ?? throw new ArgumentNullException( nameof( pizzaService ) );
      }

      public async Task OnActionExecutionAsync( ActionExecutingContext context, ActionExecutionDelegate next )
      {
        if ( !( context.ActionArguments[ "guid" ] is Guid guid ) )
        {
          context.Result = new BadRequestResult();
          return;
        }

        PizzaResponse result = await _pizzaService.GetPizzaAsync( new GetPizzaRequest { Guid = guid } );

        if ( result == null )
        {
          context.Result = new NotFoundObjectResult( $"Pizza with id {guid} not exists." );
          return;
        }

        await next();
      }
    }
  }
}
