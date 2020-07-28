using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaMenu.Domain.Requests.Ingredient
{
  public class EditIngredientRequest
  {
    public Guid Guid { get; set; }
    public string Name { get; set; }
  }
}
