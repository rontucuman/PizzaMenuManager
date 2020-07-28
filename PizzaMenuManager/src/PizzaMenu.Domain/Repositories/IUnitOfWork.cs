﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PizzaMenu.Domain.Repositories
{
  public interface IUnitOfWork : IDisposable
  {
    Task<bool> SaveEntitiesAsync( CancellationToken cancellationToken = default( CancellationToken ) );
  }
}
