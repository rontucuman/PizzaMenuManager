using PizzaMenu.Domain.Entities;
using PizzaMenu.Domain.Requests.Pizza;
using PizzaMenu.Domain.Responses.Pizza;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaMenu.Domain.Mappers
{
  public interface IPizzaMapper
  {
    Pizza Map( AddPizzaRequest request );
    Pizza Map( EditPizzaRequest request );
    PizzaResponse Map( Pizza pizza );
  }
}
