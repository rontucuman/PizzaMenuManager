using Microsoft.Extensions.DependencyInjection;
using PizzaMenu.API.Controllers;
using PizzaMenu.API.ResponseModels;
using RiskFirst.Hateoas;

namespace PizzaMenu.Domain.Extensions
{
  public static class HateoasLinksRegistration
  {
    public static IServiceCollection AddHateoasLinks( this IServiceCollection services )
    {
      services.AddLinks( config =>
      {
        config.AddPolicy<PizzaHateoasResponse>( policy =>
        {
          policy.RequireRoutedLink( nameof( PizzaHateoasController.Get ), nameof( PizzaHateoasController.Get ) )
          .RequireRoutedLink( nameof( PizzaHateoasController.GetById ), nameof( PizzaHateoasController.GetById ), _ => new { guid = _.Data.Guid } )
          .RequireRoutedLink( nameof( PizzaHateoasController.Post ), nameof( PizzaHateoasController.Post ) )
          .RequireRoutedLink( nameof( PizzaHateoasController.Put ), nameof( PizzaHateoasController.Put ), x => new { guid = x.Data.Guid } )
          .RequireRoutedLink( nameof( PizzaHateoasController.Delete ), nameof( PizzaHateoasController.Delete ), _ => new { guid = _.Data.Guid } );
        } );
      } );

      return services;
    }
  }
}
