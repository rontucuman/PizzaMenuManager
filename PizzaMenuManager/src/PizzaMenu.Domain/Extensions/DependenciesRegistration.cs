using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using PizzaMenu.Domain.Mappers;
using PizzaMenu.Domain.Services;
using System.Reflection;

namespace PizzaMenu.Domain.Extensions
{
  public static class DependenciesRegistration
  {
    public static IServiceCollection AddMappers(this IServiceCollection services )
    {
      services.AddSingleton<IPizzaMapper, PizzaMapper>();
      services.AddSingleton<IIngredientMappper, IngredientMapper>();

      return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services )
    {
      services.AddScoped<IPizzaService, PizzaService>();
      services.AddScoped<IIngredientService, IngredientService>();

      return services;
    }

    public static IMvcBuilder AddValidation(this IMvcBuilder builder )
    {
      builder.AddFluentValidation( configuration =>
        configuration.RegisterValidatorsFromAssembly( Assembly.GetExecutingAssembly() ) );

      return builder;
    }
  }
}
