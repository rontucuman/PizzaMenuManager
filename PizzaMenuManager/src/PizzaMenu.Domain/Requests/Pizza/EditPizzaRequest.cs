using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaMenu.Domain.Requests.Pizza
{
  public class EditPizzaRequest
  {
    public Guid Guid { get; set; }
    public string Name { get; set; }
  }
}
