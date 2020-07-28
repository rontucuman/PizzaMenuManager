using PizzaMenu.Domain.Entities;
using PizzaMenu.Domain.Requests.Pizza;
using PizzaMenu.Domain.Responses.Pizza;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaMenu.Domain.Mappers
{
  public class PizzaMapper : IPizzaMapper
  {
    public Pizza Map( AddPizzaRequest request )
    {
      if ( request == null )
      {
        return null;
      }

      Pizza pizza = new Pizza
      {
        Name = request.Name
      };

      return pizza;
    }

    public Pizza Map( EditPizzaRequest request )
    {
      if ( request == null )
      {
        return null;
      }

      Pizza pizza = new Pizza
      {
        RowGuid = request.Guid,
        Name = request.Name
      };

      return pizza;
    }

    public PizzaResponse Map( Pizza pizza )
    {
      if ( pizza == null )
      {
        return null;
      }

      PizzaResponse response = new PizzaResponse
      {
        Guid = pizza.RowGuid,
        Name = pizza.Name
      };

      return response;
    }
  }
}
