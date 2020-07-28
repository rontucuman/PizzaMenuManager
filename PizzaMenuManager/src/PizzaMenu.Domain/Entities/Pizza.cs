using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaMenu.Domain.Entities
{
  public class Pizza
  {
    public int Id { get; set; }
    public Guid RowGuid { get; set; }
    public string Name { get; set; }
    public bool IsInactive { get; set; }

    public ICollection<PizzaIngredient> PizzaIngredients { get; set; }
  }
}
