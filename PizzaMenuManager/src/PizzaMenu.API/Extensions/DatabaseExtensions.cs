using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PizzaMenu.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaMenu.API.Extensions
{
  public static class DatabaseExtensions
  {
    public static IServiceCollection AddPizzaMenuContext( this IServiceCollection services, string connectionString )
    {
      return services.AddDbContext<PizzaMenuContext>(
        contextOptions =>
        {
          contextOptions.UseSqlServer( connectionString,
            serverOptions =>
            {
              serverOptions.MigrationsAssembly( typeof( Startup ).Assembly.FullName );
            } );
        } );
    }
  }
}