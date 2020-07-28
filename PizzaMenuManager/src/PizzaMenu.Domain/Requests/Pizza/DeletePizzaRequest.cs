using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaMenu.Domain.Requests.Pizza
{
  public class DeletePizzaRequest
  {
    public Guid Guid { get; set; }
  }
}
