using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PizzaMenu.API.Extensions;
using PizzaMenu.Domain.Extensions;
using PizzaMenu.Domain.Repositories;
using PizzaMenu.Infrastructure.Repositories;

namespace PizzaMenu.API
{
  public class Startup
  {
    public Startup( IConfiguration configuration )
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices( IServiceCollection services )
    {
      services.AddPizzaMenuContext( Configuration.GetSection( "DataSource:ConnectionString" ).Value )
        .AddScoped<IPizzaRepository, PizzaRepository>()
        .AddScoped<IIngredientRepository, IngredientRepository>()
        .AddMappers()
        .AddServices()
        .AddControllers()
        .AddValidation();

      services.AddHateoasLinks();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
    {
      if ( env.IsDevelopment() )
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints( endpoints =>
       {
         endpoints.MapControllers();
       } );
    }
  }
}
