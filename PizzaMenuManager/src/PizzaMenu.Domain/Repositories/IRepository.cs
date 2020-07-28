using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaMenu.Domain.Repositories
{
  public interface IRepository
  {
    IUnitOfWork UnitOfWork { get; }
  }
}
