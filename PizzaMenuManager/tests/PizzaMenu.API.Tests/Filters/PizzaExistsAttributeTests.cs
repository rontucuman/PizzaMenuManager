using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using PizzaMenu.API.Filters;
using PizzaMenu.Domain.Requests.Pizza;
using PizzaMenu.Domain.Responses.Pizza;
using PizzaMenu.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PizzaMenu.API.Tests.Filters
{
  public class PizzaExistsAttributeTests
  {
    [Fact]
    public async Task Should_Continue_Pipeline_When_Guid_Is_Present()
    {
      Guid guid = Guid.NewGuid();
      Mock<IPizzaService> pizzaService = new Mock<IPizzaService>();

      pizzaService
        .Setup( x => x.GetPizzaAsync( It.IsAny<GetPizzaRequest>() ) )
        .ReturnsAsync( new PizzaResponse { Guid = guid } );

      PizzaExistsAttribute.PizzaExistsFilterImpl filter = new PizzaExistsAttribute.PizzaExistsFilterImpl( pizzaService.Object );

      ActionExecutingContext actionExecutedContext = new ActionExecutingContext(
        new ActionContext( new DefaultHttpContext(), new RouteData(), new ActionDescriptor() ),
        new List<IFilterMetadata>(),
        new Dictionary<string, object>
        {
          { "guid", guid }
        }, new object() );

      Mock<ActionExecutionDelegate> nextCallBack = new Mock<ActionExecutionDelegate>();

      await filter.OnActionExecutionAsync( actionExecutedContext, nextCallBack.Object );

      nextCallBack.Verify( executionDelegate => executionDelegate(), Times.Once );
    }
  }
}
