using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PizzaMenu.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaMenu.Fixtures
{
  public class InMemoryApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
  {
    protected override void ConfigureWebHost( IWebHostBuilder builder )
    {
      builder
        .UseEnvironment( "Testing" )
        .ConfigureTestServices( services =>
         {
           DbContextOptions<PizzaMenuContext> options = new DbContextOptionsBuilder<PizzaMenuContext>()
             .UseInMemoryDatabase( Guid.NewGuid().ToString() )
             .Options;

           services.AddScoped<PizzaMenuContext>( serviceProvider =>
            new TestPizzaMenuContext( options ) );

           ServiceProvider sp = services.BuildServiceProvider();

           using IServiceScope scope = sp.CreateScope();

           IServiceProvider scopedServices = scope.ServiceProvider;
           PizzaMenuContext db = scopedServices.GetRequiredService<PizzaMenuContext>();
           db.Database.EnsureCreated();
         } );
    }
  }
}
